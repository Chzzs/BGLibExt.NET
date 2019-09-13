using Bluegiga.BLE.Events.ATTClient;
using System.Collections.Generic;
using System.IO.Ports;

namespace BGLibExt.BleBlocks
{
    internal class BleReadLongAttribute : BleBlock
    {
        private readonly List<byte> _longReadByteBuffer = new List<byte>();
        private AttributeValueEventArgs _attrValueResponse = null;
        private AttributeValueEventArgs _lastAttrValueResponse = null;

        public ushort AttributeHandle { get; private set; }
        public byte Connection { get; private set; }

        public BleReadLongAttribute(BleProtocol ble, SerialPort port, byte connection, ushort attributeHandle)
            : base(ble, port)
        {
            Connection = connection;
            AttributeHandle = attributeHandle;
        }

        public AttributeValueEventArgs Execute()
        {
            var readAttr = Ble.Lib.BLECommandATTClientReadLong(Connection, AttributeHandle);

            Ble.Lib.BLEEventATTClientProcedureCompleted += ClientProcedureCompleted;
            Ble.Lib.BLEEventATTClientAttributeValue += VerifyAttribute;

            Ble.SendCommand(Port, readAttr);

            WaitEvent(() => _attrValueResponse != null);

            Ble.Lib.BLEEventATTClientProcedureCompleted -= ClientProcedureCompleted;
            Ble.Lib.BLEEventATTClientAttributeValue -= VerifyAttribute;

            return _attrValueResponse;
        }

        private void ClientProcedureCompleted(object sender, ProcedureCompletedEventArgs e)
        {
            _attrValueResponse = new AttributeValueEventArgs(_lastAttrValueResponse.connection, _lastAttrValueResponse.atthandle, _lastAttrValueResponse.type, _longReadByteBuffer.ToArray());
        }

        private void VerifyAttribute(object sender, AttributeValueEventArgs e)
        {
            if (e.connection == Connection && e.atthandle == AttributeHandle)
            {
                _lastAttrValueResponse = e;
                _longReadByteBuffer.AddRange(e.value);
            }
        }
    }
}
