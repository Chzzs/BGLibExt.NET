using System;
using System.Linq;

namespace BGLibExt
{
    public static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] value)
        {
            var hex = BitConverter.ToString(value);
            return hex.Replace("-", "");
        }

        public static Guid ToBleGuid(this byte[] value)
        {
            if (value.Length == 16)
            {
                return value.ToBleGuidInternal();
            }
            else
            {
                var baseGuid = new Guid("00000000-0000-1000-8000-00805F9B34FB").ToUuidByteArray().ToList();
                baseGuid.RemoveRange(12, value.Length);
                baseGuid.InsertRange(12, value);
                return baseGuid.ToArray().ToBleGuidInternal();
            }
        }

        private static Guid ToBleGuidInternal(this byte[] value)
        {
            var guidBytes = value.ToArray().Reverse().ToArray();
            Array.Reverse(guidBytes, 0, 4);
            Array.Reverse(guidBytes, 4, 2);
            Array.Reverse(guidBytes, 6, 2);
            return new Guid(guidBytes);
        }
    }
}
