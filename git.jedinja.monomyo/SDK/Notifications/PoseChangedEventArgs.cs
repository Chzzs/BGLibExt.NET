using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jedinja.monomyo.SDK.Notifications
{
    public delegate void PoseChangedEventHandler(object sender, PoseChangedEventArgs e);

    public class PoseChangedEventArgs : NotificationEventArgs
    {
        public Pose NewPose { get; private set; }

        public PoseChangedEventArgs(DateTime stamp, Pose pose)
            :base(stamp)
        {
            this.NewPose = pose;
        }
    }
}
