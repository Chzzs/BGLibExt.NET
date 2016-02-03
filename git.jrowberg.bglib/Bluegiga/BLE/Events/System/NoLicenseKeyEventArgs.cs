using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.System
{
	public delegate void NoLicenseKeyEventHandler (object sender, NoLicenseKeyEventArgs e);

	public class NoLicenseKeyEventArgs : EventArgs
	{
		public NoLicenseKeyEventArgs ()
		{
		}
	}
}
