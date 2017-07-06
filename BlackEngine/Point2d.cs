using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine
{
    public struct Point2d
    {
        public double x;
        public double y;

        public Point2d(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2d operator -(Point2d p1, Point2d p2)
        {
            return new Vector2d(p1.x - p2.x, p1.y - p2.y);
        }

        public static Point2d operator +(Point2d p, Vector2d v)
        {
            return new Point2d(p.x + v.x, p.y + v.y);
        }

        public static bool operator ==(Point2d p1, Point2d p2)
        {
            return p1.x == p2.x && p1.y == p2.y;
        }

        public static bool operator !=(Point2d p1, Point2d p2)
        {
            return !(p1.x == p2.x && p1.y == p2.y);
        }
    }
}
