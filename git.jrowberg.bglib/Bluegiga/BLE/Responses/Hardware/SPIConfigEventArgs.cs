using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void SPIConfigEventHandler (object sender, SPIConfigEventArgs e);

	public class SPIConfigEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SPIConfigEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
