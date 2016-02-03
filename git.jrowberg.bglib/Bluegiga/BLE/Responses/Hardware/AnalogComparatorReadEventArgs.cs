using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void AnalogComparatorReadEventHandler (object sender, AnalogComparatorReadEventArgs e);

	public class AnalogComparatorReadEventArgs : EventArgs
	{
		public readonly UInt16 result;
		public readonly Byte output;

		public AnalogComparatorReadEventArgs (UInt16 result, Byte output)
		{
			this.result = result;
			this.output = output;
		}
	}
}
