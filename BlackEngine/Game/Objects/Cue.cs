using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace BlackEngine.Game.Objects
{
    public class Cue: Object
    {
        public Point2d aim, m, lastAim;
        private double lastCos, r, length;
        private const double startR = 0.02, endR = 0.07;
        private Point2d[] corners;
        public bool hide = false;

        public BlackEngine.Physics.Objects.SolidBody physicalObject
        {
            get
            {
                return null;
            }
        }

        public Cue(Point2d aim, Point2d m, double r, double length)
        {
            this.aim = aim;
            this.lastAim = new Point2d();
            this.m = m;
            this.r = r;
            this.length = length;
            corners = new Point2d[4];
        }

        public void Draw()
        {
            if (!hide)
            {
                Vector2d
                    v = m - aim;
                double
                    c = v.Abs(),
                    a = v.x,
                    cos = a / c;

                if (!(aim == lastAim && cos == lastCos))
                {
                    lastCos = cos;
                    lastAim = aim;
                    Vector2d
                        v0 = v / v.Abs(),
                        nv1 = new Vector2d(-v0.y, v0.x), // Развернули против часовой стрелки
                        nv2 = new Vector2d(v0.y, -v0.x); // По часовой стрелке
                    Point2d
                        start = this.aim + v0 * (r + 0.1),
                        end = this.aim + v0 * length;

                    corners[0] = start + nv1 * startR;
                    corners[1] = end + nv1 * endR;
                    corners[2] = end + nv2 * endR;
                    corners[3] = start + nv2 * startR;
                }


                Gl.glBegin(Gl.GL_QUADS);

                Gl.glColor3f(0, 0, 0);
                foreach (var p in corners)
                    Gl.glVertex2d(p.x, p.y);

                Gl.glEnd();
            }
        }
    }
}
