using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.ATTClient
{
	public delegate void FindInformationEventHandler (object sender, FindInformationEventArgs e);

	public class FindInformationEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 result;

		public FindInformationEventArgs (Byte connection, UInt16 result)
		{
			this.connection = connection;
			this.result = result;
		}
	}
}
