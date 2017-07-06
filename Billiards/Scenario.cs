using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WindowsFormsApplication1
{
    public class Scenario
    {
        private Blow blow;
        private Waiting waiting;
        private ChooseBall chooseBall;
        private End end;
        private Game game;
        private Thread th;

        public Scenario(Game game)
        {
            this.game = game;
            blow = new Blow(game);
            waiting = new Waiting(game);
            chooseBall = new ChooseBall(game);
            end = new End(game);
            th = new Thread(new ThreadStart(Process));
        }

        public void Excecute()
        {
            th.Start();
        }

        private void Process()
        {
            while (true)
            {
                blow.Execute();

                waiting.Execute();
                if (!waiting.BallsStillRemained)
                    break;
                if (!waiting.Clog)
                    game.NextPlayer();

                chooseBall.Execute();
            }
            end.Execute();
            Stop();
        }

        public void Stop()
        {
            th.Abort();
            th.Join();
        }
    }
}
