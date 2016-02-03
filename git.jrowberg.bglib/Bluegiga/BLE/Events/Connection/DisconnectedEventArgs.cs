using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Connection
{
	public delegate void DisconnectedEventHandler (object sender, DisconnectedEventArgs e);

	public class DisconnectedEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 reason;

		public DisconnectedEventArgs (Byte connection, UInt16 reason)
		{
			this.connection = connection;
			this.reason = reason;
		}
	}
}
