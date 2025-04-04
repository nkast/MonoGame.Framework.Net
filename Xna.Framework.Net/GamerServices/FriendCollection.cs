// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.GamerServices
{
    public class FriendCollection : IList<FriendGamer>, ICollection<FriendGamer>, IEnumerable<FriendGamer>, IEnumerable, IDisposable
    {
        private List<FriendGamer> innerlist;
        
        public FriendCollection ()
        {
            innerlist = new List<FriendGamer>();
        }
        
        ~FriendCollection()
        {
            Dispose(false);
        }

        #region Properties
        public int Count
        {
            get { return innerlist.Count; }
        }
        
        public FriendGamer this[int index]
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

        private bool isReadOnly;
        public bool IsReadOnly 
        {
            get
            {
                return isReadOnly;
            }
            private set 
            {
                isReadOnly = value;
            }
        }
        
        #endregion Properties
        
        #region Public Methods
        public void Add(FriendGamer item)
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
        
        public bool Contains(FriendGamer item)
        {
            return innerlist.Contains(item);
        }
        
        public void CopyTo(FriendGamer[] array, int arrayIndex)
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

        public int IndexOf(FriendGamer item)
        {
            return innerlist.IndexOf(item);
        }
        
        public void Insert(int index, FriendGamer item)
        {
            innerlist.Insert(index, item);
        }
        
        public bool Remove(FriendGamer item)
        {
            return innerlist.Remove(item);
        }
        
        public void RemoveAt(int index)
        {
            innerlist.RemoveAt(index);
        }
        
        public IEnumerator<FriendGamer> GetEnumerator()
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

