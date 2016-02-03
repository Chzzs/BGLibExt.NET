using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void SetSoftTimerEventHandler (object sender, SetSoftTimerEventArgs e);

	public class SetSoftTimerEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SetSoftTimerEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
