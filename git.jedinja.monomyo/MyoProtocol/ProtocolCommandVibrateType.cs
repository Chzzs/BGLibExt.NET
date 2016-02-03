using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolCommandVibrateType : ProtocolCommandType
	{
		public ProtocolVibration Vibrate { get; set; }

		public ProtocolCommandVibrateType (ProtocolVibration vibration)
		{
			this.Vibrate = vibration;
		}

		#region implemented abstract members of ProtocolCommandType

		protected override void Serialize (ByteSerializer bs)
		{
			bs.Serialize (Vibrate);
		}

		public override ProtocolCommand Command {
			get {
				return ProtocolCommand.Vibrate;
			}
		}

		public override byte PayloadSize {
			get {
				return 1;
			}
		}

		#endregion
	}
}

