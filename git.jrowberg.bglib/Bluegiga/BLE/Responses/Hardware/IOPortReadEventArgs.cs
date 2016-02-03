using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void IOPortReadEventHandler (object sender, IOPortReadEventArgs e);

	public class IOPortReadEventArgs : EventArgs
	{
		public readonly UInt16 result;
		public readonly Byte port;
		public readonly Byte data;

		public IOPortReadEventArgs (UInt16 result, Byte port, Byte data)
		{
			this.result = result;
			this.port = port;
			this.data = data;
		}
	}
}
