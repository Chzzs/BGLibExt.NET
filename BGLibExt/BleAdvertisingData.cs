using System;
using System.Linq;
using System.Text;

namespace BGLibExt
{
    public class BleAdvertisingData
    {
        public byte[] Data { get; private set; }
        public BleAdvertisingDataType Type { get; private set; }

        public BleAdvertisingData(BleAdvertisingDataType type, byte[] data)
        {
            Type = type;
            Data = data;
        }

        public byte[] GetManufacturerData()
        {
            return Data.Skip(2).ToArray();
        }

        public ushort GetManufacturerId()
        {
            return BitConverter.ToUInt16(Data, 0);
        }

        public string ToAsciiString()
        {
            return Encoding.ASCII.GetString(Data);
        }

        public string ToDebugString()
        {
            switch (Type)
            {
                case BleAdvertisingDataType.CompleteLocalName:
                case BleAdvertisingDataType.ShortenedLocalName:
                    return ToAsciiString();
                case BleAdvertisingDataType.CompleteListof128BitServiceClassUUIDs:
                case BleAdvertisingDataType.IncompleteListof128BitServiceClassUUIDs:
                case BleAdvertisingDataType.Listof128BitServiceSolicitationUUIDs:
                case BleAdvertisingDataType.ServiceData128BitUUID:
                    return ToGuid().ToString();
                default:
                    return ToHexString();
            }
        }

        public Guid ToGuid()
        {
            var guidBytes = Data.ToArray().Reverse().ToArray();
            Array.Reverse(guidBytes, 0, 4);
            Array.Reverse(guidBytes, 4, 2);
            Array.Reverse(guidBytes, 6, 2);
            return new Guid(guidBytes);
        }

        public string ToHexString()
        {
            return Data.ByteArrayToHexString();
        }

        public ushort ToUint16()
        {
            return BitConverter.ToUInt16(Data, 0);
        }

        public byte ToUint8()
        {
            return Data[0];
        }
    }
}
