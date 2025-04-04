// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.GamerServices
{


    public class GamerCollection<T> : ReadOnlyCollection<T>, IEnumerable<T>, IEnumerable where T : Gamer
    {	
        internal GamerCollection(List<T> list): base(list)
        {
        }
        
        internal GamerCollection(): base(new List<T>())
        {
        }
 
        internal void AddGamer (T item) {
            // need to add gamers at the correct index based on GamerTag           
            if (base.Items.Count > 0)
            {
                for (int i = 0; i < base.Items.Count; i++)
                {
                    if (item.Gamertag.CompareTo(base.Items[i].Gamertag) > 0)
                    {
                        base.Items.Insert(i, item);
                        return;
                    }
                }
            }
            base.Items.Add(item);            
            
        }
        
        internal void RemoveGamer (T item) {
            base.Items.Remove (item);
        }		

        internal void RemoveGamerAt (int item) {
            base.Items.RemoveAt (item);
        }		
        
//	public IEnumerator<Gamer> GetEnumerator()
//        {
//            return this.GetEnumerator();
//        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
