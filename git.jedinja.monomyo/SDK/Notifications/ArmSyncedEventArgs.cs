using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jedinja.monomyo.SDK.Notifications
{
    public delegate void ArmSyncedEventHandler(object sender, ArmSyncedEventArgs e);

    public class ArmSyncedEventArgs : NotificationEventArgs
    {
        public Arm ArmUsed { get; private set; }
        public BraceletDirection BraceletOrientation { get; private set; }

        public ArmSyncedEventArgs(DateTime stamp, Arm arm, BraceletDirection direction)
            : base(stamp)
        {
            this.ArmUsed = arm;
            this.BraceletOrientation = direction;
        }
    }
}
