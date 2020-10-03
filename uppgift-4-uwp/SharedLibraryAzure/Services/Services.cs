using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Devices.Client;
using MAD2 = Microsoft.Azure.Devices;
using System.Threading.Tasks;
using SharedLibraryAzure.Models;
using Newtonsoft.Json;


namespace SharedLibraryAzure.Services
{
    public static class Services
    {
        private static readonly Random rnd = new Random();

        public static async Task SendMAsync(DeviceClient deviceClient) 
        {
            var data = new TemperatureModel
            {
                Temperature = rnd.Next(15, 25),
                Humidity = rnd.Next(35, 50)
            };

            var json = JsonConvert.SerializeObject(data);
            var payload = new Message(Encoding.UTF8.GetBytes(json));

            await deviceClient.SendEventAsync(payload);
            Console.WriteLine($"Message sent: {json}");

            await Task.Delay(2 * 1000);
        } 
        
        public static async Task SendMessageToDeviceAsync
            (MAD2.ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new MAD2.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);
        }
    }
}
