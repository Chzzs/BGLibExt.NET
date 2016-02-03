using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Hardware
{
	public delegate void SetTxpowerEventHandler (object sender, SetTxpowerEventArgs e);

	public class SetTxpowerEventArgs : EventArgs
	{
		public SetTxpowerEventArgs ()
		{
		}
	}
}
