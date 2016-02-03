using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.SM
{
	public delegate void EncryptStartEventHandler (object sender, EncryptStartEventArgs e);

	public class EncryptStartEventArgs : EventArgs
	{
		public readonly Byte handle;
		public readonly UInt16 result;

		public EncryptStartEventArgs (Byte handle, UInt16 result)
		{
			this.handle = handle;
			this.result = result;
		}
	}
}
