using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal class ProtocolImuDataType : IByteSerializable
	{
		public short OrientationW { get; set; }

		public short OrientationX { get; set; }

		public short OrientationY { get; set; }

		public short OrientationZ { get; set; }

		private const int ACCELEROMETER_DATA_LENGTH = 3;
		private short[] _accelerometer = new short[ACCELEROMETER_DATA_LENGTH];

		public short[] Accelerometer {
			get {
				return _accelerometer;
			}	
			set {
				if (value == null || value.Length != ACCELEROMETER_DATA_LENGTH)
				{
					throw new ArgumentOutOfRangeException ("value", "value should contain 3 shorts!");
				}

				_accelerometer = value;
			}
		}

		private const int GYROSCOPE_DATA_LENGTH = 3;
		private short[] _gyroscope = new short[GYROSCOPE_DATA_LENGTH];

		public short[] Gyroscope {
			get {
				return _gyroscope;
			}	
			set {
				if (value == null || value.Length != GYROSCOPE_DATA_LENGTH)
				{
					throw new ArgumentOutOfRangeException ("value", "value should contain 3 shorts!");
				}

				_gyroscope = value;
			}
		}

		public ProtocolImuDataType ()
		{
		}

		#region IByteSerializable implementation

		public Bytes Serialize ()
		{
			ByteSerializer bs = new ByteSerializer ();

			// ORDER IS MANDATORY
			bs.Serialize (OrientationW);
			bs.Serialize (OrientationX);
			bs.Serialize (OrientationY);
			bs.Serialize (OrientationZ);
			bs.Serialize (Accelerometer);
			bs.Serialize (Gyroscope);

			return bs.GetBuffer ();
		}

		public void DeSerialize (Bytes bytes)
		{
			using (ByteDeserializer bd = new ByteDeserializer (bytes))
			{
				// ORDER IS MANDATORY
				OrientationW = bd.DeSerializeShort ();
				OrientationX = bd.DeSerializeShort ();
				OrientationY = bd.DeSerializeShort ();
				OrientationZ = bd.DeSerializeShort ();
				Accelerometer = bd.DeSerializeShorts (ACCELEROMETER_DATA_LENGTH);
				Gyroscope = bd.DeSerializeShorts (GYROSCOPE_DATA_LENGTH);
			}
		}

		#endregion
	}
}

