using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void AesSetkeyEventHandler (object sender, AesSetkeyEventArgs e);

	public class AesSetkeyEventArgs : EventArgs
	{
		public AesSetkeyEventArgs ()
		{
		}
	}
}
