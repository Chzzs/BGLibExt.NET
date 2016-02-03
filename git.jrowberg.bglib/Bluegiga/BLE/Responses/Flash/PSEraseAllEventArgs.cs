using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void PSEraseAllEventHandler (object sender, PSEraseAllEventArgs e);

	public class PSEraseAllEventArgs : EventArgs
	{
		public PSEraseAllEventArgs ()
		{
		}
	}
}
