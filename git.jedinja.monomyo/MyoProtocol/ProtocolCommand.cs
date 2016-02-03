using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal enum ProtocolCommand : byte
	{
		SetMode = 1,
		Vibrate = 3,
		DeepSleep = 4,
		Vibrate2 = 7,
		SetSleepMode = 9,
		Unlock = 0x0A,
		UserAction = 0x0B,
	}
}

