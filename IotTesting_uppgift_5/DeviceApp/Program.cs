using System;
using DeviceApp.Services;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;


namespace DeviceApp
{
    class Program
    {
        //private static int telemetryInterval = 15;

        static void Main(string[] args)
        {           
            var device = new DeviceServicesH();            
            device.SendAndSet();            
            Console.ReadKey();           
        }
    }
}
