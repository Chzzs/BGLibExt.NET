using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Connection
{
	public delegate void RawTXEventHandler (object sender, RawTXEventArgs e);

	public class RawTXEventArgs : EventArgs
	{
		public readonly Byte connection;

		public RawTXEventArgs (Byte connection)
		{
			this.connection = connection;
		}
	}
}
