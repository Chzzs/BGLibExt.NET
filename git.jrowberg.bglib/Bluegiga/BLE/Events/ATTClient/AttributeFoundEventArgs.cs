using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.ATTClient
{
	public delegate void AttributeFoundEventHandler (object sender, AttributeFoundEventArgs e);

	public class AttributeFoundEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly UInt16 chrdecl;
		public readonly UInt16 value;
		public readonly Byte properties;
		public readonly Byte[] uuid;

		public AttributeFoundEventArgs (Byte connection, UInt16 chrdecl, UInt16 value, Byte properties, Byte[] uuid)
		{
			this.connection = connection;
			this.chrdecl = chrdecl;
			this.value = value;
			this.properties = properties;
			this.uuid = uuid;
		}
	}
}
