using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Hardware
{
	public delegate void AnalogComparatorStatusEventHandler (object sender, AnalogComparatorStatusEventArgs e);

	public class AnalogComparatorStatusEventArgs : EventArgs
	{
		public readonly UInt32 timestamp;
		public readonly Byte output;

		public AnalogComparatorStatusEventArgs (UInt32 timestamp, Byte output)
		{
			this.timestamp = timestamp;
			this.output = output;
		}
	}
}
