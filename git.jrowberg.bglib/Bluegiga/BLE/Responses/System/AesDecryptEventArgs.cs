using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.System
{
	public delegate void AesDecryptEventHandler (object sender, AesDecryptEventArgs e);

	public class AesDecryptEventArgs : EventArgs
	{
		public readonly Byte[] data;

		public AesDecryptEventArgs (Byte[] data)
		{
			this.data = data;
		}
	}
}
