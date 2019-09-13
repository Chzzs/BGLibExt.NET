using System;
using System.Collections.Generic;

namespace BGLibExt
{
    public class BleScanResponseReceivedEventArgs : EventArgs
    {
        public byte[] Address { get; private set; }
        public BleAddressType AddressType { get; private set; }
        public byte Bond { get; private set; }
        public byte[] Data { get; private set; }
        public byte PacketType { get; private set; }
        public List<BleAdvertisingData> ParsedData { get; private set; }
        public sbyte Rssi { get; private set; }

        public BleScanResponseReceivedEventArgs(sbyte rssi, byte packetType, byte[] address, BleAddressType addressType, byte bond, byte[] data)
        {
            Rssi = rssi;
            PacketType = packetType;
            Address = address;
            AddressType = addressType;
            Bond = bond;
            Data = data;
            ParsedData = BleAdvertisingDataParser.Parse(data);
        }
    }
}
