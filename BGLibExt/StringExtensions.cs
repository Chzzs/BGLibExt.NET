using System;

namespace BGLibExt
{
    public static class StringExtensions
    {
        public static byte[] HexStringToByteArray(this string value)
        {
            var bytes = new byte[value.Length / 2];
            for (var i = 0; i < value.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }
    }
}
