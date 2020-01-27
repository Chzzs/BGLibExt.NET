using System;
using System.Collections.Generic;
using System.Linq;

namespace BGLibExt
{
    public static class ByteArrayExtensions
    {
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

        public static string ToHexString(this byte[] value, string delimiter = "")
        {
            var hex = BitConverter.ToString(value);
            return hex.Replace("-", delimiter);
        }

        private static Guid ToBleGuidInternal(this byte[] value)
        {
            var guidBytes = value.ToArray().Reverse().ToArray();
            Array.Reverse(guidBytes, 0, 4);
            Array.Reverse(guidBytes, 4, 2);
            Array.Reverse(guidBytes, 6, 2);
            return new Guid(guidBytes);
        }

        public static IEnumerable<byte[]> Split(this byte[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size).ToArray();
            }
        }
    }
}
