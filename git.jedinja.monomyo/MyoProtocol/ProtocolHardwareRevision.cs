using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal enum ProtocolHardwareRevision : ushort
	{
		Unknown = 0,
		/// <summary>
		/// Myo Alpha
		/// </summary>
		Rev_C = 1,
		/// <summary>
		/// Myo
		/// </summary>
		Rev_D = 2,
	}
}

