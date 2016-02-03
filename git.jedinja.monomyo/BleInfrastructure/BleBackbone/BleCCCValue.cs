using System;

namespace git.jedinja.monomyo.BleInfrastructure.BleBackbone
{
	[Flags]
	internal enum BleCCCValue : ushort
	{
		None = 0x00,
		NotificationsEnabled = 0x01,
		IndicationsEnabled = 0x02,
	}
}

