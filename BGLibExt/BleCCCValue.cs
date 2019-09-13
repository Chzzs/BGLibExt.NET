using System;

namespace BGLibExt
{
    [Flags]
    public enum BleCCCValue : ushort
    {
        None = 0x00,
        NotificationsEnabled = 0x01,
        IndicationsEnabled = 0x02,
    }
}
