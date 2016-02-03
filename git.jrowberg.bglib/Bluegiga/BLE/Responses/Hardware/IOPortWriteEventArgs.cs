using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void IOPortWriteEventHandler (object sender, IOPortWriteEventArgs e);

	public class IOPortWriteEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public IOPortWriteEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
