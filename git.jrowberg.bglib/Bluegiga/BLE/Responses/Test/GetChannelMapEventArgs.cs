using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Test
{
	public delegate void GetChannelMapEventHandler (object sender, GetChannelMapEventArgs e);

	public class GetChannelMapEventArgs : EventArgs
	{
		public readonly Byte[] channel_map;

		public GetChannelMapEventArgs (Byte[] channel_map)
		{
			this.channel_map = channel_map;
		}
	}
}
