using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BGLibExt.Samples.MiTemperatureHumidityMonitor
{
    class Program
    {
        private static BleConnector _bleConnection;

        static void Main(string[] args)
        {
            _bleConnection = new BleConnector("COM3", true, 0);

            Run();
        }

        private static void Run()
        {
            Console.WriteLine("Discover and connecto to Mi temperature and humidity monitor");
            var miTemperatureHumidityMonitor = _bleConnection.ConnectByServiceUuid("0F180A18".HexStringToByteArray());

            Console.WriteLine("Device services and characteristics");
            foreach (var service in miTemperatureHumidityMonitor.Services)
            {
                Console.WriteLine($"Service Uuid={service.ServiceUuid.ToBleGuid()}");

                foreach (var characteristic in service.Characteristics)
                {
                    Console.WriteLine($"Characteristic Uuid={characteristic.AttributeUuid.ToBleGuid()}, Handle={characteristic.Handle}, HasCCC={characteristic.HasCCC}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Read device status");
            var battery = _bleConnection.ReadCharacteristic(new Guid("00002a19-0000-1000-8000-00805f9b34fb").ToUuidByteArray(), false);
            var batteryLevel = battery[0];
            var firmware = _bleConnection.ReadCharacteristic(new Guid("00002a26-0000-1000-8000-00805f9b34fb").ToUuidByteArray(), false);
            var firmwareVersion = Encoding.ASCII.GetString(firmware).TrimEnd(new char[] { (char)0 });
            Console.WriteLine($"Battery level: {batteryLevel}%");
            Console.WriteLine($"Firmware version: {firmwareVersion}");

            Console.WriteLine();
            Console.WriteLine("Read device sensor data");
            _bleConnection.CharacteristicValueChanged += (sender, args) =>
            {
                if (args.CharacteristicUuid.SequenceEqual(new Guid("226caa55-6476-4566-7562-66734470666d").ToUuidByteArray()))
                {
                    var dataString = Encoding.ASCII.GetString(args.Value).TrimEnd(new char[] { (char)0 });
                    var match = Regex.Match(dataString, @"T=([\d\.]*)\s+?H=([\d\.]*)");
                    if (match.Success)
                    {
                        var temperature = float.Parse(match.Groups[1].Captures[0].Value);
                        var airHumidity = float.Parse(match.Groups[2].Captures[0].Value);
                        Console.WriteLine($"Temperature: {temperature} °C");
                        Console.WriteLine($"Air humidity: {airHumidity}%");
                    }
                }
            };
            Console.WriteLine("Enable notifications");
            _bleConnection.WriteClientCharacteristicConfiguration(new Guid("226caa55-6476-4566-7562-66734470666d").ToUuidByteArray(), BleCCCValue.NotificationsEnabled);
            var ccc = _bleConnection.ReadClientCharacteristicConfiguration(new Guid("226caa55-6476-4566-7562-66734470666d").ToUuidByteArray());

            Task.Delay(5000).Wait();

            Console.WriteLine("Disconnect");
            _bleConnection.Disconnect();
        }
    }
}
