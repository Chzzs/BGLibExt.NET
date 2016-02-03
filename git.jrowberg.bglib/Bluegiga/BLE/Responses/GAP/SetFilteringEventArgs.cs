using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void SetFilteringEventHandler (object sender, SetFilteringEventArgs e);

	public class SetFilteringEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SetFilteringEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
