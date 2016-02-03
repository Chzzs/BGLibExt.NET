using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void WriteDataEventHandler (object sender, WriteDataEventArgs e);

	public class WriteDataEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public WriteDataEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
