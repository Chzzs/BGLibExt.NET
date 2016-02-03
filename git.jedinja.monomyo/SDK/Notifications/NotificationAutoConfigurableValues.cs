using System;

namespace git.jedinja.monomyo.SDK.Notifications
{
	public class NotificationAutoConfigurableValues
	{
		public EmgMode Emg  { get; private set; }
		public ImuMode Imu  { get; private set; }
		public MyoPoseMode Pose  { get; private set; }

		public NotificationAutoConfigurableValues (EmgMode emg, ImuMode imu, MyoPoseMode pose)
		{
			this.Emg = emg;
			this.Imu = imu;
			this.Pose = pose;
		}

		public static NotificationAutoConfigurableValues All {
			get {
				return new NotificationAutoConfigurableValues (EmgMode.Send, ImuMode.SendAll, MyoPoseMode.Enabled);
			}
		}

		public static NotificationAutoConfigurableValues PoseOnly {
			get {
				return new NotificationAutoConfigurableValues (EmgMode.None, ImuMode.None, MyoPoseMode.Enabled);
			}
		}
	}
}

