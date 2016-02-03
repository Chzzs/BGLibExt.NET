using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jedinja.monomyo.SDK.Notifications
{
    public delegate void EmgDataReceivedEventHandler(object sender, EmgDataReceivedEventArgs e);

    public class EmgDataReceivedEventArgs : NotificationEventArgs
    {
        public byte[] Data { get; private set; }
        public byte SensorNumber { get; private set; }

        public EmgDataReceivedEventArgs(DateTime timestamp, byte[] sensorData, byte sensorNumber)
            : base(timestamp)
        {
            this.Data = sensorData;
            this.SensorNumber = sensorNumber;
        }
    }
}
