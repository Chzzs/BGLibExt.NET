using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.SM
{
	public delegate void GetBondsEventHandler (object sender, GetBondsEventArgs e);

	public class GetBondsEventArgs : EventArgs
	{
		public readonly Byte bonds;

		public GetBondsEventArgs (Byte bonds)
		{
			this.bonds = bonds;
		}
	}
}
