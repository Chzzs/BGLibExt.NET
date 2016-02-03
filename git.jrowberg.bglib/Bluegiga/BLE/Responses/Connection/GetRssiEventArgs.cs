using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Connection
{
	public delegate void GetRssiEventHandler (object sender, GetRssiEventArgs e);

	public class GetRssiEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly SByte rssi;

		public GetRssiEventArgs (Byte connection, SByte rssi)
		{
			this.connection = connection;
			this.rssi = rssi;
		}
	}
}
