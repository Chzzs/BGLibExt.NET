using System;
using System.IO.Ports;

namespace git.jedinja.monomyo.BleInfrastructure.BleConnectorBlocks
{
	internal class BleDisconnectFromService: BleBlock
	{
		public byte Connection  { get; private set; }

		public BleDisconnectFromService (BleProtocol ble, SerialPort port, byte connection)
			: base (ble, port)
		{
			this.Connection = connection;
		}

		public void Execute ()
		{
			byte[] disconnect = Ble.Lib.BLECommandConnectionDisconnect (this.Connection);
			Ble.SendCommand (Port, disconnect);

			byte[] stopScan = Ble.Lib.BLECommandGAPEndProcedure ();
			Ble.SendCommand (Port, stopScan);

			byte[] stopAdvertise = Ble.Lib.BLECommandGAPSetMode (0, 0);
			Ble.SendCommand (Port, stopAdvertise);
		}
	}
}

