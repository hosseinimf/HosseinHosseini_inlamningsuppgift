using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using SharedLibraries.Models;
using SharedLibraries.Services;

namespace ConsoleApp
{
    class Program
    {
        //deklaration av en string för att spara connection string till Iot device                                     
        private static readonly string connectionString = "HostName=Hossein-hosseini-win20.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=TNxoOXV6Wswyun4f+RHWLH7hxw7aWr8doIEmy9dJ3mc=";
        
        //skapa en device client för att koppla till molnet
        private static readonly DeviceClient deviceClient =
            DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);

        
        static void Main(string[] args)
        {           
            DeviceService.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceService.ReceiveMessageAsync(deviceClient).GetAwaiter();

            Console.ReadKey();
        }
    }
}
