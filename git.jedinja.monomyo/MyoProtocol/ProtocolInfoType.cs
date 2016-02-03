using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolInfoType : IByteSerializable
	{
		private const int SERIAL_NUMBER_LENGTH = 6;
		private Bytes _serialNumber = new byte[SERIAL_NUMBER_LENGTH];

		public Bytes SerialNumber {
			get {
				return _serialNumber;
			}
			set {
				if (value == null || value.Length != SERIAL_NUMBER_LENGTH)
				{
					throw new ArgumentOutOfRangeException ("value", "value should contain 6 bytes!");
				}

				_serialNumber = value;
			}
		}

		public ProtocolClassifierPoses UnlockPose { get; set; }

		public ProtocolClasifierModel ActiveClassifierType { get; set; }

		public byte ActiveClassifierIndex { get; set; }

		public bool HasCustomClassifier { get; set; }

		public byte StreamIndicating { get; set; }

		public ProtocolSku Sku { get; set; }

		public Bytes Reserved { get; set; }

		private const int RESERVED_LENGTH = 7;

		public ProtocolInfoType ()
		{
			this.Reserved = new byte[RESERVED_LENGTH];
		}

		#region IByteSerializable implementation

		public Bytes Serialize ()
		{
			ByteSerializer bs = new ByteSerializer ();

			// ORDER IS MANDATORY
			bs.Serialize (SerialNumber);
			bs.Serialize (UnlockPose);
			bs.Serialize (ActiveClassifierType);
			bs.Serialize (ActiveClassifierIndex);
			bs.Serialize (HasCustomClassifier);
			bs.Serialize (StreamIndicating);
			bs.Serialize (Sku);
			bs.Serialize (Reserved);

			return bs.GetBuffer ();
		}

		public void DeSerialize (Bytes bytes)
		{
			using (ByteDeserializer bd = new ByteDeserializer (bytes))
			{
				// ORDER IS MANDATORY
				SerialNumber = bd.DeSerializeBytes (SERIAL_NUMBER_LENGTH);
				UnlockPose = bd.DeSerializePose ();
				ActiveClassifierType = bd.DeSerializeClassifierModel ();
				ActiveClassifierIndex = bd.DeSerializeByte ();
				HasCustomClassifier = bd.DeSerializeBool ();
				StreamIndicating = bd.DeSerializeByte ();
				Sku = bd.DeSerializeSku ();
				Reserved = bd.DeSerializeBytes (RESERVED_LENGTH);
			}
		}

		#endregion
	}
}

