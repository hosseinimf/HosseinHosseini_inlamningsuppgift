using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Uppgift_1_WorkerService
{//Hossein Hosseini
    public class Worker : BackgroundService
    {
        
        private readonly ILogger<Worker> _logger;
        public float temp;
        //Random random = new Random();
        HttpClient client = new HttpClient();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client.BaseAddress = new Uri("http://api.weatherapi.com");
            
            _logger.LogInformation("The service has been started.");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has been stopped.");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            
            while (!stoppingToken.IsCancellationRequested)
            {
                //try catch för att hantera eventuella fel.
                try
                { //väder information laddas in i response variabeln

                var response = await client.GetAsync($"/v1/current.json?key=2b464a1fd6ff41b492491028202109&q=Stockholm");
                var stringResult = await response.Content.ReadAsStringAsync();
                    //konvertera json format till en objekt.
                var obj = JsonConvert.DeserializeObject<dynamic>(stringResult);
                    //för att få bara en viss information utav objektet. till exempel temperature i centigrad format.
                temp = obj.current.temp_c;

                
                if ((temp < 18) & (temp >7) )
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation($"**************The temperature in Stockholm is {temp} c. It s below 18 c so it is a little cold.");
                    await Task.Delay(60000, stoppingToken);
                }
                else if (temp <= 7)
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);                   
                    _logger.LogInformation($"**************The temperature in Stockholm is {temp} c. It s below 7 c so it is very cold.");
                    await Task.Delay(60000, stoppingToken);
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation($"**************The temperature in Stockholm is {temp} c.");
                    await Task.Delay(60000, stoppingToken);
                }
                }
                catch 
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _logger.LogInformation($"Worker could not open the API. Try again.");
                    await Task.Delay(60000, stoppingToken);
                }


            }
        }
    }
}
