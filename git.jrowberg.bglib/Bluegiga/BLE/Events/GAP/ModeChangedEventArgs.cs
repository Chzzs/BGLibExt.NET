using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.GAP
{
	public delegate void ModeChangedEventHandler (object sender, ModeChangedEventArgs e);

	public class ModeChangedEventArgs : EventArgs
	{
		public readonly Byte discover;
		public readonly Byte connect;

		public ModeChangedEventArgs (Byte discover, Byte connect)
		{
			this.discover = discover;
			this.connect = connect;
		}
	}
}
