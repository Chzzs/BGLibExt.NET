using BGLibExt.BleCommands;
using System;
using System.Threading.Tasks;

namespace BGLibExt
{
    public class BleCharacteristic
    {
        internal byte Connection { get; private set; }
        public Guid Uuid { get; private set; }
        public ushort Handle { get; private set; }
        public ushort HandleCCC { get; private set; }
        public bool HasCCC { get; private set; }

        public event CharacteristicValueChangedEventHandler ValueChanged;

        public delegate void CharacteristicValueChangedEventHandler(object sender, BleCharacteristicValueChangedEventArgs e);

        internal BleCharacteristic(byte connection, byte[] uuid, ushort handle)
        {
            Connection = connection;
            Uuid = uuid.ToGuid();
            Handle = handle;
        }

        internal void SetCCCHandle(ushort handle)
        {
            HandleCCC = handle;
            HasCCC = true;
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
        public async Task<BleCCCValue> ReadClientCharacteristicConfigurationAsync()
        {
            if (!HasCCC)
            {
                throw new ArgumentException($"Client characteristic {Uuid} doesn't have a configuration attribute!");
            }

            var rawValue = await ReadValueAsync(HandleCCC, false);

            using (var byteDeserializer = new ByteDeserializer(rawValue))
            {
                var ccc = byteDeserializer.DeSerializeBleCCCValue();
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
        public async Task WriteClientCharacteristicConfigurationAsync(BleCCCValue value)
        {
            if (!HasCCC)
            {
                throw new ArgumentException($"Client characteristic {Uuid} doesn't have a configuration attribute!");
            }

            var byteSerializer = new ByteSerializer();
            byteSerializer.Serialize(value);

            await WriteValueAsync(HandleCCC, byteSerializer.GetBuffer());
        }

        private async Task<byte[]> ReadValueAsync(ushort handle, bool readLongValue)
        {
            var readAttributeCommand = new BleReadAttributeCommand();
            var (_, attributeValueEventArgs) = await readAttributeCommand.ExecuteAsync(Connection, handle, readLongValue);
            return attributeValueEventArgs.value;
        }

        private async Task WriteValueAsync(ushort handle, byte[] value)
        {
            var writeAttributeCommand = new BleWriteAttributeCommand();
            await writeAttributeCommand.ExecuteAsync(Connection, handle, value);
        }

        internal void TriggerCharacteristicValueChanged(byte[] data)
        {
            ValueChanged?.Invoke(this, new BleCharacteristicValueChangedEventArgs(data));
        }
    }
}
