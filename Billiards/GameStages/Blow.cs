using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BlackEngine;

namespace WindowsFormsApplication1
{
    public class Blow: GameStage
    {
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private BlackEngine.Physics.Objects.Circle trackBall;

        public Blow(Game game)
            : base(game)
        {
            BlackEngine.Physics.Objects.Circle circle = (BlackEngine.Physics.Objects.Circle)game.ball.physicalObject;
            trackBall = new BlackEngine.Physics.Objects.Circle(
                circle.r,
                circle.centerOfMass,
                circle.m,
                circle.restorationFactor
            );
        }

        public override object Execute()
        {
            game.cue.hide = false;
            game.ballWay.hide = false;
            game.AnT.MouseDown += AnT_MouseDown;
            game.AnT.MouseMove += AnT_MouseMove;
            waitHandle.WaitOne();
            waitHandle.Reset();
            game.AnT.MouseDown -= AnT_MouseDown;
            game.AnT.MouseMove -= AnT_MouseMove;
            game.cue.hide = true;
            game.ballWay.hide = true;
            return null;
        }

        private void AnT_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double
                x = game.width * (e.X) / game.AnT.Width,
                y = game.height * (game.AnT.Height - e.Y) / game.AnT.Height;
            BlackEngine.Physics.Objects.Circle circle = (BlackEngine.Physics.Objects.Circle)game.ball.physicalObject;
            Vector2d
                v = circle.centerOfMass - new Point2d(x, y),
                v0 = v / v.Abs();
            circle.forwardSpeed = v0 * 7;
            
            waitHandle.Set();
        }

        private List<Point2d> CalcWay(BlackEngine.Physics.Objects.Circle circle)
        {
            List<Point2d> way = new List<Point2d>();
            double dt = 0.02;

            trackBall.centerOfMass = circle.centerOfMass;
            trackBall.UpdateObject(dt);

            lock (((System.Collections.ICollection)game.field.world.dList).SyncRoot)
            {
                for (int i = 0; i < 40; i++)
                {
                    foreach (var b in game.field.world.dList)
                    {
                        if (b != circle)
                        {
                            BlackEngine.Physics.Collision.Solve(b, trackBall);
                        }
                    }
            
                    lock (((System.Collections.ICollection)game.field.world.sList).SyncRoot)
                        foreach (var s in game.field.world.sList)
                            BlackEngine.Physics.Collision.Solve(trackBall, s);

                    trackBall.UpdateObject(dt);
                    way.Add(trackBall.centerOfMass);
                }

                foreach (var b in game.field.world.dList)
                    b.forwardSpeed = new Vector2d();
            }

            return way;
        }

        private void AnT_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double
                x = game.width * (e.X) / game.AnT.Width,
                y = game.height * (game.AnT.Height - e.Y) / game.AnT.Height;

            BlackEngine.Physics.Objects.Circle circle = (BlackEngine.Physics.Objects.Circle)game.ball.physicalObject;
            Vector2d
                v = circle.centerOfMass - new Point2d(x, y),
                v0 = v / v.Abs();
            trackBall.forwardSpeed = v0 * 7;

            List<Point2d> way = CalcWay(circle);
            game.ballWay.way = way;

            game.cue.m = new Point2d(x, y);
        }
    }
}
