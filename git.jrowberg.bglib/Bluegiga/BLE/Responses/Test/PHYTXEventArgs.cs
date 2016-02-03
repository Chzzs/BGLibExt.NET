using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Test
{
	public delegate void PHYTXEventHandler (object sender, PHYTXEventArgs e);

	public class PHYTXEventArgs : EventArgs
	{
		public PHYTXEventArgs ()
		{
		}
	}
}
