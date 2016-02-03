using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.System
{
	public delegate void EndpointWatermarkRXEventHandler (object sender, EndpointWatermarkRXEventArgs e);

	public class EndpointWatermarkRXEventArgs : EventArgs
	{
		public readonly Byte endpoint;
		public readonly Byte data;

		public EndpointWatermarkRXEventArgs (Byte endpoint, Byte data)
		{
			this.endpoint = endpoint;
			this.data = data;
		}
	}
}
