// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.GamerServices
{
    public class AchievementCollection : IList<Achievement>, ICollection<Achievement>, IEnumerable<Achievement>, IEnumerable, IDisposable
    {
        private List<Achievement> innerlist;

        public AchievementCollection()
        {
            innerlist = new List<Achievement>();
        }

        ~AchievementCollection()
        {
            Dispose(false);
        }

        #region Properties
        public int Count
        {
            get { return innerlist.Count; }
        }

        public Achievement this[int index]
        {
            get { return innerlist[index]; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                if (index >= innerlist.Count)
                    throw new IndexOutOfRangeException();

                /*if (innerlist[index].Position == value.Position)
                    innerlist[index] = value;
                else
                {
                    innerlist.RemoveAt(index);
                    innerlist.Add(value);
                }*/
            }
        }

        private bool isReadOnly = false;
        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
        }

        #endregion Properties

        #region Public Methods
        public void Add(Achievement item)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (innerlist.Count == 0)
            {
                innerlist.Add(item);
                return;
            }

            for (int i = 0; i < innerlist.Count; i++)
            {
                /*if (item.Position < innerlist[i].Position)
                {
                    this.innerlist.Insert(i, item);
                    return;
                }*/
            }

            this.innerlist.Add(item);
        }

        public void Clear()
        {
            innerlist.Clear();
        }

        public bool Contains(Achievement item)
        {
            return innerlist.Contains(item);
        }

        public void CopyTo(Achievement[] array, int arrayIndex)
        {
            innerlist.CopyTo(array, arrayIndex);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        public int IndexOf(Achievement item)
        {
            return innerlist.IndexOf(item);
        }

        public void Insert(int index, Achievement item)
        {
            innerlist.Insert(index, item);
        }

        public bool Remove(Achievement item)
        {
            return innerlist.Remove(item);
        }

        public void RemoveAt(int index)
        {
            innerlist.RemoveAt(index);
        }

        public IEnumerator<Achievement> GetEnumerator()
        {
            return innerlist.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerlist.GetEnumerator();
        }

        #endregion Methods
    }
}

