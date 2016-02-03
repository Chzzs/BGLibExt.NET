using System;
using System.Collections.Generic;
using System.IO;
using git.jedinja.monomyo.BleInfrastructure.BleBackbone;
using git.jedinja.monomyo.MyoProtocol;

namespace git.jedinja.monomyo
{
	internal interface IByteSerializable
	{
		Bytes Serialize ();

		void DeSerialize (Bytes bytes);
	}

	internal class ByteSerializer
	{
		private List<byte> buffer = new List<byte> ();

		public Bytes GetBuffer ()
		{
			return buffer.ToArray ();
		}

		public void Serialize (byte b)
		{
			buffer.Add (b);
		}

		private void Serialize (byte[] bytes)
		{
			buffer.AddRange (bytes);
		}

		public void Serialize (Bytes bytes)
		{
			Serialize ((byte[]) bytes);
		}

		public void Serialize (ushort ush)
		{
			buffer.Add ((byte) (ush));
			buffer.Add ((byte) (ush >> 8));
		}

		public void Serialize (bool b)
		{
			buffer.Add (b ? (byte) 1 : (byte) 0);
		}

		public void Serialize (short sh)
		{
			buffer.Add ((byte) (sh));
			buffer.Add ((byte) (sh >> 8));
		}

		public void Serialize (IEnumerable<short> shorts)
		{
			foreach (short sh in shorts)
			{
				Serialize (sh);
			}
		}

		public void Serialize (ProtocolClassifierPoses pose)
		{
			Serialize ((ushort) pose);
		}

		public void Serialize (ProtocolClasifierModel model)
		{
			Serialize ((byte) model);
		}

		public void Serialize (ProtocolSku sku)
		{
			Serialize ((byte) sku);
		}

		public void Serialize (ProtocolHardwareRevision rev)
		{
			Serialize ((ushort) rev);
		}

		public void Serialize (ProtocolCommand cmd)
		{
			Serialize ((byte) cmd);
		}

		public void Serialize (ProtocolEmgMode emg)
		{
			Serialize ((byte) emg);
		}

		public void Serialize (ProtocolImuMode imu)
		{
			Serialize ((byte) imu);
		}

		public void Serialize (ProtocolClassifierMode classifier)
		{
			Serialize ((byte) classifier);
		}

		public void Serialize (ProtocolVibration vibrate)
		{
			Serialize ((byte) vibrate);
		}

		public void Serialize (ProtocolSleepMode sleep)
		{
			Serialize ((byte) sleep);
		}

		public void Serialize (ProtocolUnlockMode unlock)
		{
			Serialize ((byte) unlock);
		}

		public void Serialize (ProtocolUserAction userAction)
		{
			Serialize ((byte) userAction);
		}

		public void Serialize (ProtocolMotionEvent @event)
		{
			Serialize ((byte) @event);
		}

		public void Serialize (ProtocolClassifierEvent @event)
		{
			Serialize ((byte) @event);
		}

		public void Serialize (ProtocolArm arm)
		{
			Serialize ((byte) arm);
		}

		public void Serialize (ProtocolBraceletDirection direction)
		{
			Serialize ((byte) direction);
		}

		public void Serialize (ProtocolSyncResult sync)
		{
			Serialize ((byte) sync);
		}

		public void Serialize (BleCCCValue value)
		{
			Serialize ((ushort) value);
		}
	}

	internal class ByteDeserializer : IDisposable
	{
		private readonly MemoryStream _buffer;

		public ByteDeserializer (Bytes buf)
		{
			_buffer = new MemoryStream ((byte[]) buf);
		}

		public byte DeSerializeByte ()
		{
			return _buffer.GetByte ();
		}

		public Bytes DeSerializeBytes (int bytes)
		{
			byte[] b = new byte[bytes];

			_buffer.Read (b, 0, bytes);

			return b;
		}

		public ushort DeSerializeUshort ()
		{
			return (ushort) (_buffer.GetByte () + (_buffer.GetByte () << 8));
		}

		public short DeSerializeShort ()
		{
			return (short) (_buffer.GetByte () + (_buffer.GetByte () << 8));
		}

		public short[] DeSerializeShorts (int shorts)
		{
			short[] s = new short[shorts];

			for (int i = 0; i < shorts; i++)
			{
				s[i] = DeSerializeShort ();
			}

			return s;
		}

		public ProtocolClassifierPoses DeSerializePose ()
		{
			return (ProtocolClassifierPoses) DeSerializeUshort ();
		}

		public ProtocolClasifierModel DeSerializeClassifierModel ()
		{
			return (ProtocolClasifierModel) DeSerializeByte ();
		}

		public bool DeSerializeBool ()
		{
			return _buffer.GetByte () == 1;
		}

		public ProtocolSku DeSerializeSku ()
		{
			return (ProtocolSku) DeSerializeByte ();
		}

		public ProtocolHardwareRevision DeSerializeHardwareRevision ()
		{
			return (ProtocolHardwareRevision) DeSerializeUshort ();
		}

		public ProtocolCommand DeSerializeCommand ()
		{
			return (ProtocolCommand) DeSerializeByte ();
		}

		public ProtocolEmgMode DeSerializeEmgMode ()
		{
			return (ProtocolEmgMode) DeSerializeByte ();
		}

		public ProtocolImuMode DeSerializeImuMode ()
		{
			return (ProtocolImuMode) DeSerializeByte ();
		}

		public ProtocolClassifierMode DeSerializeClassifierMode ()
		{
			return (ProtocolClassifierMode) DeSerializeByte ();
		}

		public ProtocolVibration DeSerializeVibration ()
		{
			return (ProtocolVibration) DeSerializeByte ();
		}

		public ProtocolSleepMode DeSerializeSleepMode ()
		{
			return (ProtocolSleepMode) DeSerializeByte ();
		}

		public ProtocolUnlockMode DeSerializeUnlockMode ()
		{
			return (ProtocolUnlockMode) DeSerializeByte ();
		}

		public ProtocolUserAction DeSerializeUserAction ()
		{
			return (ProtocolUserAction) DeSerializeByte ();
		}

		public ProtocolMotionEvent DeSerializeMotionEvent ()
		{
			return (ProtocolMotionEvent) DeSerializeByte ();
		}

		public ProtocolClassifierEvent DeSerializeClassifierEvent ()
		{
			return (ProtocolClassifierEvent) DeSerializeByte ();
		}

		public ProtocolArm DeSerializeArm ()
		{
			return (ProtocolArm) DeSerializeByte ();
		}

		public ProtocolBraceletDirection DeSerializeBraceletDirection ()
		{
			return (ProtocolBraceletDirection) DeSerializeByte ();
		}

		public ProtocolSyncResult DeSerializeSyncResult ()
		{
			return (ProtocolSyncResult) DeSerializeByte ();
		}
			
		public BleCCCValue DeSerializeBleCCValue ()
		{
			return (BleCCCValue) DeSerializeUshort ();
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			if (_buffer != null)
			{
				_buffer.Close ();
				_buffer.Dispose ();
			}
		}

		#endregion
	}

	internal static class ExtMemStr
	{
		public static byte GetByte (this MemoryStream mem)
		{
			int b = mem.ReadByte ();
			if (b >= 0)
			{
				return (byte) b;
			}

			throw new Exception ("Memory stream read " + b.ToString ());
		}
	}
}

