using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal enum ProtocolClassifierPoses : UInt16
	{
		Rest = 0,
		Fist = 1,
		WaveIn = 2,
		WaveOut = 3,
		FingersSpread = 4,
		DoubleTap = 5,
		Unknown = 65535,
	}
}

