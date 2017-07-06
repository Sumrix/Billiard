using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Player
    {
        public enum Side
        {
            left,
            right
        }
        public Side side;
        public BlackEngine.Game.Objects.Text text;
        private int points;
        public int Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
                text.text = value.ToString();
            }
        }
        public Player(Side side, BlackEngine.Game.Objects.Text text)
        {
            this.side = side;
            this.text = text;
        }
    }
}
