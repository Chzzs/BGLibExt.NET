using System;
using System.Collections.Generic;

namespace BGLibExt
{
    public class BleService
    {
        public List<BleAttribute> Attributes { get; private set; }
        public List<BleCharacteristic> Characteristics { get; private set; }
        public ushort EndHandle { get; private set; }
        public Guid Uuid { get; private set; }
        public ushort StartHandle { get; private set; }

        public BleService(byte[] uuid, ushort startHandle, ushort endHandle)
        {
            Attributes = new List<BleAttribute>();
            Characteristics = new List<BleCharacteristic>();

            Uuid = uuid.ToGuid();
            StartHandle = startHandle;
            EndHandle = endHandle;
        }
    }
}
