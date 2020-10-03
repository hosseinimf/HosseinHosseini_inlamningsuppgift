using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibraryAzure.Models;
using SharedLibraryAzure.Services;
using Microsoft.Azure.Devices.Client;
using MAD2 = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices;

namespace AzureFunction_upp_4
{
    public static class SendMessageToDevice_upp4
    {
        private static readonly ServiceClient serviceClient =
            ServiceClient.CreateFromConnectionString("HostName=Hossein-hosseini-win20.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=goQ63OylYf6cWmoHmHQP6PZFKZVz73+NRb77UgwgRIw=");


        [FunctionName("SendMessageToDevice_upp4")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            string targetdeviceid = req.Query["targetdeviceid"];
            string message = req.Query["message"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<BodyMessageModel>(requestBody);

            targetdeviceid = targetdeviceid ?? data?.TargetDeviceId;
            message = message ?? data?.Message;
            
            return new OkResult();
        }
    }
}
