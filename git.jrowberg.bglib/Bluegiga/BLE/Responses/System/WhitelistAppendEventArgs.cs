using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void WhitelistAppendEventHandler (object sender, WhitelistAppendEventArgs e);

	public class WhitelistAppendEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public WhitelistAppendEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
