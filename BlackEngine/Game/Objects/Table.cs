using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace BlackEngine.Game.Objects
{
    public class Table: Object
    {
        public BlackEngine.Physics.Objects.Room room;
        public BlackEngine.Physics.Objects.SolidBody physicalObject
        {
            get
            {
                return room;
            }
        }
        public const double border = 0.5;
        protected Graphic graphic = new Graphic();

        public Table(double top, double bottom, double left, double right)
        {
            room = new Physics.Objects.Room(top, bottom, left, right);
        }

        public void Draw()
        {
            // Деревянные границы
            graphic.SetColor(0.5f, 0.27f, 0.1f);
            graphic.DrawRectangle(
                room.top + border,
                room.bottom - border,
                room.left - border,
                room.right + border
            );
            // Тёмно зелёное поле
            graphic.SetColor(0f, 0.5f, 0.3f);
            graphic.DrawRectangle(
                room.top,
                room.bottom,
                room.left,
                room.right
            );
        }
    }
}
