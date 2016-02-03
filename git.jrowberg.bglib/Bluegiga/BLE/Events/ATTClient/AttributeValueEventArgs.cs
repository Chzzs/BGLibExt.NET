using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient
{
	public delegate void AttributeValueEventHandler (object sender, AttributeValueEventArgs e);

	public class AttributeValueEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 atthandle;
		public readonly Byte type;
		public readonly Byte[] value;

		public AttributeValueEventArgs (Byte connection, UInt16 atthandle, Byte type, Byte[] value)
		{
			this.connection = connection;
			this.atthandle = atthandle;
			this.type = type;
			this.value = value;
		}
	}
}
