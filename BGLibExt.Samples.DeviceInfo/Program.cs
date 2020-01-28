using Bluegiga;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.Samples.DeviceInfo
{
    internal class Program
    {
        private readonly BleDeviceDiscovery _bleDeviceDiscovery;
        private readonly BleDeviceManager _bleDeviceManager;
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly ILogger<Program> _logger;
        private ManualResetEvent _discoveryTimeout = new ManualResetEvent(false);

        public Program(BleModuleConnection bleModuleConnection, BleDeviceDiscovery bleDeviceDiscovery, BleDeviceManager bleDeviceManager, ILogger<Program> logger = null)
        {
            _bleModuleConnection = bleModuleConnection;
            _bleDeviceDiscovery = bleDeviceDiscovery;
            _bleDeviceManager = bleDeviceManager;
            _logger = logger;
        }

        private static void Main(string[] args)
        {
            var servicesProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddSingleton<BGLib, BGLibDebug>()
                .AddSingleton<BleModuleConnection>()
                .AddTransient<BleDeviceDiscovery>()
                .AddTransient<BleDeviceManager>()
                .AddTransient<Program>()
                .BuildServiceProvider();
            var program = servicesProvider.GetRequiredService<Program>();

            program.RunAsync().Wait();
        }

        private async Task RunAsync()
        {
            _bleModuleConnection.Start("COM3");

            byte[] deviceAddress = null;
            BleAddressType deviceAddressType = BleAddressType.Public;

            _bleDeviceDiscovery.ScanResponse += (sender, args) =>
            {
                // Filter advertisement data by the complete local name
                if (args.ParsedData.Any(x => x.Type == BleAdvertisingDataType.CompleteLocalName && x.ToAsciiString() == "DEVICE-NAME"))
                {
                    _logger.LogInformation("Device found");

                    deviceAddress = args.Address;
                    deviceAddressType = args.AddressType;

                    _bleDeviceDiscovery.StopDeviceDiscovery();

                    _discoveryTimeout.Set();
                }
            };

            _bleDeviceDiscovery.StartDeviceDiscovery();

            try
            {
                _discoveryTimeout.WaitOne(10000);

                // Connecting to a device will log the discovered services and characteristics
                var device = await _bleDeviceManager.ConnectAsync(deviceAddress, deviceAddressType);
            }
            catch
            {
                _bleDeviceDiscovery.StopDeviceDiscovery();

                _logger.LogError("Device coulnd not be found");
            }

            _bleModuleConnection.Stop();
        }
    }
}
