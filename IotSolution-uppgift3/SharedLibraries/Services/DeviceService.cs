using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MAD = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibraries.Models;
using Microsoft.Azure.Devices;
using Message = Microsoft.Azure.Devices.Client.Message;

namespace SharedLibraries.Services
{
    public static class DeviceService
    {
        private static readonly Random rnd = new Random();

        //Device client = iot device 
        public static async Task SendMessageAsync(DeviceClient deviceClient)//*************************************************************
        {
            while (true)
            {
                var data = new TemperatureModel
                {
                    Temperature = rnd.Next(20, 30),
                    Humidity = rnd.Next(40, 50)
                };

                //konvertera väder informationen till json format
                var json = JsonConvert.SerializeObject(data);

                //konvertera json format till bytes och spara i payload
                var payload = new Message(Encoding.UTF8.GetBytes(json));
                //skicka payload till iot device
                await deviceClient.SendEventAsync(payload);
                Console.WriteLine($"Message sent: {json}");
                //delay för att skicka informationen varje minut
                await Task.Delay(60*1000);
            }
        }

        //Device client = iot device
        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)//***********************************************************
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                { 
                    continue; 
                }

                Console.WriteLine($"Message received: {Encoding.UTF8.GetString(payload.GetBytes())}");

                await deviceClient.CompleteAsync(payload);
            }
        }

        //Service client = iot Hub
        public static async Task SendMessageToDeviceAsync
            (MAD.ServiceClient serviceClient, string targetDeviceId, string message)//*****************************************************
        {
            var payload = new MAD.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);
        }
    }
}
