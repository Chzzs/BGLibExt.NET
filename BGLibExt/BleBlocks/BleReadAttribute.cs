using Bluegiga.BLE.Events.ATTClient;
using System.IO.Ports;

namespace BGLibExt.BleBlocks
{
    internal class BleReadAttribute : BleBlock
    {
        private AttributeValueEventArgs _attrValueResponse = null;
        public ushort AttributeHandle { get; private set; }
        public byte Connection { get; private set; }

        public BleReadAttribute(BleProtocol ble, SerialPort port, byte connection, ushort attributeHandle)
            : base(ble, port)
        {
            Connection = connection;
            AttributeHandle = attributeHandle;
        }

        public AttributeValueEventArgs Execute()
        {
            var readAttr = Ble.Lib.BLECommandATTClientReadByHandle(Connection, AttributeHandle);

            Ble.Lib.BLEEventATTClientAttributeValue += VerifyAttribute;

            Ble.SendCommand(Port, readAttr);

            WaitEvent(() => _attrValueResponse != null);

            Ble.Lib.BLEEventATTClientAttributeValue -= VerifyAttribute;

            return _attrValueResponse;
        }

        private void VerifyAttribute(object sender, AttributeValueEventArgs e)
        {
            if (e.connection == Connection && e.atthandle == AttributeHandle)
            {
                _attrValueResponse = e;
            }
        }
    }
}
