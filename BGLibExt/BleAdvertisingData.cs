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
                case BleAdvertisingDataType.CompleteListof16BitServiceClassUUIDs:
                case BleAdvertisingDataType.IncompleteListof16BitServiceClassUUIDs:
                case BleAdvertisingDataType.Listof16BitServiceSolicitationUUIDs:
                case BleAdvertisingDataType.ServiceData16BitUUID:
                    return string.Join(",", Data.Split(2).Select(x => BitConverter.ToUInt16(x, 0).ToString("X4")));
                case BleAdvertisingDataType.CompleteListof32BitServiceClassUUIDs:
                case BleAdvertisingDataType.IncompleteListof32BitServiceClassUUIDs:
                case BleAdvertisingDataType.Listof32BitServiceSolicitationUUIDs:
                case BleAdvertisingDataType.ServiceData32BitUUID:
                    return string.Join(",", Data.Split(4).Select(x => BitConverter.ToUInt32(x, 0).ToString("X8")));
                case BleAdvertisingDataType.CompleteListof128BitServiceClassUUIDs:
                case BleAdvertisingDataType.IncompleteListof128BitServiceClassUUIDs:
                case BleAdvertisingDataType.Listof128BitServiceSolicitationUUIDs:
                case BleAdvertisingDataType.ServiceData128BitUUID:
                    return string.Join(",", Data.Split(16).Select(x => x.ToBleGuid()));
                default:
                    return ToHexString();
            }
        }

        public Guid ToGuid()
        {
            return Data.ToBleGuid();
        }

        public string ToHexString()
        {
            return Data.ToHexString();
        }

        public uint ToUint32()
        {
            return BitConverter.ToUInt32(Data, 0);
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
