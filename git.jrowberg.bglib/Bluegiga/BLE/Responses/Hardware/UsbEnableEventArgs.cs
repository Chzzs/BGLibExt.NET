using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void UsbEnableEventHandler (object sender, UsbEnableEventArgs e);

	public class UsbEnableEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public UsbEnableEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
