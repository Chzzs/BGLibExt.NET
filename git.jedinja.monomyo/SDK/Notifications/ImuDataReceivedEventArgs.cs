using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jedinja.monomyo.SDK.Notifications
{
    public delegate void ImuDataReceivedEventHandler(object sender, ImuDataReceivedEventArgs e);

    public class ImuDataReceivedEventArgs : NotificationEventArgs
    {
        public ImuDataOrientation Orientation { get; private set; }
        public short[] Acceleration { get; private set; }
        public short[] GyroscopeData { get; private set; }

        public ImuDataReceivedEventArgs(DateTime stamp, ImuDataOrientation orientation, short[] acceleration, short[] gyroscopeData)
            : base(stamp)
        {
            this.Orientation = orientation;
            this.Acceleration = acceleration;
            this.GyroscopeData = gyroscopeData;
        }
    }
}
