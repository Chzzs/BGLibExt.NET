using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolVersionType : IByteSerializable
	{
		public ushort Major {
			get;
			set;
		}

		public ushort Minor {
			get;
			set;
		}

		public ushort Patch {
			get;
			set;
		}

		public ProtocolHardwareRevision HardwareRevision {
			get;
			set;
		}

		public ProtocolVersionType ()
		{
		}

		#region IByteSerializable implementation

		public Bytes Serialize ()
		{
			ByteSerializer bs = new ByteSerializer ();

			// ORDER IS MANDATORY
			bs.Serialize (Major);
			bs.Serialize (Minor);
			bs.Serialize (Patch);
			bs.Serialize (HardwareRevision);

			return bs.GetBuffer ();
		}

		public void DeSerialize (Bytes bytes)
		{
			using (ByteDeserializer bd = new ByteDeserializer (bytes))
			{
				// ORDER IS MANDATORY
				Major = bd.DeSerializeUshort ();
				Minor = bd.DeSerializeUshort ();
				Patch = bd.DeSerializeUshort ();
				HardwareRevision = bd.DeSerializeHardwareRevision ();
			}
		}

		#endregion
	}
}

