using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void EndpointRXEventHandler (object sender, EndpointRXEventArgs e);

	public class EndpointRXEventArgs : EventArgs
	{
		public readonly UInt16 result;
		public readonly Byte[] data;

		public EndpointRXEventArgs (UInt16 result, Byte[] data)
		{
			this.result = result;
			this.data = data;
		}
	}
}
