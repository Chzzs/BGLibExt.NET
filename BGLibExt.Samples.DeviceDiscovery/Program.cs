using System;
using System.Linq;
using System.Threading.Tasks;

namespace BGLibExt.Samples.DeviceDiscovery
{
    class Program
    {
        static void Main(string[] args)
        {
            BleModuleConnection.Instance.Start("COM3");

            RunAsync().Wait();

            BleModuleConnection.Instance.Stop();
        }

        private static async Task RunAsync()
        {
            var bleDeviceDiscovery = new BleDeviceDiscovery();
            bleDeviceDiscovery.ScanResponse += (sender, args) =>
            {
                Console.WriteLine($"Device discovered, Address={args.Address}, AddressType={args.AddressType}, Rssi={args.Rssi}, PacketType={args.PacketType}, Bond={args.Bond}, ParsedData={string.Join(";", args.ParsedData.Select(x => $"{x.Type}={x.ToDebugString()}"))}");
            };

            Console.WriteLine("Start device discovery");
            bleDeviceDiscovery.StartDeviceDiscovery();

            await Task.Delay(5000);

            Console.WriteLine("Stop device discovery");
            bleDeviceDiscovery.StopDeviceDiscovery();
        }
    }
}
