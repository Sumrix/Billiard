using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine
{
    public struct Vector2d
    {
        public double x;
        public double y;

        public Vector2d(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Вычислить вектор отражения
        /// </summary>
        /// <param name="a">Падающий вектор</param>
        /// <param name="n">Вектор нормали отражения</param>
        /// <returns></returns>
        public static Vector2d Reflection(Vector2d a, Vector2d n)
        {
            return a - 2 * n * Vector2d.Scalar(a, n) / Vector2d.Scalar(n, n);
        }

        public static double Scalar(Vector2d a, Vector2d b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vector2d operator *(double num, Vector2d v)
        {
            return new Vector2d(v.x * num, v.y * num);
        }

        public static Vector2d operator *(Vector2d v, double num)
        {
            return new Vector2d(v.x * num, v.y * num);
        }

        public static Vector2d operator /(Vector2d v, double num)
        {
            return new Vector2d(v.x / num, v.y / num);
        }

        public static Vector2d operator -(Vector2d v, double num)
        {
            return new Vector2d(v.x - num, v.y - num);
        }

        public static Vector2d operator -(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2d operator -(Vector2d v1)
        {
            return new Vector2d(-v1.x, -v1.y);
        }

        public static Vector2d operator +(Vector2d v1, Vector2d v2)
        {
            return new Vector2d(v1.x + v2.x, v1.y + v2.y);
        }

        public double Abs()
        {
            return Math.Sqrt(x * x + y * y);
        }
    }
}
