using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void IOPortConfigFunctionEventHandler (object sender, IOPortConfigFunctionEventArgs e);

	public class IOPortConfigFunctionEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public IOPortConfigFunctionEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
