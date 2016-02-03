using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolMotionEventType : IByteSerializable
	{
		public ProtocolMotionEvent Event { get; set; }

		public byte TapDirection { get; set; }

		public byte TapCount { get; set; }

		public ProtocolMotionEventType ()
		{
		}

		#region IByteSerializable implementation

		public Bytes Serialize ()
		{
			ByteSerializer bs = new ByteSerializer ();

			bs.Serialize (Event);
			bs.Serialize (TapDirection);
			bs.Serialize (TapCount);

			return bs.GetBuffer ();
		}

		public void DeSerialize (Bytes bytes)
		{
			using (ByteDeserializer bd = new ByteDeserializer (bytes))
			{
				Event = bd.DeSerializeMotionEvent ();
				TapDirection = bd.DeSerializeByte ();
				TapCount = bd.DeSerializeByte ();
			}
		}

		#endregion
	}
}

