using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void ReadMemoryEventHandler (object sender, ReadMemoryEventArgs e);

	public class ReadMemoryEventArgs : EventArgs
	{
		public readonly UInt32 address;
		public readonly Byte[] data;

		public ReadMemoryEventArgs (UInt32 address, Byte[] data)
		{
			this.address = address;
			this.data = data;
		}
	}
}
