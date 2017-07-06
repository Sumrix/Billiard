using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace BlackEngine.Physics.Objects
{
    public class Room : SolidBody
    {
        public readonly double top, bottom, left, right;

        public Room(double top, double bottom, double left, double right)
            : base(1)
        {
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
        }
    }
}
