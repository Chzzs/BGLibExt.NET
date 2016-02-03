using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.System
{
	public delegate void ProtocolErrorEventHandler (object sender, ProtocolErrorEventArgs e);

	public class ProtocolErrorEventArgs : EventArgs
	{
		public readonly UInt16 reason;

		public ProtocolErrorEventArgs (UInt16 reason)
		{
			this.reason = reason;
		}
	}
}
