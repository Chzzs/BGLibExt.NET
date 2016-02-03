using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void SetAdvDataEventHandler (object sender, SetAdvDataEventArgs e);

	public class SetAdvDataEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SetAdvDataEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
