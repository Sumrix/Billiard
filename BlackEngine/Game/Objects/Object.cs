using System;
using System.Collections.Generic;
using System.Text;

namespace BlackEngine.Game.Objects
{
    /// <summary>
    /// Объект игрового мира
    /// </summary>
    public interface Object
    {
        BlackEngine.Physics.Objects.SolidBody physicalObject { get; }

        void Draw();
    }
}
