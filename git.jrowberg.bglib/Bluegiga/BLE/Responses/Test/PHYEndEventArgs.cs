using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Test
{
	public delegate void PHYEndEventHandler (object sender, PHYEndEventArgs e);

	public class PHYEndEventArgs : EventArgs
	{
		public readonly UInt16 counter;

		public PHYEndEventArgs (UInt16 counter)
		{
			this.counter = counter;
		}
	}
}
