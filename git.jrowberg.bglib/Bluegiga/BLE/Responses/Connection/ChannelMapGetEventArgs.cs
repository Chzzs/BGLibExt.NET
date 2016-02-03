using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Connection
{
	public delegate void ChannelMapGetEventHandler (object sender, ChannelMapGetEventArgs e);

	public class ChannelMapGetEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly Byte[] map;

		public ChannelMapGetEventArgs (Byte connection, Byte[] map)
		{
			this.connection = connection;
			this.map = map;
		}
	}
}
