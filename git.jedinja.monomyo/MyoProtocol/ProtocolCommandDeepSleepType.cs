using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolCommandDeepSleepType : ProtocolCommandType
	{
		public ProtocolCommandDeepSleepType ()
		{
		}

		#region implemented abstract members of ProtocolCommandType

		protected override void Serialize (ByteSerializer bs)
		{
			// no payload
		}

		public override ProtocolCommand Command {
			get {
				return ProtocolCommand.DeepSleep;
			}
		}

		public override byte PayloadSize {
			get {
				return 0;
			}
		}

		#endregion
	}
}

