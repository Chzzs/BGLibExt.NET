using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void EndProcedureEventHandler (object sender, EndProcedureEventArgs e);

	public class EndProcedureEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public EndProcedureEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
