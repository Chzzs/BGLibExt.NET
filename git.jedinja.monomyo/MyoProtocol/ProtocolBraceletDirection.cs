using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal enum ProtocolBraceletDirection : byte
	{
		TowardWrist = 1,
		TowardElbow = 2,
		Unknown = 255,
	}
}

