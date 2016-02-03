using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.SM
{
	public delegate void PasskeyDisplayEventHandler (object sender, PasskeyDisplayEventArgs e);

	public class PasskeyDisplayEventArgs : EventArgs
	{
		public readonly Byte handle;
		public readonly UInt32 passkey;

		public PasskeyDisplayEventArgs (Byte handle, UInt32 passkey)
		{
			this.handle = handle;
			this.passkey = passkey;
		}
	}
}
