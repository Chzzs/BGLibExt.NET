using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Attributes
{
	public delegate void UserWriteResponseEventHandler (object sender, UserWriteResponseEventArgs e);

	public class UserWriteResponseEventArgs : EventArgs
	{
		public UserWriteResponseEventArgs ()
		{
		}
	}
}
