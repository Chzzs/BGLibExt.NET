using System;
using System.IO;

namespace BGLibExt
{
    internal static class MemoryStreamExtensions
    {
        public static byte GetByte(this MemoryStream memoryStream)
        {
            var b = memoryStream.ReadByte();
            if (b >= 0)
            {
                return (byte)b;
            }

            throw new Exception("The end of the memory stream has been reached");
        }
    }
}
