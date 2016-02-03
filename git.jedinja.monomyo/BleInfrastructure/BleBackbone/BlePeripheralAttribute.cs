using System;

namespace git.jedinja.monomyo.BleInfrastructure.BleBackbone
{
	internal class BlePeripheralAttribute
	{
		public Bytes AttributeUUID  { get; private set; }
		public ushort Handle  { get; private set; }

		public BlePeripheralAttribute (Bytes uuid, ushort handle)
		{
			this.AttributeUUID = uuid;
			this.Handle = handle;
		}

		#region protocol constants

		public static Bytes SERVICE_UUID = new Bytes (0x00, 0x28);
		public static Bytes CHARACTERISTIC_UUID = new Bytes (0x03, 0x28);
		public static Bytes CHARACTERISTIC_CCC_UUID = new Bytes (0x02, 0x29);

		#endregion
	}
}

