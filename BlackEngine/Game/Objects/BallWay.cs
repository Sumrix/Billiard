using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine.Game.Objects
{
    public class BallWay: Object
    {
        public List<Point2d> way;
        public bool hide = false;
        private Graphic graphic;
        public BlackEngine.Physics.Objects.SolidBody physicalObject
        {
            get
            {
                return null;
            }
        }

        public BallWay()
        {
            way = new List<Point2d>();
            graphic = new Graphic();
            graphic.SetColor(0.1f, 0, 0);
        }

        public void Draw()
        {
            if (!hide)
                graphic.DrawDashedLine(way);
        }
    }
}
