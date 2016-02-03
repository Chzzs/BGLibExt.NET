using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal enum ProtocolUnlockMode : byte
	{
		Lock = 0,
		UnlockTimed = 1,
		UnlockHold = 2,
	}
}

