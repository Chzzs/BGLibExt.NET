using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.SM
{
	public delegate void BondingFailEventHandler (object sender, BondingFailEventArgs e);

	public class BondingFailEventArgs : EventArgs
	{
		public readonly Byte handle;
		public readonly UInt16 result;

		public BondingFailEventArgs (Byte handle, UInt16 result)
		{
			this.handle = handle;
			this.result = result;
		}
	}
}
