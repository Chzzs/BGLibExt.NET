using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void PSDumpEventHandler (object sender, PSDumpEventArgs e);

	public class PSDumpEventArgs : EventArgs
	{
		public PSDumpEventArgs ()
		{
		}
	}
}
