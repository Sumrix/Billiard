using System;
using System.Collections.Generic;
using System.Text;

using Tao.OpenGl;

namespace BlackEngine.Physics.Objects
{
    public class GravityTrap: SolidBody
    {
        /// <summary>
        /// Радиус
        /// </summary>
        public double r;
        /// <summary>
        /// Центр круга
        /// </summary>
        public Point2d center;

        public GravityTrap(double r, Point2d center, double restorationFactor)
            : base(restorationFactor)
        {
            this.r = r;
            this.center = center;
        }
    }
}
