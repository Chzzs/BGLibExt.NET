using BGLibExt.BleCommands;
using Bluegiga;
using Bluegiga.BLE.Events.ATTClient;
using Bluegiga.BLE.Events.Connection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt
{
    public class BleDevice
    {
        private readonly BGLib _bgLib;
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly byte _connectionHandle;
        private readonly ILogger _logger;

        public event DisconnectedEventHandler Disconnected;

        public Dictionary<ushort, BleCharacteristic> CharacteristicsByHandle { get; private set; } = new Dictionary<ushort, BleCharacteristic>();
        public Dictionary<Guid, BleCharacteristic> CharacteristicsByUuid { get; private set; } = new Dictionary<Guid, BleCharacteristic>();
        public bool IsConnected { get; private set; }
        public List<BleService> Services { get; private set; }

        internal BleDevice(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger, byte connectionHandle, List<BleService> services)
        {
            _bgLib = bgLib;
            _bleModuleConnection = bleModuleConnection;
            _logger = logger;
            _connectionHandle = connectionHandle;

            Services = services;

            services.ForEach(service =>
            {
                service.Characteristics.ForEach(characteristic =>
                {
                    CharacteristicsByUuid.Add(characteristic.Uuid, characteristic);
                });
            });

            services.ForEach(service =>
            {
                service.Characteristics.ForEach(characteristic =>
                {
                    CharacteristicsByHandle.Add(characteristic.Handle, characteristic);
                });
            });

            _bgLib.BLEEventATTClientAttributeValue += OnClientAttributeValue;
            _bgLib.BLEEventConnectionDisconnected += OnDisconnected;

            IsConnected = true;
        }

        /// <summary>
        /// Disconnect from device
        /// </summary>
        public async Task DisconnectAsync()
        {
            _bgLib.BLEEventATTClientAttributeValue -= OnClientAttributeValue;
            _bgLib.BLEEventConnectionDisconnected -= OnDisconnected;

            var disconnectCommand = new BleDisconnectCommand(_bgLib, _bleModuleConnection, _logger);
            await disconnectCommand.ExecuteAsync(_connectionHandle);

            IsConnected = false;
        }

        private void OnClientAttributeValue(object sender, AttributeValueEventArgs e)
        {
            if (e.connection == _connectionHandle && CharacteristicsByHandle.ContainsKey(e.atthandle))
            {
                var characteristic = CharacteristicsByHandle[e.atthandle];
                characteristic.TriggerCharacteristicValueChanged(e.value);
            }
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            if (e.connection == _connectionHandle)
            {
                _bgLib.BLEEventConnectionDisconnected -= OnDisconnected;
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
