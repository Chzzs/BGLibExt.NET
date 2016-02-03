using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void AnalogComparatorEnableEventHandler (object sender, AnalogComparatorEnableEventArgs e);

	public class AnalogComparatorEnableEventArgs : EventArgs
	{
		public AnalogComparatorEnableEventArgs ()
		{
		}
	}
}
