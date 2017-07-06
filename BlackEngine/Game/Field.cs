using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using BlackEngine.Physics;
using BlackEngine.Game.Objects;

namespace BlackEngine.Game
{
    public class Field
    {
        /// <summary>
        /// Физический мир
        /// </summary>
        public World world;
        private Timer timer = new Timer();
        /// <summary>
        /// Коллекция игровых объектов сортированных по слоям. Верхние слои отображаются над нижними.
        /// </summary>
        public SortedDictionary<int, List<BlackEngine.Game.Objects.Object>> oblectLayerCollection;

        public delegate void OnPaintHandle();
        public event OnPaintHandle OnPaint;

        public delegate void ObjectBrokedHandle();
        public event ObjectBrokedHandle ObjectBroked;

        public Field()
        {
            world = new World();
            oblectLayerCollection = new SortedDictionary<int, List<Objects.Object>>();
            timer.Tick += timer_Tick;
            timer.Interval = 1;
            Collision.BallBroked += Collision_BallBroked;
        }

        void Collision_BallBroked(BlackEngine.Physics.Objects.Circle circle)
        {
            lock (((System.Collections.ICollection)oblectLayerCollection).SyncRoot)
                foreach (var list in oblectLayerCollection)
                    foreach (var o in list.Value)
                        if (o.physicalObject == circle)
                        {
                            list.Value.Remove(o);
                            world.Delete(circle);
                            ObjectBroked();
                            return;
                        }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // очищаем буфер цвета 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            // очищаем текущую матрицу 
            Gl.glLoadIdentity();
            // устанавливаем текущий цвет - чёрный 
            Gl.glColor3f(0, 0, 0);

            world.update();
            world.CollisionDetect();
            Draw();

            // дожидаемся конца визуализации кадра 
            Gl.glFlush();

            OnPaint();
        }

        public void Add(BlackEngine.Game.Objects.Object o, int layer = 0)
        {
            List<Objects.Object> collection;
            lock (((System.Collections.ICollection)oblectLayerCollection).SyncRoot)
            {
                if (oblectLayerCollection.TryGetValue(layer, out collection))
                {
                    collection.Add(o);
                }
                else
                {
                    collection = new List<Objects.Object>();
                    collection.Add(o);
                    oblectLayerCollection.Add(layer, collection);
                }
            }
            world.Add(o.physicalObject);
        }

        public void Remove(BlackEngine.Game.Objects.Object o)
        {
            lock (((System.Collections.ICollection)oblectLayerCollection).SyncRoot)
                foreach (var list in oblectLayerCollection)
                    foreach (var ph in list.Value)
                        if (ph == o.physicalObject)
                        {
                            world.Delete(o.physicalObject);
                            list.Value.Remove(ph);
                            return;
                        }
        }

        public void Draw()
        {
            lock (((System.Collections.ICollection)oblectLayerCollection).SyncRoot)
                foreach (var list in oblectLayerCollection)
                    foreach (var o in list.Value)
                        o.Draw();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
