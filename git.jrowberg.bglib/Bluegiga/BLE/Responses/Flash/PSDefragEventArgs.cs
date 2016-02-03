using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void PSDefragEventHandler (object sender, PSDefragEventArgs e);

	public class PSDefragEventArgs : EventArgs
	{
		public PSDefragEventArgs ()
		{
		}
	}
}
