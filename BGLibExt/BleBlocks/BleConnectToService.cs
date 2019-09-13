using Bluegiga.BLE.Events.Connection;
using System.IO.Ports;

namespace BGLibExt.BleBlocks
{
    internal class BleConnectToService : BleBlock
    {
        private StatusEventArgs _connectionResponse = null;

        public byte[] Address { get; private set; }
        public BleAddressType AddressType { get; private set; }

        public BleConnectToService(BleProtocol ble, SerialPort port, byte[] address, BleAddressType addressType)
            : base(ble, port)
        {
            Address = address;
            AddressType = addressType;
        }

        public StatusEventArgs Execute()
        {
            var connect = Ble.Lib.BLECommandGAPConnectDirect((byte[])Address, (byte)AddressType, 0x20, 0x30, 0x100, 0);

            Ble.Lib.BLEEventConnectionStatus += FindConnection;

            Ble.SendCommand(Port, connect);

            WaitEvent(() => _connectionResponse != null);

            Ble.Lib.BLEEventConnectionStatus -= FindConnection;

            return _connectionResponse;
        }

        private void FindConnection(object sender, StatusEventArgs e)
        {
            if ((e.flags & 0x05) == 0x05) // used as is from BgLib examples
            {
                _connectionResponse = e;
            }
        }
    }
}
