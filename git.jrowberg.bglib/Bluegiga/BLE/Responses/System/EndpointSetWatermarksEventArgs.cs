using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void EndpointSetWatermarksEventHandler (object sender, EndpointSetWatermarksEventArgs e);

	public class EndpointSetWatermarksEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public EndpointSetWatermarksEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
