using System;
using System.Linq;
using System.Text;

namespace git.jedinja.monomyo
{
	internal class Bytes
	{
		private readonly byte[] _bytes;

		public Bytes (params byte[] values)
		{
			if (values == null || values.Length < 1)
			{
				throw new ArgumentOutOfRangeException ();
			}
			_bytes = values;
		}

		public int Length {
			get {
				return _bytes.Length;
			}
		}

		public override bool Equals (object obj)
		{
			return obj is Bytes && this._bytes.SequenceEqual ((obj as Bytes)._bytes);
		}

		public override int GetHashCode ()
		{
			return (_bytes.Length > 2 ? _bytes[_bytes.Length - 3] : _bytes[0]) * _bytes.Length;
		}

		public override string ToString ()
		{
			return string.Format ("[Bytes {0}]", ByteArrayToHexString (_bytes));
		}

		public string ToText ()
		{
			return ByteToChar (_bytes);
		}

		public static implicit operator Bytes (byte[] value)
		{
			return new Bytes (value);
		}

		public static explicit operator byte[] (Bytes value)
		{
			return value.ToArray ();
		}

		public static Bytes FromString (string str)
		{
			byte[] b = new byte[str.Length];

			for (int i = 0; i < b.Length; i++)
			{
				b[i] = (byte) str[i];
			}

			return b;
		}

		public byte[] ToArray ()
		{
			return (byte[]) _bytes.Clone ();
		}

		private string ByteArrayToHexString (Byte[] ba)
		{
			StringBuilder hex = new StringBuilder (ba.Length * 2);
			foreach (byte b in ba)
			{
				hex.AppendFormat ("{0:x2} ", b);
			}
			return hex.ToString ();
		}

		private string ByteToChar (Byte[] ba)
		{
			return string.Join (string.Empty, ba.Select (b => ((char) b).ToString ()));
		}
	}
}

