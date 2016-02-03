using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void SetDirectedConnectableModeEventHandler (object sender, SetDirectedConnectableModeEventArgs e);

	public class SetDirectedConnectableModeEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SetDirectedConnectableModeEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
