using Bluegiga;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BGLibExt.Samples.MiTemperatureHumidityMonitor
{
    internal class Program
    {
        private readonly BleDeviceManager _bleDeviceManager;
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly ILogger<Program> _logger;

        public Program(BleModuleConnection bleModuleConnection, BleDeviceManager bleDeviceManager, ILogger<Program> logger = null)
        {
            _bleModuleConnection = bleModuleConnection;
            _bleDeviceManager = bleDeviceManager;
            _logger = logger;
        }

        private static void Main(string[] args)
        {
            var servicesProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddSingleton<BGLib, BGLibDebug>()
                .AddSingleton<BleModuleConnection>()
                .AddTransient<BleDeviceManager>()
                .AddTransient<Program>()
                .BuildServiceProvider();
            var program = servicesProvider.GetRequiredService<Program>();

            program.RunAsync().Wait();
        }

        private async Task RunAsync()
        {
            _bleModuleConnection.Start("COM3");

            _logger?.LogInformation("Discover and connect to device");
            var miTemperatureHumidityMonitor = await _bleDeviceManager.ConnectByServiceUuidAsync("0F180A18".HexStringToByteArray());

            _logger?.LogInformation("Read device status");
            var battery = await miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("00002a19-0000-1000-8000-00805f9b34fb")].ReadValueAsync();
            var batteryLevel = battery[0];
            var firmware = await miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("00002a26-0000-1000-8000-00805f9b34fb")].ReadValueAsync();
            var firmwareVersion = Encoding.ASCII.GetString(firmware).TrimEnd(new char[] { (char)0 });
            _logger?.LogInformation($"Battery level: {batteryLevel}%");
            _logger?.LogInformation($"Firmware version: {firmwareVersion}");

            _logger?.LogInformation("Read device sensor data");
            miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("226caa55-6476-4566-7562-66734470666d")].ValueChanged += (sender, args) =>
            {
                var dataString = Encoding.ASCII.GetString(args.Value).TrimEnd(new char[] { (char)0 });
                var match = Regex.Match(dataString, @"T=([\d\.]*)\s+?H=([\d\.]*)");
                if (match.Success)
                {
                    var temperature = float.Parse(match.Groups[1].Captures[0].Value);
                    var airHumidity = float.Parse(match.Groups[2].Captures[0].Value);
                    _logger?.LogInformation($"Temperature: {temperature} °C");
                    _logger?.LogInformation($"Air humidity: {airHumidity}%");
                }
            };
            _logger?.LogInformation("Enable notifications");
            await miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("226caa55-6476-4566-7562-66734470666d")].WriteCccAsync(BleCccValue.NotificationsEnabled);
            var ccc = await miTemperatureHumidityMonitor.CharacteristicsByUuid[new Guid("226caa55-6476-4566-7562-66734470666d")].ReadCccAsync();

            await Task.Delay(5000);

            await miTemperatureHumidityMonitor.DisconnectAsync();

            _bleModuleConnection.Stop();
        }
    }
}
