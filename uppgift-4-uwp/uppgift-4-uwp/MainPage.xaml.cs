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
using uppgift_4_SharedLibrary_uwp.Models;
using Microsoft.Azure.Devices.Client;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using uppgift_4_SharedLibrary_uwp.Services;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace uppgift_4_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly string connectionString = "HostName=Hossein-hosseini-win20.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=TNxoOXV6Wswyun4f+RHWLH7hxw7aWr8doIEmy9dJ3mc=";
        private static readonly DeviceClient deviceClient =
            DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnSendM_Click(object sender, RoutedEventArgs e)
        {
            Uwp_Services.SendMAsync(deviceClient).GetAwaiter();           
        } 

        private async void MPage_Loading(FrameworkElement sender, object args)
        {
            lbReceivedMessage.Items.Add(await Uwp_Services.ReceiveMAsync(deviceClient));
        }

        private async void GetMessage_Click(object sender, RoutedEventArgs e)
        {
            lbReceivedMessage.Items.Add(await Uwp_Services.ReceiveMAsync(deviceClient));
        }
    }
}
