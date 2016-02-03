using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.SM
{
	public delegate void SetBondableModeEventHandler (object sender, SetBondableModeEventArgs e);

	public class SetBondableModeEventArgs : EventArgs
	{
		public SetBondableModeEventArgs ()
		{
		}
	}
}
