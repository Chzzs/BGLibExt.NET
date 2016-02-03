using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.GAP
{
	public delegate void SetPrivacyFlagsEventHandler (object sender, SetPrivacyFlagsEventArgs e);

	public class SetPrivacyFlagsEventArgs : EventArgs
	{
		public SetPrivacyFlagsEventArgs ()
		{
		}
	}
}
