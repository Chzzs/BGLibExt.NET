using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.DFU
{
	public delegate void FlashSetAddressEventHandler (object sender, FlashSetAddressEventArgs e);

	public class FlashSetAddressEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public FlashSetAddressEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
