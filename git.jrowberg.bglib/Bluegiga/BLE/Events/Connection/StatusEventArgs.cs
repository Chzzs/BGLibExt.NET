using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Connection
{
	public delegate void StatusEventHandler (object sender, StatusEventArgs e);

	public class StatusEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly Byte flags;
		public readonly Byte[] address;
		public readonly Byte address_type;
		public readonly UInt16 conn_interval;
		public readonly UInt16 timeout;
		public readonly UInt16 latency;
		public readonly Byte bonding;

		public StatusEventArgs (Byte connection, Byte flags, Byte[] address, Byte address_type, UInt16 conn_interval, UInt16 timeout, UInt16 latency, Byte bonding)
		{
			this.connection = connection;
			this.flags = flags;
			this.address = address;
			this.address_type = address_type;
			this.conn_interval = conn_interval;
			this.timeout = timeout;
			this.latency = latency;
			this.bonding = bonding;
		}
	}
}
