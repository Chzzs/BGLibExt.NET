using Bluegiga;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGLibExt.Samples.FlowerCareSmartMonitor
{
    class Program
    {
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly BleDeviceFactory _bleDeviceFactory;

        public Program(BleModuleConnection bleModuleConnection, BleDeviceFactory bleDeviceFactory)
        {
            _bleModuleConnection = bleModuleConnection;
            _bleDeviceFactory = bleDeviceFactory;
        }

        static void Main(string[] args)
        {
            var servicesProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Trace))
                .AddSingleton<BGLib, BGLibDebug>()
                .AddSingleton<BleModuleConnection>()
                .AddTransient<BleDeviceFactory>()
                .AddTransient<Program>()
                .BuildServiceProvider();
            var program = servicesProvider.GetRequiredService<Program>();

            program.RunAsync().Wait();
        }

        private async Task RunAsync()
        {
            _bleModuleConnection.Start("COM3");

            Console.WriteLine("Discover and connect to device");
            var flowerCareSmartMonitor = await _bleDeviceFactory.ConnectByServiceUuidAsync("95FE".HexStringToByteArray());

            Console.WriteLine("Read device status");
            var status = await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a02-0000-1000-8000-00805f9b34fb")].ReadValueAsync();
            var batteryLevel = status[0];
            var firmwareVersion = Encoding.ASCII.GetString(status.Skip(2).ToArray()).TrimEnd(new char[] { (char)0 });
            Console.WriteLine($"Battery level: {batteryLevel}%");
            Console.WriteLine($"Firmware version: {firmwareVersion}");

            Console.WriteLine("Read device sensor data");
            await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a00-0000-1000-8000-00805f9b34fb")].WriteValueAsync(new byte[] { 0xa0, 0x1f });
            var sensorData = await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a01-0000-1000-8000-00805f9b34fb")].ReadValueAsync();
            var temperature = BitConverter.ToInt16(sensorData.Take(2).ToArray(), 0) / 10f;
            var lightIntensity = BitConverter.ToInt32(sensorData.Skip(3).Take(4).ToArray(), 0);
            var soilMoisture = sensorData[7];
            var soilFertility = BitConverter.ToInt16(sensorData.Skip(8).Take(2).ToArray(), 0);
            Console.WriteLine($"Temperature: {temperature} °C");
            Console.WriteLine($"Light intensity: {lightIntensity} lux");
            Console.WriteLine($"Soil moisture: {soilMoisture}%");
            Console.WriteLine($"Soil fertility: {soilFertility} µS/cm");

            await flowerCareSmartMonitor.DisconnectAsync();

            await Task.Delay(1000);

            _bleModuleConnection.Stop();
        }
    }
}
