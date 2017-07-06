using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine.Physics.Objects
{
    public class DynamicObject: SolidBody
    {
        /// <summary>
        /// Текучесть пространства
        /// </summary>
        public const double fluidity = 0.999;
        /// <summary>
        /// Масса (кг)
        /// </summary>
        public double m;
        /// <summary>
        /// Приложенная к объекту сила
        /// </summary>
        public Vector2d F;
        /// <summary>
        /// Ускорение
        /// </summary>
        public Vector2d a
        {
            get
            {
                return F / m;
            }
        }
        /// <summary>
        /// Импульс тела
        /// </summary>
        public Vector2d forwardImpulse
        {
            get
            {
                return forwardSpeed * m;
            }
            set
            {
                forwardSpeed = value / m;
            }
        }
        /// <summary>
        /// Центр масс (м)
        /// </summary>
        public Point2d centerOfMass;
        /// <summary>
        /// Поступательная скорость (м/с)
        /// </summary>
        public Vector2d forwardSpeed;
        /// <summary>
        /// Скорость вращения (рад/с)
        /// </summary>
        public double rotarySpeed;

        public DynamicObject(
            double restorationFactor,
            Point2d centerOfMass,
            double m,
            Vector2d forwardSpeed = new Vector2d())
            : base(restorationFactor)
        {
            this.restorationFactor = restorationFactor;
            this.centerOfMass = centerOfMass;
            this.m = m;
            this.forwardSpeed = forwardSpeed;
        }

        public virtual void UpdateObject(double dt)
        {
            Move(dt);
            CalcNewForwardSpeed(dt);
        }

        protected virtual void CalcNewForwardSpeed(double dt)
        {
            forwardSpeed = (forwardSpeed - new Vector2d(0, World.g * dt)) * fluidity;
            double dv = 0.01;
            if (forwardSpeed.x != 0)
            {
                if (forwardSpeed.x < 0)
                {
                    if (forwardSpeed.x > -dv)
                        forwardSpeed.x = 0;
                    else
                        forwardSpeed.x += dv;
                }
                else
                {
                    if (forwardSpeed.x < dv)
                        forwardSpeed.x = 0;
                    else
                        forwardSpeed.x -= dv;
                }
            }
            if (forwardSpeed.y != 0)
            {
                if (forwardSpeed.y < 0)
                {
                    if (forwardSpeed.y > -dv)
                        forwardSpeed.y = 0;
                    else
                        forwardSpeed.y += dv;
                }
                else
                {
                    if (forwardSpeed.y < dv)
                        forwardSpeed.y = 0;
                    else
                        forwardSpeed.y-= dv;
                }
            }
        }

        protected virtual void Move(double dt)
        {
            centerOfMass += forwardSpeed * dt + a * dt * dt / 2;
        }
    }
}
