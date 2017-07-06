using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine.Physics.Objects
{
    public abstract class SolidBody
    {
        /// <summary>
        /// Коэфициент восстановления при ударе
        /// </summary>
        public double restorationFactor;

        public SolidBody(double restorationFactor)
        {
            this.restorationFactor = restorationFactor;
        }
    }
}
