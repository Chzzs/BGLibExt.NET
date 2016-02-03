using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Attributes
{
	public delegate void ReadTypeEventHandler (object sender, ReadTypeEventArgs e);

	public class ReadTypeEventArgs : EventArgs
	{
		public readonly UInt16 handle;
		public readonly UInt16 result;
		public readonly Byte[] value;

		public ReadTypeEventArgs (UInt16 handle, UInt16 result, Byte[] value)
		{
			this.handle = handle;
			this.result = result;
			this.value = value;
		}
	}
}
