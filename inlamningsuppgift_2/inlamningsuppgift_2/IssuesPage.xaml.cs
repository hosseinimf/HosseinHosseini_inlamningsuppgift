using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace inlamningsuppgift_2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IssuesPage : Page
    {

        private IEnumerable<Issue> issues { get; set; }
        private long _Id { get; set; }

        

        public IssuesPage()
        {
            this.InitializeComponent();
            

            SettingsContext.GetSettingsInformation();

            LoadCustomersAsync().GetAwaiter();
            LoadIssuesAsync().GetAwaiter();
            LoadStatusAsync();
        }


        private void LoadStatusAsync()
        {
            cmbStatus.ItemsSource = SettingsContext.GetStatus();
        }

        private async Task LoadIssuesAsync()
        {
            Settings setting = new Settings();
            setting = await SettingsContext.ReadFromJsonFileAsync("settings.json");

            issues = await SqliteContext.GetIssues();
            LoadActiveIssues(setting.Status[2]); //getting a closed Phrase
            LoadClosedIssues(setting.Status[2]);
        }

        private void LoadActiveIssues(string closedPhrase)
        {
            lstvActiveIssues.ItemsSource = issues
                .Where(i => i.Status != closedPhrase)
                .OrderByDescending(i => i.Created)
                .Take(SettingsContext.GetMaxItemsCount())
                .ToList();
        }

        private void LoadClosedIssues(string closedPhrase)
        {
            lstvClosedIssues.ItemsSource = issues
                .Where(i => i.Status == closedPhrase)
                .OrderByDescending(i => i.Created)
                .Take(SettingsContext.GetMaxItemsCount())
                .ToList();
        }

        private async Task LoadCustomersAsync()
        {
            cmbCustomers.ItemsSource = await SqliteContext.GetCustomers();
        }

        private async void btnCreateIssue_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCustomers.SelectedIndex < 0)
            {

                var dialog = new MessageDialog("Please select a customer!");
                await dialog.ShowAsync();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(tbxTitle.Text) && !string.IsNullOrWhiteSpace(tbxDescription.Text))
                {
                    Customer customer = (Customer)cmbCustomers.SelectedItem;

                    await SqliteContext.CreateIssueAsync(
                        new Issue
                        {
                            Title = tbxTitle.Text,
                            Description = tbxDescription.Text,
                            CustomerId = customer.Id
                        }
                    );
                }
                else
                {
                    var dialog = new MessageDialog("Please fill in both of description and title fields!");
                    await dialog.ShowAsync();
                }
               
            }
            await LoadIssuesAsync();
            ResetTextBoxes();
        }

        private void lstvActiveIssues_ItemClick(object sender, ItemClickEventArgs e)
        {
            lstvClosedIssues.SelectedItem = null;
            Issue issue = e.ClickedItem as Issue;
            _Id = issue.Id;
            tbxComment.IsEnabled = true;
            tbxComment.Text = string.Empty;      
        }

        private async void btnUpdateIssue_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace( tbxTitle.Text) && !string.IsNullOrWhiteSpace (tbxDescription.Text))
            {
                if (cmbStatus.SelectedItem == null)
                {
                    var dialog = new MessageDialog("Please select a status!");
                    await dialog.ShowAsync();
                }
                else
                {
                    await SqliteContext.UpdateIssueById(_Id, cmbStatus.SelectedItem.ToString(), tbxTitle.Text, tbxDescription.Text);
                }
            }
            else 
            {
                if (string.IsNullOrWhiteSpace(tbxTitle.Text) && string.IsNullOrWhiteSpace(tbxDescription.Text))
                {
                    if (cmbStatus.SelectedItem == null)
                    {
                        var dialog = new MessageDialog("Please select a status!");
                        await dialog.ShowAsync();
                    }
                    else
                    {
                        await SqliteContext.UpdateIssueStatusById(_Id, cmbStatus.SelectedItem.ToString());
                    }
                }
                else
                {
                    var dialog = new MessageDialog("If you want to update the titel and description of the issue beside the status, you need fill in both of title and description fields.!");
                    await dialog.ShowAsync();
                }
            }                    
            LoadIssuesAsync().GetAwaiter();
            ResetTextBoxes();
            tbxComment.IsEnabled = false;

        }

        private void lstvClosedIssues_ItemClick(object sender, ItemClickEventArgs e)
        {
            lstvActiveIssues.SelectedItem = null;
            Issue issue = e.ClickedItem as Issue;
            _Id = issue.Id;
            tbxComment.IsEnabled = true;
            tbxComment.Text = string.Empty;
        }

        private void ResetTextBoxes()
        {
            tbxDescription.Text = string.Empty;
            tbxTitle.Text = string.Empty;
            cmbStatus.SelectedIndex = -1;
            cmbCustomers.SelectedIndex = -1;
        }

        private async void btnCreateComment_Click(object sender, RoutedEventArgs e)
        {
            if (lstvActiveIssues.SelectedIndex < 0 && lstvClosedIssues.SelectedIndex < 0)
            {

                var dialog = new MessageDialog("Please select an issue!");
                await dialog.ShowAsync();
            }
            else
            {
                if (lstvActiveIssues.SelectedIndex >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(tbxComment.Text))
                    {
                        Issue issue = (Issue)lstvActiveIssues.SelectedItem;

                        await SqliteContext.CreateCommentAsync(
                            new Comment
                            {
                                Description = tbxComment.Text,
                                IssueId = issue.Id
                            }
                        );
                    }
                    else
                    {
                        var dialog = new MessageDialog("Please Write a comment!");
                        await dialog.ShowAsync();
                    }
                }

                if (lstvClosedIssues.SelectedIndex >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(tbxComment.Text))
                    {
                        Issue issue = (Issue)lstvClosedIssues.SelectedItem;

                        await SqliteContext.CreateCommentAsync(
                            new Comment
                            {
                                Description = tbxComment.Text,
                                IssueId = issue.Id
                            }
                        );
                    }
                    else
                    {
                        var dialog = new MessageDialog("Please Write a comment!");
                        await dialog.ShowAsync();
                    }
                }

                

            }
            
            ResetTextBoxes();
        }
    }
}
