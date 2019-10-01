using System;
using System.Linq;
using System.Threading.Tasks;

namespace BGLibExt.Samples.DeviceDiscovery
{
    class Program
    {
        private static BleConnector _bleConnection;

        static void Main(string[] args)
        {
            _bleConnection = new BleConnector("COM3", true, 0);

            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            _bleConnection.ScanResponse += (sender, args) =>
            {
                Console.WriteLine($"Device discovered, Address={args.Address.ByteArrayToHexString()}, AddressType={args.AddressType}, Rssi={args.Rssi}, PacketType={args.PacketType}, Bond={args.Bond}, ParsedData={string.Join(";", args.ParsedData.Select(x => $"{x.Type}={x.ToDebugString()}"))}");
            };

            Console.WriteLine("Start device discovery");
            _bleConnection.StartDeviceDiscovery();

            await Task.Delay(10000);

            Console.WriteLine("Stop device discovery");
            _bleConnection.StopDeviceDiscovery();
        }
    }
}
