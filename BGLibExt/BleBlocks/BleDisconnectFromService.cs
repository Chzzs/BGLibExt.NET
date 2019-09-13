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

            var stopScan = Ble.Lib.BLECommandGAPEndProcedure();
            Ble.SendCommand(Port, stopScan);

            var stopAdvertise = Ble.Lib.BLECommandGAPSetMode(0, 0);
            Ble.SendCommand(Port, stopAdvertise);
        }
    }
}
