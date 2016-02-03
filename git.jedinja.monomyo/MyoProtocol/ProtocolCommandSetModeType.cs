using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolCommandSetModeType : ProtocolCommandType
	{
		public ProtocolEmgMode EmgMode { get; set; }

		public ProtocolImuMode ImuMode { get; set; }

		public ProtocolClassifierMode ClassifierMode { get; set; }

		public ProtocolCommandSetModeType (ProtocolEmgMode emg, ProtocolImuMode imu, ProtocolClassifierMode classifier)
		{
			this.EmgMode = emg;
			this.ImuMode = imu;
			this.ClassifierMode = classifier;
		}

		#region implemented abstract members of ProtocolCommandType

		protected override void Serialize (ByteSerializer bs)
		{
			bs.Serialize (EmgMode);
			bs.Serialize (ImuMode);
			bs.Serialize (ClassifierMode);
		}

		public override ProtocolCommand Command {
			get {
				return ProtocolCommand.SetMode;
			}
		}

		public override byte PayloadSize {
			get {
				return 3;
			}
		}

		#endregion
	}
}

