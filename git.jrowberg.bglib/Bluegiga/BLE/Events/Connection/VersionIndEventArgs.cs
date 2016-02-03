using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Connection
{
	public delegate void VersionIndEventHandler (object sender, VersionIndEventArgs e);

	public class VersionIndEventArgs : EventArgs
	{
		public readonly Byte connection;
		public readonly Byte vers_nr;
		public readonly UInt16 comp_id;
		public readonly UInt16 sub_vers_nr;

		public VersionIndEventArgs (Byte connection, Byte vers_nr, UInt16 comp_id, UInt16 sub_vers_nr)
		{
			this.connection = connection;
			this.vers_nr = vers_nr;
			this.comp_id = comp_id;
			this.sub_vers_nr = sub_vers_nr;
		}
	}
}
