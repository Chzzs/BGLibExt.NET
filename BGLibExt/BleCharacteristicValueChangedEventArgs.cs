using System;

namespace BGLibExt
{
    public class BleCharacteristicValueChangedEventArgs : EventArgs
    {
        public byte[] Value { get; private set; }

        public BleCharacteristicValueChangedEventArgs(byte[] value)
        {
            Value = value;
        }
    }
}
