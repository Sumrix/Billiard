using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine.Game.Objects
{
    public class Text: Object
    {
        private Graphic graphic;
        public string text;
        public double red, green, blue;
        private Point2d p;
        public bool active = false;

        public BlackEngine.Physics.Objects.SolidBody physicalObject
        {
            get
            {
                return null;
            }
        }

        public Text(double x, double y, string text, float red = 0, float green = 0, float blue = 0)
        {
            p = new Point2d(x, y);
            this.text = text;
            graphic = new Graphic();
            graphic.SetColor(red, green, blue);
        }

        public void Draw()
        {
            if (active)
                graphic.DrawCircle(p.x + 0.1, p.y + 0.14, 0.3, 30);
            graphic.PrintText2D(p.x, p.y, text);
        }
    }
}
