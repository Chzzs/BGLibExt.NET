using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Attributes
{
	public delegate void UserReadRequestEventHandler (object sender, UserReadRequestEventArgs e);
	public class UserReadRequestEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 handle;
		public readonly UInt16 offset;
		public readonly Byte maxsize;

		public UserReadRequestEventArgs (Byte connection, UInt16 handle, UInt16 offset, Byte maxsize)
		{
			this.connection = connection;
			this.handle = handle;
			this.offset = offset;
			this.maxsize = maxsize;
		}
	}
}
