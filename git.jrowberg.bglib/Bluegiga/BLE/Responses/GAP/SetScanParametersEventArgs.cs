using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void SetScanParametersEventHandler (object sender, SetScanParametersEventArgs e);

	public class SetScanParametersEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SetScanParametersEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
