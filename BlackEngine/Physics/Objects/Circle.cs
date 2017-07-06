using System;
using System.Collections.Generic;
using System.Text;

using Tao.OpenGl;

namespace BlackEngine.Physics.Objects
{
    public class Circle: DynamicObject
    {
        /// <summary>
        /// Радиус
        /// </summary>
        public double r;

        public Circle(double r, Point2d centerOfMass, double m, double restorationFactor, Vector2d forwardSpeed = new Vector2d())
            : base(restorationFactor, centerOfMass, m, forwardSpeed)
        {
            this.r = r;
        }
    }
}
