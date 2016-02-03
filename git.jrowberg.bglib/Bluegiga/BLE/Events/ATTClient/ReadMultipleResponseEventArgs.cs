using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient
{
	public delegate void ReadMultipleResponseEventHandler (object sender, Bluegiga.BLE.Events.ATTClient.ReadMultipleResponseEventArgs e);
	public class ReadMultipleResponseEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly Byte[] handles;

		public ReadMultipleResponseEventArgs (Byte connection, Byte[] handles)
		{
			this.connection = connection;
			this.handles = handles;
		}
	}
}
