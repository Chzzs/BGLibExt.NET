namespace BGLibExt
{
    public class BlePeripheralAttribute
    {
        public static byte[] CharacteristicCccUuid = new byte[] { 0x02, 0x29 };
        public static byte[] CharacteristicUuid = new byte[] { 0x03, 0x28 };
        public static byte[] ServiceUuid = new byte[] { 0x00, 0x28 };

        public byte[] AttributeUuid { get; private set; }
        public ushort Handle { get; private set; }

        public BlePeripheralAttribute(byte[] uuid, ushort handle)
        {
            AttributeUuid = uuid;
            Handle = handle;
        }
    }
}
