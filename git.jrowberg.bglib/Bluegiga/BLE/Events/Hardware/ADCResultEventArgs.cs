using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.Hardware
{
	public delegate void ADCResultEventHandler (object sender, ADCResultEventArgs e);

	public class ADCResultEventArgs : EventArgs
	{
		public readonly Byte input;
		public readonly Int16 value;

		public ADCResultEventArgs (Byte input, Int16 value)
		{
			this.input = input;
			this.value = value;
		}
	}
}
