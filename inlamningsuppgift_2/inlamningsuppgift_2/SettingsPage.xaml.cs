using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataAccessLibrary.Data;
using Windows.Storage;
using Windows.ApplicationModel.ConversationalAgent;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace inlamningsuppgift_2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        
        SettingsContext settingService = new SettingsContext();

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private async void btnWriteJsonFile_Click(object sender, RoutedEventArgs e)
        {
            await SettingsContext.CreateJsonFileAsync();

            
            if (string.IsNullOrWhiteSpace(tbxStatus1.Text) && string.IsNullOrWhiteSpace(tbxStatus2.Text)
                && string.IsNullOrWhiteSpace(tbxStatus3.Text) && !string.IsNullOrWhiteSpace(tbxMaxNumber.Text))
            {
                if (Int32.TryParse(tbxMaxNumber.Text, out int max))
                {
                    await SettingsContext.WriteToJsonFileAsync(new Settings("new", "active", "closed", max));
                    txbStatusSaved.Text = "Saved";
                    var dialog = new MessageDialog($"The Maximum number changed to {max}.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Please write a number in the field!");
                    await dialog.ShowAsync();
                }

            }
            else 
            {
                var dialog = new MessageDialog("Please fill in all fields to change the settings (even the max number). Otherwise settings does not change.");
                await dialog.ShowAsync();
            }

            if (!string.IsNullOrWhiteSpace(tbxStatus1.Text) && !string.IsNullOrWhiteSpace(tbxStatus2.Text)
                && !string.IsNullOrWhiteSpace(tbxStatus3.Text) && !string.IsNullOrWhiteSpace(tbxMaxNumber.Text))
            {
                if (Int32.TryParse(tbxMaxNumber.Text, out int max))
                {
                    await SettingsContext.WriteToJsonFileAsync(new Settings(tbxStatus1.Text, tbxStatus2.Text, tbxStatus3.Text, max));
                    txbStatusSaved.Text = "Saved";
                    var dialog = new MessageDialog($"The Maximum number changed to {max}. \n The statuses changed to: {tbxStatus1.Text}  {tbxStatus2.Text}  {tbxStatus3.Text}");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Please write a number in the field!");
                    await dialog.ShowAsync();
                }
            }
            
            SettingsContext.GetSettingsInformation();

        }
    }
}
