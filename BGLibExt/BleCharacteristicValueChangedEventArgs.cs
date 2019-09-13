using System;

namespace BGLibExt
{
    public class BleCharacteristicValueChangedEventArgs : EventArgs
    {
        public byte[] CharacteristicUuid { get; private set; }
        public byte[] Value { get; private set; }

        public BleCharacteristicValueChangedEventArgs(byte[] uuid, byte[] value)
        {
            CharacteristicUuid = uuid;
            Value = value;
        }
    }
}
