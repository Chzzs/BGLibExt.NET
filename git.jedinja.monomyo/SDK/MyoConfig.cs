using System;

namespace git.jedinja.monomyo.SDK
{
	public class MyoConfig
	{
		public bool Debug  { get; private set; }
		public int NotificationThreadSleep  { get; private set; }
		public int PortThreadSleep  { get; private set; }

		public MyoConfig (bool debug = false, int notificationThreadSleep = 100, int portThreadSleep = 20)
		{
			this.Debug = debug;
			this.NotificationThreadSleep = notificationThreadSleep;
			this.PortThreadSleep = portThreadSleep;
		}
	}
}

