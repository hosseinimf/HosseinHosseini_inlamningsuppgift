using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class CustomersPage : Page
    {
        public CustomersPage()
        {
            this.InitializeComponent();

            LoadCustomersAsync().GetAwaiter();
        }

        private async void btnCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxFirstName.Text) || string.IsNullOrWhiteSpace(tbxLastName.Text))
            {
                var dialog = new MessageDialog("Please write the customer's First name and last name!");
                await dialog.ShowAsync();
            }
            else 
            {
                await SqliteContext.CreateCustomerAsync(new Customer { FirstName = tbxFirstName.Text, LastName = tbxLastName.Text});
                
            }
            await LoadCustomersAsync();
            ResetTextBoxes();
        }


        private async Task LoadCustomersAsync()
        {
            lstvCustomer.ItemsSource = await SqliteContext.GetCustomers();
        }


        private void ResetTextBoxes()
        {
            tbxFirstName.Text = string.Empty;
            tbxLastName.Text = string.Empty;
        }

    }
}
