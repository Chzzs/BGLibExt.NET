using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolClassifierEventType : IByteSerializable
	{
		public ProtocolClassifierEvent Event { get; set; }
		// Always present

		// Arm and direction OR
		public ProtocolArm Arm { get; set; }

		public ProtocolBraceletDirection Direction { get; set; }

		// Pose OR
		public ProtocolClassifierPoses Pose { get; set; }

		// Sync
		public ProtocolSyncResult SyncResult { get; set; }

		public ProtocolClassifierEventType ()
		{
		}

		#region IByteSerializable implementation

		public Bytes Serialize ()
		{
			throw new NotImplementedException ();
		}

		public void DeSerialize (Bytes bytes)
		{
			using (ByteDeserializer bd = new ByteDeserializer (bytes))
			{
				Event = bd.DeSerializeClassifierEvent ();

				switch (Event)
				{
				case ProtocolClassifierEvent.ArmSynced:
					{
						Arm = bd.DeSerializeArm ();
						Direction = bd.DeSerializeBraceletDirection ();
						break;
					}
				case ProtocolClassifierEvent.Pose:
					{
						Pose = bd.DeSerializePose ();
						break;
					}
				case ProtocolClassifierEvent.SyncFailed:
					{
						SyncResult = bd.DeSerializeSyncResult ();
						break;
					}
				}
			}
		}

		#endregion
	}
}

