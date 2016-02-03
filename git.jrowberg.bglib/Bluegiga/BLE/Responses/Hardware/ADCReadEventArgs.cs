using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void ADCReadEventHandler (object sender, ADCReadEventArgs e);

	public class ADCReadEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public ADCReadEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
