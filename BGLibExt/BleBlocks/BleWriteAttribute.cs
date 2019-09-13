using Bluegiga.BLE.Events.ATTClient;
using System.IO.Ports;

namespace BGLibExt.BleBlocks
{
    internal class BleWriteAttribute : BleBlock
    {
        private ProcedureCompletedEventArgs _response = null;

        public ushort AttributeHandle { get; private set; }
        public byte Connection { get; private set; }
        public byte[] Value { get; private set; }

        public BleWriteAttribute(BleProtocol ble, SerialPort port, byte connection, ushort attributeHandle, byte[] value)
            : base(ble, port)
        {
            Connection = connection;
            AttributeHandle = attributeHandle;
            Value = value;
        }

        public void Execute()
        {
            var writeToAttr = Ble.Lib.BLECommandATTClientAttributeWrite(Connection, AttributeHandle, (byte[])Value);

            Ble.Lib.BLEEventATTClientProcedureCompleted += ProcedureCompletedHandler;

            Ble.SendCommand(Port, writeToAttr);

            WaitEvent(() => _response != null);

            Ble.Lib.BLEEventATTClientProcedureCompleted -= ProcedureCompletedHandler;
        }

        private void ProcedureCompletedHandler(object sender, ProcedureCompletedEventArgs e)
        {
            if (e.connection == Connection && e.chrhandle == AttributeHandle)
            {
                _response = e;
            }
        }
    }
}
