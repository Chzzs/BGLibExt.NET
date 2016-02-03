using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Connection
{
	public delegate void VersionUpdateEventHandler (object sender, VersionUpdateEventArgs e);

	public class VersionUpdateEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 result;

		public VersionUpdateEventArgs (Byte connection, UInt16 result)
		{
			this.connection = connection;
			this.result = result;
		}
	}
}
