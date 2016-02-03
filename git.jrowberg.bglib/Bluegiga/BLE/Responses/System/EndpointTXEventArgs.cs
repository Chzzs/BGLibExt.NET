using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void EndpointTXEventHandler (object sender, EndpointTXEventArgs e);

	public class EndpointTXEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public EndpointTXEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
