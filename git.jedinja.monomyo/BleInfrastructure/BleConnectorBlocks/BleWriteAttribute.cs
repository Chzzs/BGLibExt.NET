using System;
using System.IO.Ports;
using git.jrowberg.bglib.Bluegiga.BLE.Responses.ATTClient;
using git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient;

namespace git.jedinja.monomyo.BleInfrastructure.BleConnectorBlocks
{
	internal class BleWriteAttribute : BleBlock
	{
		public byte Connection  { get; private set; }
		public ushort AttributeHandle  { get; private set; }
		public Bytes Value  { get; private set; }

		public BleWriteAttribute (BleProtocol ble, SerialPort port, byte connection, ushort attributeHandle, Bytes value)
			: base (ble, port)
		{
			this.Connection = connection;
			this.AttributeHandle = attributeHandle;
			this.Value = value;
		}

		public void Execute ()
		{
			Byte[] writeToAttr = Ble.Lib.BLECommandATTClientAttributeWrite (this.Connection, this.AttributeHandle, (byte[]) this.Value);

			ProcedureCompletedEventHandler handler = this.ProcedureCompletedHandler;

			Ble.Lib.BLEEventATTClientProcedureCompleted += handler;

			Ble.SendCommand (this.Port, writeToAttr);

			this.WaitEvent (() => _response != null);

			Ble.Lib.BLEEventATTClientProcedureCompleted -= handler;
		}

		private ProcedureCompletedEventArgs _response = null;
		private void ProcedureCompletedHandler (object sender, ProcedureCompletedEventArgs e)
		{
			if (e.connection == this.Connection && e.chrhandle == this.AttributeHandle)
			{
				_response = e;
			}
		}
	}
}

