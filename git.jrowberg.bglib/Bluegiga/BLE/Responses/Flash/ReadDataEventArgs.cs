using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void ReadDataEventHandler (object sender, ReadDataEventArgs e);

	public class ReadDataEventArgs : EventArgs
	{
		public readonly Byte[] data;

		public ReadDataEventArgs (Byte[] data)
		{
			this.data = data;
		}
	}
}
