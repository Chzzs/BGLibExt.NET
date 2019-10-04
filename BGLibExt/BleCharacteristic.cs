using BGLibExt.BleCommands;
using Bluegiga;
using Microsoft.Extensions.Logging;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace BGLibExt
{
    public class BleCharacteristic
    {
        private readonly BGLib _bgLib;
        private readonly BleModuleConnection _bleModuleConnection;
        private readonly ILogger _logger;
        private readonly byte _connection;

        public Guid Uuid { get; private set; }
        public ushort Handle { get; private set; }
        public ushort HandleCcc { get; private set; }
        public bool HasCcc { get; private set; }

        public event CharacteristicValueChangedEventHandler ValueChanged;

        public delegate void CharacteristicValueChangedEventHandler(object sender, BleCharacteristicValueChangedEventArgs e);

        internal BleCharacteristic(BGLib bgLib, BleModuleConnection bleModuleConnection, ILogger logger, byte connection, byte[] uuid, ushort handle)
        {
            _bgLib = bgLib;
            _bleModuleConnection = bleModuleConnection;
            _logger = logger;
            _connection = connection;
            Uuid = uuid.ToBleGuid();
            Handle = handle;
        }

        internal void SetCccHandle(ushort handle)
        {
            HandleCcc = handle;
            HasCcc = true;
        }

        /// <summary>
        /// Read characteristic value
        /// </summary>
        /// <param name="readLongValue">false = read &lt;= 22 bytes, true = read &gt; 22 bytes</param>
        /// <returns>Characteristic value</returns>
        public async Task<byte[]> ReadValueAsync(bool readLongValue = false)
        {
            return await ReadValueAsync(Handle, readLongValue);
        }

        /// <summary>
        /// Read client characteristic configuration
        /// </summary>
        /// <returns>Configuration value</returns>
        public async Task<BleCccValue> ReadCccAsync()
        {
            if (!HasCcc)
            {
                throw new ArgumentException($"Client characteristic {Uuid} doesn't have a configuration attribute!");
            }

            var rawValue = await ReadValueAsync(HandleCcc, false);

            using (var byteDeserializer = new ByteDeserializer(rawValue))
            {
                var ccc = byteDeserializer.DeSerializeBleCccValue();
                return ccc;
            }
        }

        /// <summary>
        /// Write characteristic value
        /// </summary>
        /// <param name="value">Characteristic value</param>
        public async Task WriteValueAsync(byte[] value)
        {
            await WriteValueAsync(Handle, value);
        }

        /// <summary>
        /// Write client characteristic configuration
        /// </summary>
        /// <param name="value">Configuration value</param>
        public async Task WriteCccAsync(BleCccValue value)
        {
            if (!HasCcc)
            {
                throw new ArgumentException($"Client characteristic {Uuid} doesn't have a configuration attribute!");
            }

            var byteSerializer = new ByteSerializer();
            byteSerializer.Serialize(value);

            await WriteValueAsync(HandleCcc, byteSerializer.GetBuffer());
        }

        private async Task<byte[]> ReadValueAsync(ushort handle, bool readLongValue)
        {
            var readAttributeCommand = new BleReadAttributeCommand(_bgLib, _bleModuleConnection, _logger);
            var (_, attributeValueEventArgs) = await readAttributeCommand.ExecuteAsync(_connection, handle, readLongValue);
            return attributeValueEventArgs.value;
        }

        private async Task WriteValueAsync(ushort handle, byte[] value)
        {
            var writeAttributeCommand = new BleWriteAttributeCommand(_bgLib, _bleModuleConnection, _logger);
            await writeAttributeCommand.ExecuteAsync(_connection, handle, value);
        }

        internal void TriggerCharacteristicValueChanged(byte[] data)
        {
            ValueChanged?.Invoke(this, new BleCharacteristicValueChangedEventArgs(data));
        }
    }
}
