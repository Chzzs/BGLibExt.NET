using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolCommandUserActionType : ProtocolCommandType
	{
		public ProtocolUserAction UserAction { get; set; }

		public ProtocolCommandUserActionType (ProtocolUserAction action)
		{
			this.UserAction = action;
		}

		#region implemented abstract members of ProtocolCommandType

		protected override void Serialize (ByteSerializer bs)
		{
			bs.Serialize (UserAction);
		}

		public override ProtocolCommand Command {
			get {
				return ProtocolCommand.UserAction;
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

