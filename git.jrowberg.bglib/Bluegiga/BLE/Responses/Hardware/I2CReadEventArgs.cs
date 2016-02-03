using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void I2CReadEventHandler (object sender, I2CReadEventArgs e);

	public class I2CReadEventArgs : EventArgs
	{
		public readonly UInt16 result;
		public readonly Byte[] data;

		public I2CReadEventArgs (UInt16 result, Byte[] data)
		{
			this.result = result;
			this.data = data;
		}
	}
}
