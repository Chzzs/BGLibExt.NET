using System;
using System.IO;

namespace BGLibExt
{
    internal static class ExtMemStr
    {
        public static byte GetByte(this MemoryStream mem)
        {
            var b = mem.ReadByte();
            if (b >= 0)
            {
                return (byte)b;
            }

            throw new Exception("Memory stream read " + b.ToString());
        }
    }
}
