using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void IOPortConfigIrqEventHandler (object sender, IOPortConfigIrqEventArgs e);

	public class IOPortConfigIrqEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public IOPortConfigIrqEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
