using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal enum ProtocolClassifierEvent : byte
	{
		ArmSynced = 1,
		ArmUnsynced = 2,
		Pose = 3,
		Unlocked = 4,
		Locked = 5,
		SyncFailed = 6,
	}
}

