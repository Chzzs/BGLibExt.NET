using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.SM
{
	public delegate void BondStatusEventHandler (object sender, BondStatusEventArgs e);

	public class BondStatusEventArgs : EventArgs
	{
		public readonly Byte bond;
		public readonly Byte keysize;
		public readonly Byte mitm;
		public readonly Byte keys;

		public BondStatusEventArgs (Byte bond, Byte keysize, Byte mitm, Byte keys)
		{
			this.bond = bond;
			this.keysize = keysize;
			this.mitm = mitm;
			this.keys = keys;
		}
	}
}
