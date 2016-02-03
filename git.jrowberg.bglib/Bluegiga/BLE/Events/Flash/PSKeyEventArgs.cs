using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Flash
{
	public delegate void PSKeyEventHandler (object sender, PSKeyEventArgs e);

	public class PSKeyEventArgs : EventArgs
	{
		public readonly UInt16 key;
		public readonly Byte[] value;

		public PSKeyEventArgs (UInt16 key, Byte[] value)
		{
			this.key = key;
			this.value = value;
		}
	}
}
