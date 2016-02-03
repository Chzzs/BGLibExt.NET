using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.SM
{
	public delegate void DeleteBondingEventHandler (object sender, DeleteBondingEventArgs e);

	public class DeleteBondingEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public DeleteBondingEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
