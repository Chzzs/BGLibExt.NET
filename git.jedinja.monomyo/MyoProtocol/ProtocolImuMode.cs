using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal enum ProtocolImuMode : byte
	{
		None = 0,
		SendData = 1,
		SendEvents = 2,
		SendAll = 3,
		SendRaw = 4,
	}
}

