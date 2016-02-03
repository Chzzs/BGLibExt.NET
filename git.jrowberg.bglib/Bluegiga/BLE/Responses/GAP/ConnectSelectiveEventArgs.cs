using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void ConnectSelectiveEventHandler (object sender, ConnectSelectiveEventArgs e);

	public class ConnectSelectiveEventArgs : EventArgs
	{
		public readonly UInt16 result;
		public readonly Byte connection_handle;

		public ConnectSelectiveEventArgs (UInt16 result, Byte connection_handle)
		{
			this.result = result;
			this.connection_handle = connection_handle;
		}
	}
}
