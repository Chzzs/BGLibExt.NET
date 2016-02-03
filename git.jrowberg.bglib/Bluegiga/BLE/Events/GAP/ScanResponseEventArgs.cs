using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.GAP
{
	public delegate void ScanResponseEventHandler (object sender, ScanResponseEventArgs e);

	public class ScanResponseEventArgs : EventArgs
	{
		public readonly SByte rssi;
		public readonly Byte packet_type;
		public readonly Byte[] sender;
		public readonly Byte address_type;
		public readonly Byte bond;
		public readonly Byte[] data;

		public ScanResponseEventArgs (SByte rssi, Byte packet_type, Byte[] sender, Byte address_type, Byte bond, Byte[] data)
		{
			this.rssi = rssi;
			this.packet_type = packet_type;
			this.sender = sender;
			this.address_type = address_type;
			this.bond = bond;
			this.data = data;
		}
	}
}
