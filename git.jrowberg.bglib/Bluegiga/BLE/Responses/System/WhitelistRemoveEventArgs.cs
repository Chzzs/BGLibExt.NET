using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void WhitelistRemoveEventHandler (object sender, WhitelistRemoveEventArgs e);

	public class WhitelistRemoveEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public WhitelistRemoveEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
