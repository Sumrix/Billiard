using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;

namespace WindowsFormsApplication1
{
    public class Waiting: GameStage
    {
        public bool BallsStillRemained;
        public bool Clog;
        private System.Timers.Timer timer;
        private AutoResetEvent waitHandle = new AutoResetEvent(false);

        public Waiting(Game game)
            : base(game)
        {
            BallsStillRemained = true;
            game.field.ObjectBroked += field_ObjectBroked;
            timer = new System.Timers.Timer(500);
            timer.Enabled = false;
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool rezult = true;
            lock (((System.Collections.ICollection)game.field.world.dList).SyncRoot)
            {
                if (game.field.world.dList.Count == 0)
                {
                    BallsStillRemained = false;
                    timer.Stop();
                    waitHandle.Set();
                }
                foreach (var d in game.field.world.dList)
                {
                    if (d.forwardSpeed.Abs() > 0.02)
                    {
                        rezult = false;
                        break;
                    }
                }
            }
            if (rezult)
            {
                timer.Stop();
                waitHandle.Set();
            }
        }

        public override object Execute()
        {
            Clog = false;
            timer.Enabled = true;
            timer.Start();
            waitHandle.WaitOne();
            waitHandle.Reset();
            timer.Start();
            waitHandle.WaitOne();
            waitHandle.Reset();
            return null;
        }

        private void field_ObjectBroked()
        {
            game.GetCurrentPlayer().Points++;
            Clog = true;
        }
    }
}
