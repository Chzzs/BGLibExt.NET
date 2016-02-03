using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolCommandSetSleepModeType : ProtocolCommandType
	{
		public ProtocolSleepMode SleepMode { get; set; }

		public ProtocolCommandSetSleepModeType (ProtocolSleepMode sleep)
		{
			this.SleepMode = sleep;
		}

		#region implemented abstract members of ProtocolCommandType

		protected override void Serialize (ByteSerializer bs)
		{
			bs.Serialize (SleepMode);
		}

		public override ProtocolCommand Command {
			get {
				return ProtocolCommand.SetSleepMode;
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

