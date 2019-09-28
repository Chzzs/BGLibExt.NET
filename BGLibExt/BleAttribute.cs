namespace BGLibExt
{
    public class BleAttribute
    {
        public static byte[] CharacteristicCccUuid = new byte[] { 0x02, 0x29 };
        public static byte[] CharacteristicUuid = new byte[] { 0x03, 0x28 };
        public static byte[] ServiceUuid = new byte[] { 0x00, 0x28 };

        internal byte Connection { get; private set; }
        public byte[] AttributeUuid { get; private set; }
        public ushort Handle { get; private set; }

        public BleAttribute(byte connection, byte[] uuid, ushort handle)
        {
            Connection = connection;
            AttributeUuid = uuid;
            Handle = handle;
        }
    }
}
