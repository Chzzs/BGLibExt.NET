using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void HelloEventHandler (object sender, HelloEventArgs e);

	public class HelloEventArgs : EventArgs
	{
		public HelloEventArgs ()
		{
		}
	}
}
