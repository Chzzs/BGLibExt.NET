using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void PSLoadEventHandler (object sender, PSLoadEventArgs e);

	public class PSLoadEventArgs : EventArgs
	{
		public readonly UInt16 result;
		public readonly Byte[] value;

		public PSLoadEventArgs (UInt16 result, Byte[] value)
		{
			this.result = result;
			this.value = value;
		}
	}
}
