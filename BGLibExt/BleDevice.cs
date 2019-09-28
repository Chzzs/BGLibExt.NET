using BGLibExt.BleCommands;
using Bluegiga.BLE.Events.ATTClient;
using Bluegiga.BLE.Events.Connection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt
{
    public class BleDevice
    {
        private readonly byte _connectionHandle;

        public event DisconnectedEventHandler Disconnected;

        public bool IsConnected { get; private set; }
        public Dictionary<Guid, BleCharacteristic> CharacteristicsByUuid { get; private set; } = new Dictionary<Guid, BleCharacteristic>();
        public Dictionary<ushort, BleCharacteristic> CharacteristicsByHandle { get; private set; } = new Dictionary<ushort, BleCharacteristic>();
        public List<BleService> Services { get; private set; }

        private BleDevice(byte connectionHandle, List<BleService> services)
        {
            _connectionHandle = connectionHandle;

            Services = services;

            services.ForEach(service =>
            {
                service.Characteristics.ForEach(characteristic =>
                {
                    CharacteristicsByUuid.Add(characteristic.AttributeUuid.ToGuid(), characteristic);
                });
            });

            services.ForEach(service =>
            {
                service.Characteristics.ForEach(characteristic =>
                {
                    CharacteristicsByHandle.Add(characteristic.Handle, characteristic);
                });
            });

            IsConnected = true;
        }

        /// <summary>
        /// Connect to a device
        /// </summary>
        /// <param name="address">Device address</param>
        /// <param name="addressType">Device address type</param>
        /// <returns></returns>
        public static async Task<BleDevice> ConnectAsync(byte[] address, BleAddressType addressType)
        {
            var connectCommand = new BleConnectCommand();
            var connectionStatus = await connectCommand.ExecuteAsync(address, addressType);

            var findServicesCommand = new BleFindServicesCommand();
            var findCharacteristicsCommand = new BleFindCharacteristicsCommand();
            var services = await findServicesCommand.ExecuteAsync(connectionStatus.connection);
            foreach (var service in services)
            {
                var (characteristics, attributes) = await findCharacteristicsCommand.ExecuteAsync(connectionStatus.connection, service.StartHandle, service.EndHandle);
                service.Characteristics.AddRange(characteristics);
                service.Attributes.AddRange(attributes);
            }

            var bleDevice = new BleDevice(connectionStatus.connection, services);

            BleModuleConnection.Instance.BleProtocol.Lib.BLEEventATTClientAttributeValue += bleDevice.OnClientAttributeValue;
            BleModuleConnection.Instance.BleProtocol.Lib.BLEEventConnectionDisconnected += bleDevice.OnDisconnected;

            return bleDevice;
        }

        /// <summary>
        /// Connect to the first device that is being discovered by the specified manufacturer ID
        /// </summary>
        /// <param name="manufacturerId">Manufacturer ID</param>
        /// <returns></returns>
        public static async Task<BleDevice> ConnectByManufacturerIdAsync(ushort manufacturerId)
        {
            var discoverManufacturerSpecificDataCommand = new BleDiscoverManufacturerSpecificDataCommand();
            var discoverdDevice = await discoverManufacturerSpecificDataCommand.ExecuteAsync(manufacturerId);

            return await ConnectAsync(discoverdDevice.sender, (BleAddressType)discoverdDevice.address_type);
        }

        /// <summary>
        /// Connect to the first device that is being discovered by the specified service UUID
        /// </summary>
        /// <param name="serviceUuid">Service UUID</param>
        /// <returns></returns>
        public static async Task<BleDevice> ConnectByServiceUuidAsync(byte[] serviceUuid)
        {
            var discoverServiceCommand = new BleDiscoverServiceCommand();
            var discoveredDevice = await discoverServiceCommand.ExecuteAsync(serviceUuid);

            return await ConnectAsync(discoveredDevice.sender, (BleAddressType)discoveredDevice.address_type);
        }

        /// <summary>
        /// Disconnect from device
        /// </summary>
        public async Task DisconnectAsync()
        {
            var disconnectCommand = new BleDisconnectCommand();
            await disconnectCommand.ExecuteAsync(_connectionHandle);

            BleModuleConnection.Instance.BleProtocol.Lib.BLEEventATTClientAttributeValue -= OnClientAttributeValue;
            BleModuleConnection.Instance.BleProtocol.Lib.BLEEventConnectionDisconnected -= OnDisconnected;

            IsConnected = false;
        }

        private void OnClientAttributeValue(object sender, AttributeValueEventArgs e)
        {
            if (e.connection == _connectionHandle)
            {
                var characteristic = CharacteristicsByHandle[e.atthandle];
                characteristic.TriggerCharacteristicValueChanged(e.value);
            }
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            if (e.connection == _connectionHandle)
            {
                BleModuleConnection.Instance.BleProtocol.Lib.BLEEventConnectionDisconnected -= OnDisconnected;
                var disconnectThread = new Thread((parameters) =>
                {
                    var parameterArray = (object[])parameters;
                    Disconnected?.Invoke(parameterArray[0], (DisconnectedEventArgs)parameterArray[1]);
                });
                disconnectThread.Start(new object[] { this, e });

                IsConnected = false;
            }
        }
    }
}
