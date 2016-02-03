using System;

namespace git.jedinja.monomyo.SDK.Notifications
{
	public delegate void BatteryChangeEventHandler (object sender, BatteryChangeEventArgs e) ;

	public class BatteryChangeEventArgs : NotificationEventArgs
	{
		public BatteryLevel Level { get; private set; }

		public BatteryChangeEventArgs (DateTime stamp, BatteryLevel level)
			: base (stamp)
		{
			this.Level = level;
		}
	}
}

