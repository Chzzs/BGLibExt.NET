using System;

namespace git.jedinja.monomyo.SDK
{
	public class BatteryLevel
	{
		public byte Value { get; private set; }

		internal BatteryLevel (Bytes bytes)
		{
			this.Value = bytes.ToArray ()[0];
		}
	}
}

