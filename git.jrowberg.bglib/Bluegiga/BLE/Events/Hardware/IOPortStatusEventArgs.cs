using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Hardware
{
	public delegate void IOPortStatusEventHandler (object sender, IOPortStatusEventArgs e);

	public class IOPortStatusEventArgs : EventArgs
	{
		public readonly UInt32 timestamp;
		public readonly Byte port;
		public readonly Byte irq;
		public readonly Byte state;

		public IOPortStatusEventArgs (UInt32 timestamp, Byte port, Byte irq, Byte state)
		{
			this.timestamp = timestamp;
			this.port = port;
			this.irq = irq;
			this.state = state;
		}
	}
}
