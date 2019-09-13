using System.Collections.Generic;

namespace BGLibExt
{
    public class BlePeripheralService
    {
        public List<BlePeripheralAttribute> Attributes { get; private set; }
        public List<BlePeripheralCharacteristic> Characteristics { get; private set; }
        public ushort EndHandle { get; private set; }
        public byte[] ServiceUuid { get; private set; }
        public ushort StartHandle { get; private set; }

        public BlePeripheralService(byte[] uuid, ushort startHandle, ushort endHandle)
        {
            Attributes = new List<BlePeripheralAttribute>();
            Characteristics = new List<BlePeripheralCharacteristic>();

            ServiceUuid = uuid;
            StartHandle = startHandle;
            EndHandle = endHandle;
        }
    }
}
