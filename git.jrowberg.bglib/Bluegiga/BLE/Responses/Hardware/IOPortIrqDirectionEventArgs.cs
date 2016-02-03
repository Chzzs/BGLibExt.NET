using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void IOPortIrqDirectionEventHandler (object sender, IOPortIrqDirectionEventArgs e);

	public class IOPortIrqDirectionEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public IOPortIrqDirectionEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
