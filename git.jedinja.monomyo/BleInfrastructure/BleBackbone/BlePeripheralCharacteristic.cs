using System;

namespace git.jedinja.monomyo.BleInfrastructure.BleBackbone
{
	internal class BlePeripheralCharacteristic
	{
		public Bytes AttributeUUID  { get; private set; }
		public ushort Handle  { get; private set; }
		public ushort HandleCCC { get; private set; }
		public bool HasCCC  { get; private set; }

		public BlePeripheralCharacteristic (Bytes uuid, ushort handle)
		{
			this.AttributeUUID = uuid;
			this.Handle = handle;
		}

		public void SetCCCHandle (ushort handle)
		{
			this.HandleCCC = handle;
			this.HasCCC = true;
		}
	}
}

