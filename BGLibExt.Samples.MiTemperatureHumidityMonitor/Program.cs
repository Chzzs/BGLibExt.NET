using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.Samples.MiTemperatureHumidityMonitor
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
            Console.WriteLine("Discover Mi temperature and humidity monitor");
            var flowerCareDevice = await BleDevice.ConnectByManufacturerIdAsync(0x0000);

            Console.WriteLine("Read device status");
            var battery = await flowerCareDevice.CharacteristicsByUuid[new Guid("00002a19-0000-1000-8000-00805f9b34fb")].ReadValueAsync();
            var batteryLevel = battery[0];
            var firmware = await flowerCareDevice.CharacteristicsByUuid[new Guid("00002a26-0000-1000-8000-00805f9b34fb")].ReadValueAsync();
            var firmwareVersion = Encoding.ASCII.GetString(firmware).TrimEnd(new char[] { (char)0 });
            Console.WriteLine($"Battery level: {batteryLevel}");
            Console.WriteLine($"Firmware version: {firmwareVersion}");

            Console.WriteLine("Read device sensor data");
            var manualResetEvent = new ManualResetEvent(false);
            flowerCareDevice.CharacteristicsByUuid[new Guid("226caa55-6476-4566-7562-66734470666d")].ValueChanged += (sender, args) =>
            {
                var dataString = Encoding.ASCII.GetString(args.Value).TrimEnd(new char[] { (char)0 });
                var match = Regex.Match(dataString, @"T=([\d\.]*)\s+?H=([\d\.]*)");
                if (match.Success)
                {
                    var temperature = float.Parse(match.Groups[1].Captures[0].Value);
                    var airHumidity = float.Parse(match.Groups[2].Captures[0].Value);
                }
                manualResetEvent.Set();
            };
            await flowerCareDevice.CharacteristicsByUuid[new Guid("226caa55-6476-4566-7562-66734470666d")].WriteClientCharacteristicConfigurationAsync(BleCCCValue.NotificationsEnabled);
            manualResetEvent.WaitOne();
        }
    }
}
