using BGLibExt.BleBlocks;
using Bluegiga.BLE.Events.ATTClient;
using Bluegiga.BLE.Events.Connection;
using Bluegiga.BLE.Events.GAP;
using System;
using System.IO.Ports;
using System.Threading;

namespace BGLibExt
{
    public class BleConnector
    {
        private readonly bool _isDebugMode;
        private readonly string _portName;
        private readonly int _portThreadSleep;
        private BlePeripheralMap _blePeripheralMap;
        private BleProtocol _bleProtocol;
        private byte _connectionHandle;
        private ReceiveFromPeripheralThread _receiveFromPeripheralThread;
        private SerialPort _serialPort;

        public event CharacteristicValueChangedEventHandler CharacteristicValueChanged;
        public event DisconnectedEventHandler Disconnected;
        public event ScanResponseReceivedEventHandler ScanResponse;

        public delegate void CharacteristicValueChangedEventHandler(object sender, BleCharacteristicValueChangedEventArgs e);

        public delegate void ScanResponseReceivedEventHandler(object sender, BleScanResponseReceivedEventArgs e);

        public bool IsConnected { get; private set; }

        /// <summary>
        /// Create a BleConnector instance
        /// </summary>
        /// <param name="portName">Serial port name</param>
        public BleConnector(string portName)
            : this(portName, false, 0)
        {
        }

        /// <summary>
        /// Create a BleConnector instance
        /// </summary>
        /// <param name="portName">Serial port name</param>
        /// <param name="isDebugMode">Print debug information to console</param>
        /// <param name="portThreadSleep">Sleep duration while reading serial port data</param>
        public BleConnector(string portName, bool isDebugMode, int portThreadSleep)
        {
            _portName = portName;
            _isDebugMode = isDebugMode;
            _portThreadSleep = portThreadSleep;
        }

        /// <summary>
        /// Connect to a device
        /// </summary>
        /// <param name="address">Device address</param>
        /// <param name="addressType">Device address type</param>
        /// <returns></returns>
        public BlePeripheralMap Connect(byte[] address, BleAddressType addressType)
        {
            Init();

            return ConnectInternal(address, addressType);
        }

        /// <summary>
        /// Connect to the first device that is being discovered by the specified manufacturer ID
        /// </summary>
        /// <param name="manufacturerId">Manufacturer ID</param>
        /// <returns></returns>
        public BlePeripheralMap ConnectByManufacturerId(ushort manufacturerId)
        {
            Init();

            var discoverBlock = new BleDiscoverManufacturerSpecificData(_bleProtocol, _serialPort, manufacturerId);
            var found = discoverBlock.Execute();

            return ConnectInternal(found.sender, (BleAddressType)found.address_type);
        }

        /// <summary>
        /// Connect to the first device that is being discovered by the specified  service UUID
        /// </summary>
        /// <param name="serviceUuid">Service UUID</param>
        /// <returns></returns>
        public BlePeripheralMap ConnectByServiceUuid(byte[] serviceUuid)
        {
            Init();

            var discoverBlock = new BleDiscoverService(_bleProtocol, _serialPort, serviceUuid);
            var found = discoverBlock.Execute();

            return ConnectInternal(found.sender, (BleAddressType)found.address_type);
        }

        private BlePeripheralMap ConnectInternal(byte[] address, BleAddressType addressType)
        {
            var connectBlock = new BleConnectToService(_bleProtocol, _serialPort, address, addressType);
            _connectionHandle = connectBlock.Execute().connection;

            var infoBlock = new BleFindServicesAndCharacteristics(_bleProtocol, _serialPort, _connectionHandle);
            _blePeripheralMap = infoBlock.Execute();

            _bleProtocol.Lib.BLEEventATTClientAttributeValue += OnClientAttributeValue;
            _bleProtocol.Lib.BLEEventConnectionDisconnected += OnDisconnected;

            IsConnected = true;

            return _blePeripheralMap;
        }

        /// <summary>
        /// Disconnect from device
        /// </summary>
        public void Disconnect()
        {
            var disconnectBlock = new BleDisconnectFromService(_bleProtocol, _serialPort, _connectionHandle);
            disconnectBlock.Execute();

            IsConnected = false;

            Dispose();
        }

        /// <summary>
        /// Read characteristic value
        /// </summary>
        /// <param name="uuid">Characteristic UUID</param>
        /// <param name="readLongValue">false = read &lt;= 22 bytes, true = read &gt; 22 bytes</param>
        /// <returns></returns>
        public byte[] ReadCharacteristic(byte[] uuid, bool readLongValue)
        {
            var attrHandle = _blePeripheralMap.FindCharacteristicByUuid(uuid).Handle;

            return ReadAttribute(attrHandle, readLongValue);
        }

        /// <summary>
        /// Read client characteristic configuration
        /// </summary>
        /// <param name="uuid">Characteristic UUID</param>
        /// <returns>Configuration value</returns>
        public BleCCCValue ReadClientCharacteristicConfiguration(byte[] uuid)
        {
            BleCCCValue ccc;

            var characteristic = _blePeripheralMap.FindCharacteristicByUuid(uuid);

            if (!characteristic.HasCCC)
            {
                throw new ArgumentException("Client characteristic {uuid} doesn't have a configuration attribute!");
            }

            var attrHandle = characteristic.HandleCCC;
            var rawValue = ReadAttribute(attrHandle, false);

            using (var bd = new ByteDeserializer(rawValue))
            {
                ccc = bd.DeSerializeBleCCCValue();
            }

            return ccc;
        }

        /// <summary>
        /// Start device discovery
        /// </summary>
        public void StartDeviceDiscovery()
        {
            Init();

            _bleProtocol.Lib.BLEEventGAPScanResponse += OnScanResponse;

            _bleProtocol.Lib.SendCommand(_serialPort, _bleProtocol.Lib.BLECommandGAPDiscover(1));
        }

        /// <summary>
        /// Stop device discovery
        /// </summary>
        public void StopDeviceDiscovery()
        {
            _bleProtocol.Lib.SendCommand(_serialPort, _bleProtocol.Lib.BLECommandGAPEndProcedure());

            _bleProtocol.Lib.BLEEventGAPScanResponse -= OnScanResponse;

            Dispose();
        }

        /// <summary>
        /// Write characteristic
        /// </summary>
        /// <param name="uuid">Characteristic UUID</param>
        /// <param name="value">Value</param>
        public void WriteCharacteristic(byte[] uuid, byte[] value)
        {
            var attrHandle = _blePeripheralMap.FindCharacteristicByUuid(uuid).Handle;

            WriteAttribute(attrHandle, value);
        }

        /// <summary>
        /// Write client characteristic configuration
        /// </summary>
        /// <param name="uuid">Characteristic UUID</param>
        /// <param name="value">Configuration value</param>
        public void WriteClientCharacteristicConfiguration(byte[] uuid, BleCCCValue value)
        {
            var characteristic = _blePeripheralMap.FindCharacteristicByUuid(uuid);

            if (!characteristic.HasCCC)
            {
                throw new ArgumentException($"Client characteristic {uuid} doesn't have a configuration attribute!");
            }

            var bs = new ByteSerializer();
            bs.Serialize(value);

            var attrHandle = characteristic.HandleCCC;

            WriteAttribute(attrHandle, bs.GetBuffer());
        }

        private void Dispose()
        {
            _receiveFromPeripheralThread.Stop();

            _serialPort.Close();
            _serialPort = null;

            _bleProtocol = null;
        }

        private void Init()
        {
            _bleProtocol = new BleProtocol(_isDebugMode);

            _serialPort = new SerialPort(_portName, 115200, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.RequestToSend
            };
            _serialPort.Open();

            _receiveFromPeripheralThread = new ReceiveFromPeripheralThread(_serialPort, _bleProtocol, _portThreadSleep);
            _receiveFromPeripheralThread.Start();
        }

        private void OnClientAttributeValue(object sender, AttributeValueEventArgs e)
        {
            if (_blePeripheralMap.FindCharacteristicByHandle(e.atthandle, out var characteristic))
            {
                CharacteristicValueChanged?.Invoke(sender, new BleCharacteristicValueChangedEventArgs(characteristic.AttributeUuid, e.value));
            }
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            if (e.connection == _connectionHandle)
            {
                _bleProtocol.Lib.BLEEventConnectionDisconnected -= OnDisconnected;
                var disconnectThread = new Thread((parameters) =>
                {
                    var parameterArray = (object[])parameters;
                    Disconnected?.Invoke(parameterArray[0], (DisconnectedEventArgs)parameterArray[1]);
                });
                disconnectThread.Start(new object[] { this, e });

                IsConnected = false;
            }
        }

        private void OnScanResponse(object sender, ScanResponseEventArgs e)
        {
            ScanResponse?.Invoke(sender, new BleScanResponseReceivedEventArgs(e.rssi, e.packet_type, e.sender, (BleAddressType)e.address_type, e.bond, e.data));
        }

        private byte[] ReadAttribute(ushort handle, bool readLongValue)
        {
            if (readLongValue)
            {
                var readLongAttribute = new BleReadLongAttribute(_bleProtocol, _serialPort, _connectionHandle, handle);
                return readLongAttribute.Execute().value;
            }
            else
            {
                var readAttribute = new BleReadAttribute(_bleProtocol, _serialPort, _connectionHandle, handle);
                return readAttribute.Execute().value;
            }
        }

        private void WriteAttribute(ushort handle, byte[] value)
        {
            var writeAttribute = new BleWriteAttribute(_bleProtocol, _serialPort, _connectionHandle, handle, value);

            writeAttribute.Execute();
        }
    }
}
