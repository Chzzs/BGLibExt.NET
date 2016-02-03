using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void AesEncryptEventHandler (object sender, AesEncryptEventArgs e);

	public class AesEncryptEventArgs : EventArgs
	{
		public readonly Byte[] data;

		public AesEncryptEventArgs (Byte[] data)
		{
			this.data = data;
		}
	}
}
