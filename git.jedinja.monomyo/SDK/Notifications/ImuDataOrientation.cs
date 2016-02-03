using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jedinja.monomyo.SDK.Notifications
{
    public class ImuDataOrientation
    {
        public short W { get; private set; }
        public short X { get; private set; }
        public short Y { get; private set; }
        public short Z { get; private set; }

        public ImuDataOrientation(short w, short x, short y, short z)
        {
            this.W = w;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
