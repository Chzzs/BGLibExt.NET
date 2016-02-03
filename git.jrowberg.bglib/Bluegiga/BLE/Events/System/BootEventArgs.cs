using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jrowberg.bglib.Bluegiga.BLE.Events.System
{
	public delegate void BootEventHandler (object sender, BootEventArgs e);

	public class BootEventArgs : EventArgs
	{
		public readonly UInt16 major;
		public readonly UInt16 minor;
		public readonly UInt16 patch;
		public readonly UInt16 build;
		public readonly UInt16 ll_version;
		public readonly Byte protocol_version;
		public readonly Byte hw;

		public BootEventArgs (UInt16 major, UInt16 minor, UInt16 patch, UInt16 build, UInt16 ll_version, Byte protocol_version, Byte hw)
		{
			this.major = major;
			this.minor = minor;
			this.patch = patch;
			this.build = build;
			this.ll_version = ll_version;
			this.protocol_version = protocol_version;
			this.hw = hw;
		}
	}
}
