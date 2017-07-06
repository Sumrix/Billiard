using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class End: GameStage
    {
        public End(Game game)
            : base(game)
        {
        }

        public override object Execute()
        {
            Player p1 = game.GetCurrentPlayer(), p2;
            game.NextPlayer();
            p2 = game.GetCurrentPlayer();

            if (p1.Points > p2.Points)
                MessageBox.Show(string.Format("Победил синий игрок со счётом {0}:{1}", p1.Points, p2.Points), "Игра окончена");
            else
                if (p1.Points < p2.Points)
                    MessageBox.Show(string.Format("Победил красный игрок со счётом {0}:{1}", p1.Points, p2.Points), "Игра окончена");
                else
                    MessageBox.Show(string.Format("Игра окончилась ничьёй со счётом {0}:{1}", p1.Points, p2.Points), "Игра окончена");
            return null;
        }
    }
}
