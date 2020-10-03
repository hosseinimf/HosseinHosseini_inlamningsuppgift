using System;
using System.Collections.Generic;
using System.Text;
using MAD = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace SharedLibrary.Services
{
    public class ServiceAppServices
    {
        private MAD.ServiceClient serviceClient;

        public ServiceAppServices(string connectionString)
        {
            serviceClient = MAD.ServiceClient.CreateFromConnectionString(connectionString);
        }


        public async Task<CloudToDeviceMethodResult> InvokeMethodAsync(string deviceId, string methodName, string payload)
        {
            var methodInvocation = new MAD.CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson(payload);

            var response = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);
            Console.WriteLine($"Response Status: {response.Status}");
            Console.WriteLine(response.GetPayloadAsJson());
            return response;
        }
    }
}
