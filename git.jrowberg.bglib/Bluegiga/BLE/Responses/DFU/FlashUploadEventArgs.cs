using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.DFU
{
	public delegate void FlashUploadEventHandler (object sender, FlashUploadEventArgs e);

	public class FlashUploadEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public FlashUploadEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
