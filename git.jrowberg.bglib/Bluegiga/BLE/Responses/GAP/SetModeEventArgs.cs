using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void SetModeEventHandler (object sender, SetModeEventArgs e);

	public class SetModeEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SetModeEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
