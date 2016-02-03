using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.DFU
{
	public delegate void BootEventHandler (object sender, BootEventArgs e);

	public class BootEventArgs : EventArgs
	{
		public readonly UInt32 version;

		public BootEventArgs (UInt32 version)
		{
			this.version = version;
		}
	}
}
