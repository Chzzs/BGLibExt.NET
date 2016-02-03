using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void GetCountersEventHandler (object sender, GetCountersEventArgs e);

	public class GetCountersEventArgs : EventArgs
	{
		public readonly Byte txok;
		public readonly Byte txretry;
		public readonly Byte rxok;
		public readonly Byte rxfail;
		public readonly Byte mbuf;

		public GetCountersEventArgs (Byte txok, Byte txretry, Byte rxok, Byte rxfail, Byte mbuf)
		{
			this.txok = txok;
			this.txretry = txretry;
			this.rxok = rxok;
			this.rxfail = rxfail;
			this.mbuf = mbuf;
		}
	}
}
