using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Connection
{
	public delegate void FeaturesGetEventHandler (object sender, FeaturesGetEventArgs e);

	public class FeaturesGetEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 result;

		public FeaturesGetEventArgs (Byte connection, UInt16 result)
		{
			this.connection = connection;
			this.result = result;
		}
	}
}
