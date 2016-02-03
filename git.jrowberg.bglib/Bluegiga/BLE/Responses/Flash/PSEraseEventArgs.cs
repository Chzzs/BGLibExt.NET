using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void PSEraseEventHandler (object sender, PSEraseEventArgs e);

	public class PSEraseEventArgs : EventArgs
	{
		public PSEraseEventArgs ()
		{
		}
	}
}
