using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Attributes
{
	public delegate void SendEventHandler (object sender, SendEventArgs e);

	public class SendEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SendEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
