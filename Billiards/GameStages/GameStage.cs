using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication1
{
    public abstract class GameStage
    {
        protected Game game;

        public GameStage(Game game)
        {
            this.game = game;
        }

        public abstract object Execute();

    }
}
