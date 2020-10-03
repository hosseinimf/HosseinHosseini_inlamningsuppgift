using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace DeviceApp.Services
{
    public class DeviceServicesH
    {
        private int _interval = 8;
        private static Random rnd = new Random();
        private DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=Hossein-hosseini-win20.azure-devices.net;DeviceId=DeviceApp;SharedAccessKey=gdqLa55CvXeTaiYCRKSV6PGefSjXwNIjPgORo73Gql0=", TransportType.Mqtt);


        public bool ChangeInterval (int interval)
        {
            try
            {
                _interval = interval;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SendAndSet ()
        {
            deviceClient.SetMethodHandlerAsync("SetIntervalMethod", SetTelemetryInterval2, null).GetAwaiter();
            SendMessageAsync(deviceClient).GetAwaiter();
        }

        public async Task SendMessageAsync(DeviceClient deviceC)
        {
            while (true)
            {
                double temp = 10 + rnd.NextDouble() * 15;
                double hum = 40 + rnd.NextDouble() * 20;

                var data = new
                {
                    temperature = temp,
                    humidity = hum
                };

                var json = JsonConvert.SerializeObject(data);
                var payload = new Message(Encoding.UTF8.GetBytes(json));
                payload.Properties.Add("temperatureAlert", (temp > 30) ? "true" : "false");

                await deviceC.SendEventAsync(payload);
                Console.WriteLine($"Message sent: {json}");
                Console.WriteLine($"Interval is {_interval} ");

                await Task.Delay(_interval * 1000);
            }
        }


        public Task<MethodResponse> SetTelemetryInterval2(MethodRequest request, object userContext)
        {
            var payload = Encoding.UTF8.GetString(request.Data).Replace("\"", "");
            Console.WriteLine(payload);

            if (Int32.TryParse(payload, out int sentInterval))
            {
                ChangeInterval(sentInterval);
                Console.WriteLine($"Interval set to {sentInterval} seconds.");
                
                string json = "{\"result\": \"Executed direct method: " + request.Name + "\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 200));
            }
            else
            {
                string json = "{\"result\": \"Method not implemented \"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 501));
            }
        }
    }
}
