using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Attributes
{
	public delegate void ValueEventHandler (object sender, ValueEventArgs e);

	public class ValueEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly Byte reason;
		public readonly UInt16 handle;
		public readonly UInt16 offset;
		public readonly Byte[] value;

		public ValueEventArgs (Byte connection, Byte reason, UInt16 handle, UInt16 offset, Byte[] value)
		{
			this.connection = connection;
			this.reason = reason;
			this.handle = handle;
			this.offset = offset;
			this.value = value;
		}
	}
}
