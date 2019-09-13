using System;
using System.Linq;

namespace BGLibExt
{
    public static class GuidExtensions
    {
        public static byte[] ToUuidByteArray(this Guid value)
        {
            var bytes = value.ToByteArray().Reverse().ToArray();
            Array.Reverse(bytes, 8, 2);
            Array.Reverse(bytes, 10, 2);
            Array.Reverse(bytes, 12, 4);
            return bytes;
        }
    }
}
