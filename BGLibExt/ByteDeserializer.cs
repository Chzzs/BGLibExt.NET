using System;
using System.IO;

namespace BGLibExt
{
    internal class ByteDeserializer : IDisposable
    {
        private readonly MemoryStream _buffer;

        public ByteDeserializer(byte[] buf)
        {
            _buffer = new MemoryStream((byte[])buf);
        }

        public BleCCCValue DeSerializeBleCCCValue()
        {
            return (BleCCCValue)DeSerializeUshort();
        }

        public bool DeSerializeBool()
        {
            return _buffer.GetByte() == 1;
        }

        public byte DeSerializeByte()
        {
            return _buffer.GetByte();
        }

        public byte[] DeSerializeBytes(int bytes)
        {
            var b = new byte[bytes];

            _buffer.Read(b, 0, bytes);

            return b;
        }

        public short DeSerializeShort()
        {
            return (short)(_buffer.GetByte() + (_buffer.GetByte() << 8));
        }

        public short[] DeSerializeShorts(int shorts)
        {
            var s = new short[shorts];

            for (var i = 0; i < shorts; i++)
            {
                s[i] = DeSerializeShort();
            }

            return s;
        }

        public ushort DeSerializeUshort()
        {
            return (ushort)(_buffer.GetByte() + (_buffer.GetByte() << 8));
        }

        public void Dispose()
        {
            if (_buffer != null)
            {
                _buffer.Close();
                _buffer.Dispose();
            }
        }
    }
}
