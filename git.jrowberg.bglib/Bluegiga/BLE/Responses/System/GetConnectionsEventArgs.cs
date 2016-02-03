using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void GetConnectionsEventHandler (object sender, GetConnectionsEventArgs e);

	public class GetConnectionsEventArgs : EventArgs
	{
		public readonly Byte maxconn;

		public GetConnectionsEventArgs (Byte maxconn)
		{
			this.maxconn = maxconn;
		}
	}
}
