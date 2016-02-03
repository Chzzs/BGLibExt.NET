using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolCommandUnlockType : ProtocolCommandType
	{
		public ProtocolUnlockMode Unlock { get; set; }

		public ProtocolCommandUnlockType (ProtocolUnlockMode mode)
		{
			this.Unlock = mode;
		}

		#region implemented abstract members of ProtocolCommandType

		protected override void Serialize (ByteSerializer bs)
		{
			bs.Serialize (Unlock);
		}

		public override ProtocolCommand Command {
			get {
				return ProtocolCommand.Unlock;
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

