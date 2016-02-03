using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void SPITransferEventHandler (object sender, SPITransferEventArgs e);

	public class SPITransferEventArgs : EventArgs
	{
		public readonly UInt16 result;
		public readonly Byte channel;
		public readonly Byte[] data;

		public SPITransferEventArgs (UInt16 result, Byte channel, Byte[] data)
		{
			this.result = result;
			this.channel = channel;
			this.data = data;
		}
	}
}
