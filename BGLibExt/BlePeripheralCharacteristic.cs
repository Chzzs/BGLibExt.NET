namespace BGLibExt
{
    public class BlePeripheralCharacteristic
    {
        public byte[] AttributeUuid { get; private set; }
        public ushort Handle { get; private set; }
        public ushort HandleCCC { get; private set; }
        public bool HasCCC { get; private set; }

        public BlePeripheralCharacteristic(byte[] uuid, ushort handle)
        {
            AttributeUuid = uuid;
            Handle = handle;
        }

        public void SetCCCHandle(ushort handle)
        {
            HandleCCC = handle;
            HasCCC = true;
        }
    }
}
