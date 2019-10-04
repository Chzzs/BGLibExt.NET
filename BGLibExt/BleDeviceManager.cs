using BGLibExt.BleCommands;
using Bluegiga;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BGLibExt
{
    public class BleDeviceManager
    {
        private readonly BGLib _bgLib;
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly ILogger<BleDevice> _logger;

        public BleDeviceManager(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger<BleDevice> logger = null)
        {
            _bgLib = bgLib;
            _bleModuleConnection = bleModuleConnection;
            _logger = logger;
        }

        /// <summary>
        /// Connect to a device
        /// </summary>
        /// <param name="address">Device address</param>
        /// <param name="addressType">Device address type</param>
        /// <returns></returns>
        public async Task<BleDevice> ConnectAsync(byte[] address, BleAddressType addressType)
        {
            var connectCommand = new BleConnectCommand(_bgLib, _bleModuleConnection, _logger);
            var connectionStatus = await connectCommand.ExecuteAsync(address, addressType);

            var findServicesCommand = new BleFindServicesCommand(_bgLib, _bleModuleConnection, _logger);
            var findCharacteristicsCommand = new BleFindCharacteristicsCommand(_bgLib, _bleModuleConnection, _logger);
            var services = await findServicesCommand.ExecuteAsync(connectionStatus.connection);
            foreach (var service in services)
            {
                _logger?.LogDebug($"Service found Uuid={service.Uuid}");

                var (characteristics, attributes) = await findCharacteristicsCommand.ExecuteAsync(connectionStatus.connection, service.StartHandle, service.EndHandle);
                service.Characteristics.AddRange(characteristics);
                service.Attributes.AddRange(attributes);

                foreach (var characteristic in service.Characteristics)
                {
                    _logger?.LogDebug($"Characteristic found, Uuid={characteristic.Uuid}, Handle={characteristic.Handle}, HasCcc={characteristic.HasCcc}");
                }
            }

            var bleDevice = new BleDevice(_bgLib, _bleModuleConnection, _logger, connectionStatus.connection, services);
            return bleDevice;
        }

        /// <summary>
        /// Connect to the first device that is being discovered by the specified manufacturer ID
        /// </summary>
        /// <param name="manufacturerId">Manufacturer ID</param>
        /// <returns></returns>
        public async Task<BleDevice> ConnectByManufacturerIdAsync(ushort manufacturerId)
        {
            var discoverManufacturerSpecificDataCommand = new BleDiscoverManufacturerSpecificDataCommand(_bgLib, _bleModuleConnection, _logger);
            var discoveredDevice = await discoverManufacturerSpecificDataCommand.ExecuteAsync(manufacturerId);

            return await ConnectAsync(discoveredDevice.sender, (BleAddressType)discoveredDevice.address_type);
        }

        /// <summary>
        /// Connect to the first device that is being discovered by the specified service UUID
        /// </summary>
        /// <param name="serviceUuid">Service UUID</param>
        /// <returns></returns>
        public async Task<BleDevice> ConnectByServiceUuidAsync(byte[] serviceUuid)
        {
            var discoverServiceCommand = new BleDiscoverServiceCommand(_bgLib, _bleModuleConnection, _logger);
            var discoveredDevice = await discoverServiceCommand.ExecuteAsync(serviceUuid);

            return await ConnectAsync(discoveredDevice.sender, (BleAddressType)discoveredDevice.address_type);
        }
    }
}
