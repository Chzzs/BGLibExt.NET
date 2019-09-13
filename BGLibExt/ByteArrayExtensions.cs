using System;
using System.Linq;

namespace BGLibExt
{
    public static class ByteArrayExtensions
    {
        public static string ByteArrayToHexString(this byte[] value)
        {
            var hex = BitConverter.ToString(value);
            return hex.Replace("-", "");
        }

        public static Guid ToGuid(this byte[] value)
        {
            var guidBytes = value.ToArray().Reverse().ToArray();
            Array.Reverse(guidBytes, 0, 4);
            Array.Reverse(guidBytes, 4, 2);
            Array.Reverse(guidBytes, 6, 2);
            return new Guid(guidBytes);
        }
    }
}
