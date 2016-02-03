using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void AddressGetEventHandler (object sender, AddressGetEventArgs e);

	public class AddressGetEventArgs : EventArgs
	{
		public readonly Byte[] address;

		public AddressGetEventArgs (Byte[] address)
		{
			this.address = address;
		}
	}
}
