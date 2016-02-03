using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Connection
{
	public delegate void DisconnectEventHandler (object sender, DisconnectEventArgs e);

	public class DisconnectEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 result;

		public DisconnectEventArgs (Byte connection, UInt16 result)
		{
			this.connection = connection;
			this.result = result;
		}
	}
}
