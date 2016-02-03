using System;

namespace git.jedinja.monomyo.BleInfrastructure.BleNotifications
{
	internal delegate void CharacteristicValueChangedEventHandler (object sender, CharacteristicValueChangedEventArgs e);

	internal class CharacteristicValueChangedEventArgs : EventArgs
	{
		public Bytes CharacteristicUUID  { get; private set; }
		public Bytes Value  { get; private set; }

		public CharacteristicValueChangedEventArgs (Bytes uuid, Bytes value)
		{
			this.CharacteristicUUID = uuid;
			this.Value = value;
		}
	}
}

