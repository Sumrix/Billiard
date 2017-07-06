using System;
using System.Collections.Generic;
using System.Text;
using BlackEngine.Physics.Objects;

namespace BlackEngine.Physics
{
    public static class Collision
    {
        public delegate void CollisionDetectedHandler(Circle circle);
        public static event CollisionDetectedHandler BallBroked;

        public static void Solve(DynamicObject d1, DynamicObject d2)
        {
            if (d1 is Circle)
                if (d2 is Circle)
                    Solve((Circle)d1, (Circle)d2);
        }

        public static void Solve(DynamicObject d, SolidBody s)
        {
            if (d is Circle)
                if (s is Room)
                    Solve((Circle)d, (Room)s);
                else
                    if (s is GravityTrap)
                    Solve((Circle)d, (GravityTrap)s);
        }

        public static void Solve(Circle ball1, Circle ball2)
        {
            Vector2d v = ball1.centerOfMass - ball2.centerOfMass;
            double l = v.Abs();
            if (l < ball1.r + ball2.r)
            {
                Vector2d
                    v0 = v / l,
                    v1 = Vector2d.Scalar(ball1.forwardSpeed, v0) * v0,
                    v2 = Vector2d.Scalar(ball2.forwardSpeed, v0) * v0;

                if (Vector2d.Scalar(v1 - v2, v0) > 0)
                    return;

                Vector2d
                    nv1 = ((ball1.m - ball2.m) * v1 + 2 * v2 * ball2.m) / (ball1.m + ball2.m),
                    nv2 = ball1.m / ball2.m * (v1 - nv1) + v2;

                ball1.forwardSpeed = ball1.forwardSpeed - v1 + nv1;
                ball2.forwardSpeed = ball2.forwardSpeed - v2 + nv2;
            }
        }

        public static void Solve(Circle ball, Room table)
        {
            Vector2d
                N = new Vector2d(); // Нормальный вектор

            // Удар по левой стене
            if (ball.centerOfMass.x < table.left + ball.r)
                N.x += 1;
            // Удар по правой стене
            if (ball.centerOfMass.x > table.right - ball.r)
                N.x -= 1;
            // Удар по верхней стене
            if (ball.centerOfMass.y > table.top - ball.r)
                N.y -= 1;
            // Удар по нижней стене
            if (ball.centerOfMass.y < table.bottom + ball.r)
                N.y += 1;

            if (!(N.x == 0 && N.y == 0))
                ball.forwardSpeed = Vector2d.Reflection(ball.forwardSpeed, N);
        }

        public static void Solve(Circle ball, GravityTrap trap)
        {
            Vector2d v = trap.center - ball.centerOfMass;
            double l = v.Abs();
            if (l < trap.r)
            {
                if (l < 0.1)
                {
                    ball.forwardSpeed = new Vector2d();
                    BallBroked(ball);
                }
                Vector2d
                    v0 = v / l,
                    d = v0 * 0.05;
                ball.forwardSpeed = new Vector2d();
                ball.centerOfMass += d;
            }
        }
    }
}
