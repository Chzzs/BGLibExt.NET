using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient
{
	public delegate void IndicatedEventHandler (object sender, IndicatedEventArgs e);

	public class IndicatedEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 attrhandle;

		public IndicatedEventArgs (Byte connection, UInt16 attrhandle)
		{
			this.connection = connection;
			this.attrhandle = attrhandle;
		}
	}
}
