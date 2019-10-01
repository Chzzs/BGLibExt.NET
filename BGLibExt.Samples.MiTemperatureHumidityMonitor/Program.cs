using System;
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
            BleModuleConnection.Instance.Start("COM3", true, 0);

            RunAsync().Wait();

            BleModuleConnection.Instance.Stop();
        }

        private static async Task RunAsync()
        {
            Console.WriteLine("Discover and connecto to Mi temperature and humidity monitor");
            var miTemperatureHumidityMonitor = await BleDevice.ConnectByServiceUuidAsync("0F180A18".HexStringToByteArray());

            Console.WriteLine("Device services and characteristics");
            foreach(var service in miTemperatureHumidityMonitor.Services)
            {
                Console.WriteLine($"Service Uuid={service.Uuid}");

                foreach (var characteristic in service.Characteristics)
                {
                    Console.WriteLine($"Characteristic Uuid={characteristic.Uuid}, Handle={characteristic.Handle}, HasCCC={characteristic.HasCCC}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Read device status");
            var battery = await miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("00002a19-0000-1000-8000-00805f9b34fb")].ReadValueAsync(true);
            var batteryLevel = battery[0];
            var firmware = await miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("00002a26-0000-1000-8000-00805f9b34fb")].ReadValueAsync(true);
            var firmwareVersion = Encoding.ASCII.GetString(firmware).TrimEnd(new char[] { (char)0 });
            Console.WriteLine($"Battery level: {batteryLevel}");
            Console.WriteLine($"Firmware version: {firmwareVersion}");

            Console.WriteLine();
            Console.WriteLine("Read device sensor data");
            var manualResetEvent = new ManualResetEvent(false);
            miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("226caa55-6476-4566-7562-66734470666d")].ValueChanged += (sender, args) =>
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
            await miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("226caa55-6476-4566-7562-66734470666d")].WriteClientCharacteristicConfigurationAsync(BleCCCValue.NotificationsEnabled);
            manualResetEvent.WaitOne();

            Console.WriteLine("Disconnect");
            await miTemperatureHumidityMonitor.DisconnectAsync();
        }
    }
}
