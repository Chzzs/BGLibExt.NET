using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Hardware
{
	public delegate void SoftTimerEventHandler (object sender, SoftTimerEventArgs e);

	public class SoftTimerEventArgs : EventArgs
	{
		public readonly Byte handle;

		public SoftTimerEventArgs (Byte handle)
		{
			this.handle = handle;
		}
	}
}
