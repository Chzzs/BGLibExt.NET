using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient
{
	public delegate void ProcedureCompletedEventHandler (object sender, ProcedureCompletedEventArgs e);

	public class ProcedureCompletedEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 result;
		public readonly UInt16 chrhandle;

		public ProcedureCompletedEventArgs (Byte connection, UInt16 result, UInt16 chrhandle)
		{
			this.connection = connection;
			this.result = result;
			this.chrhandle = chrhandle;
		}
	}
}
