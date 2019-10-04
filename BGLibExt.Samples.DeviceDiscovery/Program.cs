using Bluegiga;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BGLibExt.Samples.DeviceDiscovery
{
    class Program
    {
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly BleDeviceDiscovery _bleDeviceDiscovery;
        private readonly ILogger<Program> _logger;

        public Program(BleModuleConnection bleModuleConnection, BleDeviceDiscovery bleDeviceDiscovery, ILogger<Program> logger = null)
        {
            _bleModuleConnection = bleModuleConnection;
            _bleDeviceDiscovery = bleDeviceDiscovery;
            _logger = logger;
        }

        static void Main(string[] args)
        {
            var servicesProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddSingleton<BGLib, BGLibDebug>()
                .AddSingleton<BleModuleConnection>()
                .AddTransient<BleDeviceDiscovery>()
                .AddTransient<Program>()
                .BuildServiceProvider();
            var program = servicesProvider.GetRequiredService<Program>();

            program.RunAsync().Wait();
        }

        private async Task RunAsync()
        {
            _bleModuleConnection.Start("COM3");

            _bleDeviceDiscovery.ScanResponse += (sender, args) =>
            {
                _logger?.LogInformation($"Device discovered, Address={args.Address.ToHexString()}, Data={args.Data.ToHexString()}, ParsedData={string.Join(";", args.ParsedData.Select(x => $"{x.Type}={x.ToDebugString()}"))}");
            };

            _bleDeviceDiscovery.StartDeviceDiscovery();

            await Task.Delay(10000);

            _bleDeviceDiscovery.StopDeviceDiscovery();

            _bleModuleConnection.Stop();
        }
    }
}
