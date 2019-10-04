using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGLibExt.Samples.FlowerCareSmartMonitor
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
            Console.WriteLine("Discover and connect to flower care smart monitor");
            var flowerCareSmartMonitor = _bleConnection.ConnectByServiceUuid("95FE".HexStringToByteArray());

            Console.WriteLine("Device services and characteristics");
            foreach (var service in flowerCareSmartMonitor.Services)
            {
                Console.WriteLine($"Service Uuid={service.ServiceUuid.ToBleGuid()}");

                foreach (var characteristic in service.Characteristics)
                {
                    Console.WriteLine($"Characteristic Uuid={characteristic.AttributeUuid.ToBleGuid()}, Handle={characteristic.Handle}, HasCCC={characteristic.HasCCC}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Read device status");
            var status = _bleConnection.ReadCharacteristic(new Guid("00001a02-0000-1000-8000-00805f9b34fb").ToUuidByteArray(), false);
            var batteryLevel = status[0];
            var firmwareVersion = Encoding.ASCII.GetString(status.Skip(2).ToArray()).TrimEnd(new char[] { (char)0 });
            Console.WriteLine($"Battery level: {batteryLevel}%");
            Console.WriteLine($"Firmware version: {firmwareVersion}");

            Console.WriteLine();
            Console.WriteLine("Read device sensor data");
            _bleConnection.WriteCharacteristic(new Guid("00001a00-0000-1000-8000-00805f9b34fb").ToUuidByteArray(), new byte[] { 0xa0, 0x1f });
            var sensorData = _bleConnection.ReadCharacteristic(new Guid("00001a01-0000-1000-8000-00805f9b34fb").ToUuidByteArray(), false);
            var temperature = BitConverter.ToInt16(sensorData.Take(2).ToArray(), 0) / 10f;
            var lightIntensity = BitConverter.ToInt32(sensorData.Skip(3).Take(4).ToArray(), 0);
            var soilMoisture = sensorData[7];
            var soilFertility = BitConverter.ToInt16(sensorData.Skip(8).Take(2).ToArray(), 0);
            Console.WriteLine($"Temperature: {temperature} °C");
            Console.WriteLine($"Light intensity: {lightIntensity} lux");
            Console.WriteLine($"Soil moisture: {soilMoisture}%");
            Console.WriteLine($"Soil fertility: {soilFertility} µS/cm");

            Console.WriteLine("Disconnect");
            _bleConnection.Disconnect();
        }
    }
}
