using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void WhitelistClearEventHandler (object sender, WhitelistClearEventArgs e);

	public class WhitelistClearEventArgs : EventArgs
	{
		public WhitelistClearEventArgs ()
		{
		}
	}
}
