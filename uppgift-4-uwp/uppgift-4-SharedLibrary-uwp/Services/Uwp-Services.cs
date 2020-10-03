using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using uppgift_4_SharedLibrary_uwp.Models;
using MAD2 = Microsoft.Azure.Devices;

namespace uppgift_4_SharedLibrary_uwp.Services
{
    public static class Uwp_Services
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
                await Task.Delay(2*1000);           
        } 

        public static async Task<string> ReceiveMAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                {
                    continue;
                }
                
                string receivedMessage =  $"Message received: {Encoding.UTF8.GetString(payload.GetBytes())}";
                
                await deviceClient.CompleteAsync(payload);
                return receivedMessage;
            }
        }
        
        public static async Task SendMessageToDeviceAsync 
            (MAD2.ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new MAD2.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);
        }
    }
}
