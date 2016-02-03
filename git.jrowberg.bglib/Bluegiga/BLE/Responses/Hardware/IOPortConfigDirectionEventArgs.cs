using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void IOPortConfigDirectionEventHandler (object sender, IOPortConfigDirectionEventArgs e);

	public class IOPortConfigDirectionEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public IOPortConfigDirectionEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
