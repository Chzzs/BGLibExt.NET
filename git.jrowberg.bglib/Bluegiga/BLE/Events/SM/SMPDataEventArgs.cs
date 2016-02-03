using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.SM
{
	public delegate void SMPDataEventHandler (object sender, SMPDataEventArgs e);

	public class SMPDataEventArgs : EventArgs
	{
		public readonly Byte handle;
		public readonly Byte packet;
		public readonly Byte[] data;

		public SMPDataEventArgs (Byte handle, Byte packet, Byte[] data)
		{
			this.handle = handle;
			this.packet = packet;
			this.data = data;
		}
	}
}
