using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace git.jrowberg.bglib.Bluegiga.BLE.Responses.Flash
{
	public delegate void PSSaveEventHandler (object sender, PSSaveEventArgs e);

	public class PSSaveEventArgs : EventArgs
	{
		public readonly UInt16 result;

		public PSSaveEventArgs (UInt16 result)
		{
			this.result = result;
		}
	}
}
