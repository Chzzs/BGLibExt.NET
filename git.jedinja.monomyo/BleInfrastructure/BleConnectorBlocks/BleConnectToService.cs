using System;
using System.IO.Ports;
using git.jrowberg.bglib.Bluegiga.BLE.Events.Connection;

namespace git.jedinja.monomyo.BleInfrastructure.BleConnectorBlocks
{
	internal class BleConnectToService : BleBlock
	{
		public Bytes ServiceUUID  { get; private set; }
		public byte Address  { get; private set; }

		public BleConnectToService (BleProtocol ble, SerialPort port, Bytes service, byte address)
			: base (ble, port)
		{
			this.ServiceUUID = service;
			this.Address = address;
		}

		public StatusEventArgs Execute ()
		{
			Byte[] connect = Ble.Lib.BLECommandGAPConnectDirect ((byte[]) this.ServiceUUID, this.Address, 0x20, 0x30, 0x100, 0);

			StatusEventHandler handler = this.FindConnection;

			Ble.Lib.BLEEventConnectionStatus += handler;

			Ble.SendCommand (this.Port, connect);

			this.WaitEvent (() => _connectionResponse != null);

			Ble.Lib.BLEEventConnectionStatus -= handler; // not needed anymore
			return _connectionResponse;
		}

		private StatusEventArgs _connectionResponse = null;
		private void FindConnection (object sender, StatusEventArgs e)
		{
			if ((e.flags & 0x05) == 0x05) // used as is from BgLib examples
			{
				_connectionResponse = e;
			}
		}
	}
}

