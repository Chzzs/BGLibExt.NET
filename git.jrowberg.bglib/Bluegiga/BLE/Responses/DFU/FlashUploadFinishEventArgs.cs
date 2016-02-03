using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.DFU
{
	public delegate void FlashUploadFinishEventHandler (object sender, FlashUploadFinishEventArgs e);

	public class FlashUploadFinishEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public FlashUploadFinishEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
