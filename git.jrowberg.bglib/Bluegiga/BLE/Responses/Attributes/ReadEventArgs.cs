using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Attributes
{
	public delegate void ReadEventHandler (object sender, ReadEventArgs e);

	public class ReadEventArgs : EventArgs
	{
		public readonly UInt16 handle;
		public readonly UInt16 offset;
		public readonly UInt16 result;
		public readonly Byte[] value;

		public ReadEventArgs (UInt16 handle, UInt16 offset, UInt16 result, Byte[] value)
		{
			this.handle = handle;
			this.offset = offset;
			this.result = result;
			this.value = value;
		}
	}
}
