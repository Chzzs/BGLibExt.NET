using System;
using System.Collections.Generic;

namespace git.jedinja.monomyo.BleInfrastructure.BleBackbone
{
	internal class BlePeripheralService
	{
		internal List<BlePeripheralAttribute> Attributes { get; private set; }

		public List<BlePeripheralCharacteristic> Characteristics  { get; private set; }
		public Bytes ServiceUUID  { get; private set; }
		public ushort StartHandle  { get; private set; }
		public ushort EndHandle  { get; private set; }

		public BlePeripheralService (Bytes UUID, ushort startHandle, ushort endHandle)
		{
			this.Attributes = new List<BlePeripheralAttribute> ();
			this.Characteristics = new List<BlePeripheralCharacteristic> ();

			this.ServiceUUID = UUID;
			this.StartHandle = startHandle;
			this.EndHandle = endHandle;
		}
	}
}

