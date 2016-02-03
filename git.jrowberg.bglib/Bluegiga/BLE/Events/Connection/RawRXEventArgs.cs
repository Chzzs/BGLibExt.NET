using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Connection
{
	public delegate void RawRXEventHandler (object sender, RawRXEventArgs e);

	public class RawRXEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly Byte[] data;

		public RawRXEventArgs (Byte connection, Byte[] data)
		{
			this.connection = connection;
			this.data = data;
		}
	}
}
