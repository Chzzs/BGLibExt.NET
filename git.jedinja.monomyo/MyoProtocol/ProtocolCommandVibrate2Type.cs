using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolCommandVibrate2Type : ProtocolCommandType
	{
		public const int VIBRATION_STEPS_COUNT = 6;

		public ReadOnlyCollection<VibrationStep> Steps { get; }

		public ProtocolCommandVibrate2Type ()
		{
			List<VibrationStep> s = new List<VibrationStep> ();
			for (int i = 0; i < VIBRATION_STEPS_COUNT; i++)
			{
				s.Add (new VibrationStep (0, 0));
			}

			Steps = s.AsReadOnly ();
		}

		#region implemented abstract members of ProtocolCommandType

		protected override void Serialize (ByteSerializer bs)
		{
			foreach (VibrationStep step in Steps)
			{
				bs.Serialize (step.Duration);
				bs.Serialize (step.Strength);
			}
		}

		public override ProtocolCommand Command {
			get {
				return ProtocolCommand.Vibrate2;
			}
		}

		public override byte PayloadSize {
			get {
				return 18;
			}
		}

		#endregion

		public class VibrationStep
		{
			public VibrationStep (ushort duration, byte strength)
			{
				Strength = strength;
				Duration = duration;
			}

			/// <summary>
			/// In ms
			/// </summary>
			public ushort Duration { get; set; }

			public byte Strength { get; set; }
		}
	}
}

