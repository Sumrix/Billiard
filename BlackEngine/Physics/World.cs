using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BlackEngine.Physics.Objects;

namespace BlackEngine.Physics
{
    public class World
    {
        /// <summary>
        /// Движущиеся тела
        /// </summary>
        public List<DynamicObject> dList = new List<DynamicObject>();
        /// <summary>
        /// Неподвижные объекты
        /// </summary>
        public List<SolidBody> sList = new List<SolidBody>();
        /// <summary>
        /// Уничтожаемые объекты
        /// </summary>
        private List<SolidBody> dBasket = new List<SolidBody>();
        private List<SolidBody> sBasket = new List<SolidBody>();

        public const double g = 0;
        public int FPS = 60;
        private double dt;

        public World()
        {
            dt = 1.0 / FPS;
        }

        public void Add(SolidBody body)
        {
            if (body is DynamicObject)
                dList.Add((DynamicObject)body);
            else
                sList.Add(body);
        }

        public void Delete(SolidBody body)
        {
            if (body is DynamicObject)
                dBasket.Add(body);
            else
                sBasket.Add(body);
        }

        public void update()
        {
            ClearBasket();
            lock (((ICollection)dList).SyncRoot)
                foreach (var d in dList)
                    d.UpdateObject(dt);
        }

        private void ClearBasket()
        {
            lock (((ICollection)dList).SyncRoot)
                foreach(var d in dBasket)
                    dList.Remove((DynamicObject)d);
            lock (((ICollection)sList).SyncRoot)
                foreach (var s in sBasket)
                    sList.Remove(s);
        }

        public void CollisionDetect()
        {
            lock (((ICollection)dList).SyncRoot)
            {
                for (int i = 0, iend = dList.Count - 1; i < iend; i++)
                {
                    for (int j = i + 1; j < dList.Count; j++)
                    {
                        Collision.Solve(dList[i], dList[j]);
                    }
                }

                lock (((ICollection)sList).SyncRoot)
                    foreach (var d in dList)
                        foreach (var s in sList)
                            Collision.Solve(d, s);
            }
        }
    }
}
