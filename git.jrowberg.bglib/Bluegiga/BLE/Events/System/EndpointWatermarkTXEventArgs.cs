using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.System
{
	public delegate void EndpointWatermarkTXEventHandler (object sender, EndpointWatermarkTXEventArgs e);

	public class EndpointWatermarkTXEventArgs : EventArgs
	{
		public readonly Byte endpoint;
		public readonly Byte data;

		public EndpointWatermarkTXEventArgs (Byte endpoint, Byte data)
		{
			this.endpoint = endpoint;
			this.data = data;
		}
	}
}
