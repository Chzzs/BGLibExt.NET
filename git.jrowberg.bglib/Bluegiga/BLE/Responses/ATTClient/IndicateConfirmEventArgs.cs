using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.ATTClient
{
	public delegate void IndicateConfirmEventHandler (object sender, IndicateConfirmEventArgs e);

	public class IndicateConfirmEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public IndicateConfirmEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
