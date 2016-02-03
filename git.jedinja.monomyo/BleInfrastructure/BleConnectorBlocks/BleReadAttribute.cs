using System;
using System.IO.Ports;
using git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient;

namespace git.jedinja.monomyo.BleInfrastructure.BleConnectorBlocks
{
	internal class BleReadAttribute : BleBlock
	{
		public byte Connection  { get; private set; }
		public ushort AttributeHandle  { get; private set; }

		public BleReadAttribute (BleProtocol ble, SerialPort port, byte connection, ushort attributeHandle)
			: base (ble, port)
		{
			this.Connection = connection;
			this.AttributeHandle = attributeHandle;
		}

		public AttributeValueEventArgs Execute ()
		{
			byte[] readAttr = Ble.Lib.BLECommandATTClientReadByHandle (this.Connection, this.AttributeHandle);

			AttributeValueEventHandler handler = this.VerifyAttribute;

			Ble.Lib.BLEEventATTClientAttributeValue += handler;

			Ble.SendCommand (this.Port, readAttr);

			this.WaitEvent (() => _attrValueResponse != null);

			Ble.Lib.BLEEventATTClientAttributeValue -= handler;

			return _attrValueResponse;
		}

		private AttributeValueEventArgs _attrValueResponse = null;
		private void VerifyAttribute (object sender, AttributeValueEventArgs e)
		{
			if (e.connection == this.Connection && e.atthandle == this.AttributeHandle)
			{
				_attrValueResponse = e;
			}
		}
	}
}

