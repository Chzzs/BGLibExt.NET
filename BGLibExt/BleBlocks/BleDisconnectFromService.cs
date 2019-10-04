using System.IO.Ports;

namespace BGLibExt.BleBlocks
{
    internal class BleDisconnectFromService : BleBlock
    {
        public byte Connection { get; private set; }

        public BleDisconnectFromService(BleProtocol ble, SerialPort port, byte connection)
            : base(ble, port)
        {
            Connection = connection;
        }

        public void Execute()
        {
            var disconnect = Ble.Lib.BLECommandConnectionDisconnect(Connection);
            Ble.SendCommand(Port, disconnect);
        }
    }
}
