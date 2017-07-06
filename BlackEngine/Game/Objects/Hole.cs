using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine.Game.Objects
{
    public class Hole: Object
    {
        private Graphic graphic = new Graphic();
        public BlackEngine.Physics.Objects.GravityTrap circle;
        public BlackEngine.Physics.Objects.SolidBody physicalObject
        {
            get
            {
                return circle;
            }
        }

        public Hole(double r, Point2d centerOfMass)
        {
            circle = new Physics.Objects.GravityTrap(r, centerOfMass, 1);
        }

        public void Draw()
        {
            graphic.SetColor(0f, 0f, 0f);
            graphic.DrawFillCircle(circle.center.x, circle.center.y, circle.r, 30);
        }
    }
}
