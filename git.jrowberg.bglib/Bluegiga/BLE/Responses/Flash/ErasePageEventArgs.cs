using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void ErasePageEventHandler (object sender, ErasePageEventArgs e);

	public class ErasePageEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public ErasePageEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
