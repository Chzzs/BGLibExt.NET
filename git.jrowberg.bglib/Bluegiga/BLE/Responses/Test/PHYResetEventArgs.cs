using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Test
{
	public delegate void PHYResetEventHandler (object sender, PHYResetEventArgs e);

	public class PHYResetEventArgs : EventArgs
	{
		public PHYResetEventArgs ()
		{
		}
	}
}
