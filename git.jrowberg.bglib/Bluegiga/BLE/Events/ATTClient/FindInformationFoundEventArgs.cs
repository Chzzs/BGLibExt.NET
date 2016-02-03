using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient
{
	public delegate void FindInformationFoundEventHandler (object sender, FindInformationFoundEventArgs e);

	public class FindInformationFoundEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 chrhandle;
		public readonly Byte[] uuid;

		public FindInformationFoundEventArgs (Byte connection, UInt16 chrhandle, Byte[] uuid)
		{
			this.connection = connection;
			this.chrhandle = chrhandle;
			this.uuid = uuid;
		}
	}
}
