using System.Collections.Generic;

namespace BGLibExt
{
    internal class ByteSerializer
    {
        private readonly List<byte> _buffer = new List<byte>();

        public byte[] GetBuffer()
        {
            return _buffer.ToArray();
        }

        public void Serialize(byte b)
        {
            _buffer.Add(b);
        }

        public void Serialize(ushort ush)
        {
            _buffer.Add((byte)ush);
            _buffer.Add((byte)(ush >> 8));
        }

        public void Serialize(bool b)
        {
            _buffer.Add(b ? (byte)1 : (byte)0);
        }

        public void Serialize(short sh)
        {
            _buffer.Add((byte)sh);
            _buffer.Add((byte)(sh >> 8));
        }

        public void Serialize(IEnumerable<short> shorts)
        {
            foreach (var sh in shorts)
            {
                Serialize(sh);
            }
        }

        public void Serialize(BleCccValue value)
        {
            Serialize((ushort)value);
        }

        private void Serialize(byte[] bytes)
        {
            _buffer.AddRange(bytes);
        }
    }
}
