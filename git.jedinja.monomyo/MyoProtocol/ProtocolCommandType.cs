using System;

namespace git.jedinja.monomyo.MyoProtocol
{
	internal abstract class ProtocolCommandType : IByteSerializable
	{
		public abstract ProtocolCommand Command { get; }

		public abstract byte PayloadSize { get; }

		#region IByteSerializable implementation

		public Bytes Serialize ()
		{
			ByteSerializer bs = new ByteSerializer ();

			bs.Serialize (Command);
			bs.Serialize (PayloadSize);

			this.Serialize (bs);

			return bs.GetBuffer ();
		}

		protected abstract void Serialize (ByteSerializer bs);

		public void DeSerialize (Bytes bytes)
		{
			throw new InvalidOperationException ("DeSerialize is not possible for ~CommandType-s");
		}

		#endregion
	}
}

