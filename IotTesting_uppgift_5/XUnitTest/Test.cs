using System;
using Xunit;
using Microsoft.Azure.Devices.Client;
using MAD = Microsoft.Azure.Devices;
using SharedLibrary.Services;
using DeviceApp.Services;
using Microsoft.Azure.Devices;

namespace XUnitTest
{
    public class Test
    {
        //private MAD.ServiceClient serviceClientTest = MAD.ServiceClient.CreateFromConnectionString("HostName=Hossein-hosseini-win20.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=goQ63OylYf6cWmoHmHQP6PZFKZVz73+NRb77UgwgRIw=");

        [Theory]
        [InlineData("DeviceApp", "SetIntervalMethod", "15", "200")]   // Det ska ge grön resultat 
        [InlineData("DeviceApp", "SetIntervalMethod", "18", "200")]   // Det ska ge grön resultat 
        [InlineData("DeviceApp", "GetInterval", "20", "501")]         // Det ska ge grön resultat för att det inte finns GetInterval metod. 
        [InlineData("DeviceApp", "get", "17", "501")]                 // Det ska ge grön resultat för att status måste vara 501 (get metod finns inte)


        public void Test1(string targetDevice, string methodName, string payload, string expected)
        {

            var service = new ServiceAppServices("HostName=Hossein-hosseini-win20.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=goQ63OylYf6cWmoHmHQP6PZFKZVz73+NRb77UgwgRIw=");
            var response = service.InvokeMethodAsync(targetDevice, methodName, payload );

            Assert.Equal(expected, response.Result.Status.ToString());

        }
    }
}
 