using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.SM
{
	public delegate void WhitelistBondsEventHandler (object sender, WhitelistBondsEventArgs e);

	public class WhitelistBondsEventArgs : EventArgs
	{
		public readonly UInt16 result;
		public readonly Byte count;

		public WhitelistBondsEventArgs (UInt16 result, Byte count)
		{
			this.result = result;
			this.count = count;
		}
	}
}
