using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.ATTClient
{
	public delegate void PrepareWriteEventHandler (object sender, PrepareWriteEventArgs e);

	public class PrepareWriteEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 result;

		public PrepareWriteEventArgs (Byte connection, UInt16 result)
		{
			this.connection = connection;
			this.result = result;
		}
	}
}
