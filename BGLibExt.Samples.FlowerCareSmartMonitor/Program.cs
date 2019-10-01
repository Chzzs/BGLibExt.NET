using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGLibExt.Samples.FlowerCareSmartMonitor
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
            Console.WriteLine("Discover and connect to flower care smart monitor");
            var flowerCareSmartMonitor = await BleDevice.ConnectByServiceUuidAsync("95FE".HexStringToByteArray());

            Console.WriteLine("Device services and characteristics");
            foreach (var service in flowerCareSmartMonitor.Services)
            {
                Console.WriteLine($"Service Uuid={service.Uuid}");

                foreach (var characteristic in service.Characteristics)
                {
                    Console.WriteLine($"Characteristic Uuid={characteristic.Uuid}, Handle={characteristic.Handle}, HasCCC={characteristic.HasCCC}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Read device status");
            var status = await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a02-0000-1000-8000-00805f9b34fb")].ReadValueAsync(true);
            var batteryLevel = status[0];
            var firmwareVersion = Encoding.ASCII.GetString(status.Skip(2).ToArray()).TrimEnd(new char[] { (char)0 });
            Console.WriteLine($"Battery level: {batteryLevel}%");
            Console.WriteLine($"Firmware version: {firmwareVersion}");

            Console.WriteLine();
            Console.WriteLine("Read device sensor data");
            await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a00-0000-1000-8000-00805f9b34fb")].WriteValueAsync(new byte[] { 0xa0, 0x1f });
            var sensorData = await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a01-0000-1000-8000-00805f9b34fb")].ReadValueAsync(true);
            var temperature = BitConverter.ToInt16(sensorData.Take(2).ToArray(), 0) / 10f;
            var lightIntensity = BitConverter.ToInt32(sensorData.Skip(3).Take(4).ToArray(), 0);
            var soilMoisture = sensorData[7];
            var soilFertility = BitConverter.ToInt16(sensorData.Skip(8).Take(2).ToArray(), 0);
            Console.WriteLine($"Temperature: {temperature} °C");
            Console.WriteLine($"Light intensity: {lightIntensity} lux");
            Console.WriteLine($"Soil moisture: {soilMoisture}%");
            Console.WriteLine($"Soil fertility: {soilFertility} µS/cm");

            Console.WriteLine("Disconnect");
            await flowerCareSmartMonitor.DisconnectAsync();
        }
    }
}
