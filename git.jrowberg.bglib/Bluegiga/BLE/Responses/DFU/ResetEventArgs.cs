using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.DFU
{
	public delegate void ResetEventHandler (object sender, ResetEventArgs e);

	public class ResetEventArgs : EventArgs
	{
		public ResetEventArgs ()
		{
		}
	}
}
