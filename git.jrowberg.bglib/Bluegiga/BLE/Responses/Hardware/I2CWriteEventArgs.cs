using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void I2CWriteEventHandler (object sender, I2CWriteEventArgs e);

	public class I2CWriteEventArgs : EventArgs
	{
		public readonly Byte written;

		public I2CWriteEventArgs (Byte written)
		{
			this.written = written;
		}
	}
}
