using System;
using System.Collections.Generic;
using System.Text;
using Tao.FreeGlut;

using BlackEngine;
using BlackEngine.Game;
using BlackEngine.Game.Objects;

namespace WindowsFormsApplication1
{
    public class Game
    {
        public Field field;
        public Tao.Platform.Windows.SimpleOpenGlControl AnT;
        public double
            top = 8,
            bottom = 2,
            left = 2,
            right = 13,
            r = 0.2,
            width,
            height;
        public Cue cue;
        public Ball ball;
        public BallWay ballWay;
        private Queue<Player> playerList;
        private Player player;

        private Scenario scenario;

        public Game(Tao.Platform.Windows.SimpleOpenGlControl AnT)
        {
            this.AnT = AnT;
            width = 10.0 * (float)AnT.Width / (float)AnT.Height;
            height = 10.0;
            double
                emptySpace = 0.01,
                tableWidth = width * (1 - emptySpace * 2) - Table.border * 2,
                tableHeight = tableWidth / 2;
            left = (width - tableWidth) / 2;
            right = left + tableWidth;
            bottom = Table.border + width * emptySpace;
            top = bottom + tableHeight;


            field = new Field();
            field.OnPaint += field_OnPaint;
            InitTable();
            field.Start();

            InitPlayers();

            scenario = new Scenario(this);
            scenario.Excecute();
        }

        private void InitPlayers()
        {
            playerList = new Queue<Player>();
            
            Text text1 = new Text(right - 1, height * 0.94, "0", 1f, 0f, 0f);
            Player player1 = new Player(Player.Side.right, text1);
            playerList.Enqueue(player1);
            field.Add(text1);

            Text text2 = new Text(left + 1, height * 0.94, "0", 0f, 0f, 1f);
            text2.active = true;
            player = new Player(Player.Side.left, text2);
            field.Add(text2);
        }

        private void field_OnPaint()
        {
            // посылаем сигнал перерисовки элемента AnT. 
            AnT.Invalidate();
        }

        private void InitTable()
        {
            field.Add(new Table(top, bottom, left, right), 0);
            InitBalls();
            InitHoles();
            ballWay = new BallWay();
            field.Add(ballWay, 4);
        }

        private void InitHoles()
        {
            double
                d = 0.08,
                rHole = r + 0.08,
                alignCenter = (left + right) / 2.0;

            field.Add(new Hole(rHole, new Point2d(left + d, top - d)),1);
            field.Add(new Hole(rHole, new Point2d(right - d, top - d)), 1);
            field.Add(new Hole(rHole, new Point2d(left + d, bottom + d)), 1);
            field.Add(new Hole(rHole, new Point2d(right - d, bottom + d)), 1);
            field.Add(new Hole(rHole, new Point2d(alignCenter, top + d)), 1);
            field.Add(new Hole(rHole, new Point2d(alignCenter, bottom - d)), 1);
        }

        private void InitBalls()
        {
            Point2d start = new Point2d((left + right) / 2, (top + bottom) / 2);
            double
                xd = r * 2 - 0.03,
                yd = r * 2 + 0.0001;

            for (int count = 0; count < 6; count++)
            {
                double x = start.x + count * xd;
                for (int n = 0; n < count; n++)
                {
                    double y = start.y + (n - (count - 1) / 2.0) * yd;
                    field.Add(new Ball(0.2, new BlackEngine.Point2d(x, y), 10), 2);
                }
            }

            Point2d aim = new Point2d(start.x - 4, start.y);

            ball = new Ball(
                r: 0.2,
                centerOfMass: aim,
                m: 10,
                forwardSpeed: new Vector2d(0, 0)
            );
            field.Add(ball, 2);

            cue = new Cue(aim, new Point2d(aim.x - 10, aim.y), r, 4);
            field.Add(cue, 4);
        }

        public Player GetCurrentPlayer()
        {
            return player;
        }

        public void NextPlayer()
        {
            player.text.active = false;
            playerList.Enqueue(player);
            player = playerList.Dequeue();
            player.text.active = true;
        }

        public void Close()
        {
            scenario.Stop();
        }
    }
}
