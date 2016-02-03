using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void TimerComparatorEventHandler (object sender, TimerComparatorEventArgs e);

	public class TimerComparatorEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public TimerComparatorEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
