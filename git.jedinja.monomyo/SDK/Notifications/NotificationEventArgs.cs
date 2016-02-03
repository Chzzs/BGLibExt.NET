using System;

namespace git.jedinja.monomyo.SDK.Notifications
{
    public delegate void NotificationEventHandler(object sender, NotificationEventArgs e);

	public class NotificationEventArgs : EventArgs
	{
		public DateTime Timestamp  { get; private set; }

		public NotificationEventArgs (DateTime stamp)
		{
			this.Timestamp = stamp;
		}
	}
}

