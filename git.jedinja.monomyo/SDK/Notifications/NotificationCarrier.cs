using System;

namespace git.jedinja.monomyo.SDK.Notifications
{
	internal class NotificationCarrier
	{
		public Bytes CharacteristicUUID  { get; private set; }
		public DateTime Timestamp  { get; private set; }
		public Bytes CharacteristicValue  { get; private set; }

		public NotificationCarrier (Bytes uuid, DateTime timestamp, Bytes value)
		{
			this.CharacteristicUUID = uuid;
			this.Timestamp = timestamp;
			this.CharacteristicValue = value;
		}
	}
}

