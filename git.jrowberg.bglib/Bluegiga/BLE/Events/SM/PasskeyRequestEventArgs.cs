using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.SM
{
	public delegate void PasskeyRequestEventHandler (object sender, PasskeyRequestEventArgs e);

	public class PasskeyRequestEventArgs : EventArgs
	{
		public readonly Byte handle;

		public PasskeyRequestEventArgs (Byte handle)
		{
			this.handle = handle;
		}
	}
}
