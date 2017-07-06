using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BlackEngine;

namespace WindowsFormsApplication1
{
    public class ChooseBall: GameStage
    {
        private AutoResetEvent waitHandle = new AutoResetEvent(false);

        public ChooseBall(Game game)
            : base(game)
        {
        }
        
        public override object Execute()
        {
            game.AnT.MouseDown += AnT_MouseDown;
            game.AnT.MouseMove += AnT_MouseMove;
            waitHandle.WaitOne();
            waitHandle.Reset();
            game.AnT.MouseDown -= AnT_MouseDown;
            game.AnT.MouseMove -= AnT_MouseMove;
            game.ball.active = false;
            return null;
        }

        private void AnT_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double
                x = game.width * (e.X) / game.AnT.Width,
                y = game.height * (game.AnT.Height - e.Y) / game.AnT.Height;

            game.cue.aim = ((BlackEngine.Physics.Objects.Circle)game.ball.physicalObject).centerOfMass;
            game.cue.m = new Point2d(x, y);
            waitHandle.Set();
        }

        private void AnT_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double
                x = game.width * (e.X) / game.AnT.Width,
                y = game.height * (game.AnT.Height - e.Y) / game.AnT.Height,
                min = 100;
            BlackEngine.Game.Objects.Ball ball = null;
            Point2d p = new Point2d(x, y);

            lock (((System.Collections.ICollection)game.field.oblectLayerCollection).SyncRoot)
            {
                foreach (var b in game.field.oblectLayerCollection[2])
                {
                    double l = (p - ((BlackEngine.Physics.Objects.Circle)b.physicalObject).centerOfMass).Abs();
                    if (l < min)
                    {
                        min = l;
                        ball = (BlackEngine.Game.Objects.Ball)b;
                    }
                }
            }

            if (ball != null)
            {
                game.ball.active = false;
                ball.active = true;
                this.game.ball = ball;
            }
        }
    }
}
