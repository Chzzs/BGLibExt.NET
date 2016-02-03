using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolEmgDataType : IByteSerializable
	{
		private const int EMG_DATA_SIZE = 16;
		private Bytes _emgData = new byte[EMG_DATA_SIZE];

		public Bytes EmgData {
			get {
				return _emgData;
			}
			set {
				if (value == null || value.Length != EMG_DATA_SIZE)
				{
					throw new ArgumentOutOfRangeException ("value", "value must have size of " + EMG_DATA_SIZE.ToString ());
				}

				_emgData = value;
			}
		}

		public ProtocolEmgDataType ()
		{
		}

		#region IByteSerializable implementation

		public Bytes Serialize ()
		{
			ByteSerializer bs = new ByteSerializer ();

			bs.Serialize (EmgData);

			return bs.GetBuffer ();
		}

		public void DeSerialize (Bytes bytes)
		{
			using (ByteDeserializer bd = new ByteDeserializer (bytes))
			{
				EmgData = bd.DeSerializeBytes (EMG_DATA_SIZE);
			}
		}

		#endregion
	}
}

