using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.SM
{
	public delegate void SetOobDataEventHandler (object sender, SetOobDataEventArgs e);

	public class SetOobDataEventArgs : EventArgs
	{
		public SetOobDataEventArgs ()
		{
		}
	}
}
