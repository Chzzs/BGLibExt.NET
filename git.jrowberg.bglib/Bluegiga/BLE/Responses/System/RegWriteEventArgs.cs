using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void RegWriteEventHandler (object sender, RegWriteEventArgs e);

	public class RegWriteEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public RegWriteEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
