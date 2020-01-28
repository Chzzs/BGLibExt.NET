using Bluegiga;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace BGLibExt.Samples.DeviceDiscovery
{
    internal class Program
    {
        private readonly BleDeviceDiscovery _bleDeviceDiscovery;
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly ILogger<Program> _logger;

        public Program(BleModuleConnection bleModuleConnection, BleDeviceDiscovery bleDeviceDiscovery, ILogger<Program> logger = null)
        {
            _bleModuleConnection = bleModuleConnection;
            _bleDeviceDiscovery = bleDeviceDiscovery;
            _logger = logger;
        }

        private static void Main(string[] args)
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
                // Filter advertisement data by an advertised service uuid
                if (args.ParsedData.Any(x => x.Type == BleAdvertisingDataType.CompleteListof128BitServiceClassUUIDs && x.ToGuid() == new System.Guid("0a5d8ff1-67f2-4a14-b6b7-4e3baa285f12")))
            {
                    _logger?.LogInformation($"Device discovered, Address={args.Address.Reverse().ToArray().ToHexString(":")}, Data={args.Data.ToHexString()}, ParsedData={string.Join(";", args.ParsedData.Select(x => $"{x.Type}={x.ToDebugString()}"))}");
                }
            };

            _bleDeviceDiscovery.StartDeviceDiscovery();

            await Task.Delay(10000);

            _bleDeviceDiscovery.StopDeviceDiscovery();

            _bleModuleConnection.Stop();
        }
    }
}
