using System;

namespace git.jedinja.monomyo.SDK
{
	public class FirmwareVersion
	{
		public int Major { get; private set; }
		public int Minor { get; private set; }
		public int Patch { get; private set; }
		public HardwareRevision Revision { get; set; }

		public FirmwareVersion (int major, int minor, int patch, HardwareRevision revision)
		{
			this.Major = major;
			this.Minor = minor;
			this.Patch = patch;
			this.Revision = revision;
		}
	}
}

