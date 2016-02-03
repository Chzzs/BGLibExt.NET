using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Attributes
{
	public delegate void UserReadResponseEventHandler (object sender, UserReadResponseEventArgs e);

	public class UserReadResponseEventArgs : EventArgs
	{
		public UserReadResponseEventArgs ()
		{
		}
	}
}
