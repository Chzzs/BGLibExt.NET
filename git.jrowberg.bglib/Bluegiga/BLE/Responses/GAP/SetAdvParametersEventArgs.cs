using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void SetAdvParametersEventHandler (object sender, SetAdvParametersEventArgs e);

	public class SetAdvParametersEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public SetAdvParametersEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
