using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine.Game.Objects
{
    public class Ball: Object
    {
        private Graphic graphic = new Graphic();
        public BlackEngine.Physics.Objects.Circle circle;
        public BlackEngine.Physics.Objects.SolidBody physicalObject
        {
            get
            {
                return circle;
            }
        }
        public bool active = false;

        public Ball(double r, Point2d centerOfMass, double m, Vector2d forwardSpeed = new Vector2d())
        {
            circle = new Physics.Objects.Circle(r, centerOfMass, m, 1, forwardSpeed);
            graphic.SetColor(0.9f, 0.9f, 0.9f);
        }

        public void Draw()
        {
            graphic.DrawFillCircle(circle.centerOfMass.x, circle.centerOfMass.y, circle.r, 30);
            if (active)
                graphic.DrawCircle(circle.centerOfMass.x, circle.centerOfMass.y, circle.r + 0.1, 30);
        }
    }
}
