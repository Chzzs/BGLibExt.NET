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
        private readonly ILogger<Program> _logger;

        public Program(BleModuleConnection bleModuleConnection, BleDeviceFactory bleDeviceFactory, ILogger<Program> logger)
        {
            _bleModuleConnection = bleModuleConnection;
            _bleDeviceFactory = bleDeviceFactory;
            _logger = logger;
        }

        static void Main(string[] args)
        {
            var servicesProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddTransient<BGLib, BGLibDebug>()
                .AddSingleton<BleModuleConnection>()
                .AddTransient<Program>()
                .BuildServiceProvider();
            var program = servicesProvider.GetRequiredService<Program>();

            program.RunAsync().Wait();
        }

        private async Task RunAsync()
        {
            _bleModuleConnection.Start("COM3");

            _logger.LogInformation("Discover and connect to flower care smart monitor");
            var flowerCareSmartMonitor = await _bleDeviceFactory.ConnectByServiceUuidAsync("95FE".HexStringToByteArray());

            _logger.LogInformation("Device services and characteristics");
            foreach (var service in flowerCareSmartMonitor.Services)
            {
                _logger.LogInformation($"Service Uuid={service.Uuid}");

                foreach (var characteristic in service.Characteristics)
                {
                    _logger.LogInformation($"Characteristic Uuid={characteristic.Uuid}, Handle={characteristic.Handle}, HasCCC={characteristic.HasCCC}");
                }
            }

            _logger.LogInformation("Read device status");
            var status = await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a02-0000-1000-8000-00805f9b34fb")].ReadValueAsync();
            var batteryLevel = status[0];
            var firmwareVersion = Encoding.ASCII.GetString(status.Skip(2).ToArray()).TrimEnd(new char[] { (char)0 });
            _logger.LogInformation($"Battery level: {batteryLevel}%");
            _logger.LogInformation($"Firmware version: {firmwareVersion}");

            _logger.LogInformation("Read device sensor data");
            await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a00-0000-1000-8000-00805f9b34fb")].WriteValueAsync(new byte[] { 0xa0, 0x1f });
            var sensorData = await flowerCareSmartMonitor.CharacteristicsByUuid[new Guid("00001a01-0000-1000-8000-00805f9b34fb")].ReadValueAsync();
            var temperature = BitConverter.ToInt16(sensorData.Take(2).ToArray(), 0) / 10f;
            var lightIntensity = BitConverter.ToInt32(sensorData.Skip(3).Take(4).ToArray(), 0);
            var soilMoisture = sensorData[7];
            var soilFertility = BitConverter.ToInt16(sensorData.Skip(8).Take(2).ToArray(), 0);
            _logger.LogInformation($"Temperature: {temperature} °C");
            _logger.LogInformation($"Light intensity: {lightIntensity} lux");
            _logger.LogInformation($"Soil moisture: {soilMoisture}%");
            _logger.LogInformation($"Soil fertility: {soilFertility} µS/cm");

            _logger.LogInformation("Disconnect");
            await flowerCareSmartMonitor.DisconnectAsync();

            _bleModuleConnection.Stop();
        }
    }
}
