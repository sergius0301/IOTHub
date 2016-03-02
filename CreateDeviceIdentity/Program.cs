using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace CreateDeviceIdentity
{
    class Program
    {
        static RegistryManager registryManager;
        static string connectionString = "HostName=RedAlertHubArduino.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Cj/S1iTIyUcSgDv0fZXcNZ7RlXC8XZRT8EmeZHp+x4c=";
        
        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }


        private async static Task AddDeviceAsync()
        {
            bool exceptionFlag = false;
            string deviceId = "myFirstDevice";

            Device device=null;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                exceptionFlag = true;
            }

            if (exceptionFlag == true)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
         
        }
    }
}
