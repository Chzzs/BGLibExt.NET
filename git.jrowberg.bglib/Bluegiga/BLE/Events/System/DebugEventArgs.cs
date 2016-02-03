using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.System
{
	public delegate void DebugEventHandler (object sender, DebugEventArgs e);

	public class DebugEventArgs : EventArgs
	{
		public readonly Byte[] data;

		public DebugEventArgs (Byte[] data)
		{
			this.data = data;
		}
	}
}
