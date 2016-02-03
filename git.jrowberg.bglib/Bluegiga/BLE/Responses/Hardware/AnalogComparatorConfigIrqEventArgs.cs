using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void AnalogComparatorConfigIrqEventHandler (object sender, AnalogComparatorConfigIrqEventArgs e);

	public class AnalogComparatorConfigIrqEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public AnalogComparatorConfigIrqEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
