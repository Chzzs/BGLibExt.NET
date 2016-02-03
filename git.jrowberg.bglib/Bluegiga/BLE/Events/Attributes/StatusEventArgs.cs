using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Attributes
{
	public delegate void StatusEventHandler (object sender, StatusEventArgs e);

	public class StatusEventArgs : EventArgs
	{
		public readonly UInt16 handle;
		public readonly Byte flags;

		public StatusEventArgs (UInt16 handle, Byte flags)
		{
			this.handle = handle;
			this.flags = flags;
		}
	}
}
