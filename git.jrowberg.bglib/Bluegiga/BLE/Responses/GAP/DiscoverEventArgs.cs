using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void DiscoverEventHandler (object sender, DiscoverEventArgs e);

	public class DiscoverEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public DiscoverEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
