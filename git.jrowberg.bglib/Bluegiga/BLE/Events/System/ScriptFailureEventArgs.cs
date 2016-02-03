using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.System
{
	public delegate void ScriptFailureEventHandler (object sender, ScriptFailureEventArgs e);

	public class ScriptFailureEventArgs : EventArgs
	{
		public readonly UInt16 address;
		public readonly UInt16 reason;

		public ScriptFailureEventArgs (UInt16 address, UInt16 reason)
		{
			this.address = address;
			this.reason = reason;
		}
	}
}
