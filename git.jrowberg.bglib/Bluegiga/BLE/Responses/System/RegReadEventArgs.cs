using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void RegReadEventHandler (object sender, RegReadEventArgs e);

	public class RegReadEventArgs : EventArgs
	{
		public readonly UInt16 address;
		public readonly Byte value;

		public RegReadEventArgs (UInt16 address, Byte value)
		{
			this.address = address;
			this.value = value;
		}
	}
}
