using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.SM
{
	public delegate void PasskeyEntryEventHandler (object sender, PasskeyEntryEventArgs e);

	public class PasskeyEntryEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public PasskeyEntryEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
