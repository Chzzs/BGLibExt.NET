using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Connection
{
	public delegate void GetStatusEventHandler (object sender, GetStatusEventArgs e);

	public class GetStatusEventArgs : EventArgs
	{
		public readonly Byte connection;

		public GetStatusEventArgs (Byte connection)
		{
			this.connection = connection;
		}
	}
}
