using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Attributes
{
	public delegate void WriteEventHandler (object sender, WriteEventArgs e);

	public class WriteEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public WriteEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
