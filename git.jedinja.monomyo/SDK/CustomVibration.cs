using System;

namespace git.jedinja.monomyo.SDK
{
	public class CustomVibration
	{
		/// <summary>
		/// In miliseconds
		/// </summary>
		public ushort Length { get; set; }
		public byte Power { get; set; }

		public CustomVibration ()
		{
		}

		public CustomVibration (ushort length, byte power)
		{
			this.Length = length;
			this.Power = power;
		}
	}
}

