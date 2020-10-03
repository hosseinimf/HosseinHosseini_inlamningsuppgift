using System;
using System.Threading.Tasks;
using SharedLibrary.Services;

namespace ServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Delay(5000).Wait();
            var invokeobj = new ServiceAppServices("HostName=Hossein-hosseini-win20.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=goQ63OylYf6cWmoHmHQP6PZFKZVz73+NRb77UgwgRIw=");
            invokeobj.InvokeMethodAsync("DeviceApp", "SetIntervalMethod", "4").GetAwaiter();

            Console.ReadKey();
        }
    }
}
