using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jedinja.monomyo.SDK.Notifications
{
    public delegate void TapDetectedEventHandler(object sender, TapDetectedEventArgs e);

    public class TapDetectedEventArgs : NotificationEventArgs
    {
        public byte Direction { get; private set; }
        public byte Count { get; private set; }

        public TapDetectedEventArgs(DateTime stamp, byte tapDirection, byte tapCount)
            : base(stamp)
        {
            this.Direction = tapDirection;
            this.Count = tapCount;
        }
    }
}
