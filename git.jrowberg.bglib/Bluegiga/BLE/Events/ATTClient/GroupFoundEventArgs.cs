using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient
{
	public delegate void GroupFoundEventHandler (object sender, GroupFoundEventArgs e);

	public class GroupFoundEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 start;
		public readonly UInt16 end;
		public readonly Byte[] uuid;

		public GroupFoundEventArgs (Byte connection, UInt16 start, UInt16 end, Byte[] uuid)
		{
			this.connection = connection;
			this.start = start;
			this.end = end;
			this.uuid = uuid;
		}
	}
}
