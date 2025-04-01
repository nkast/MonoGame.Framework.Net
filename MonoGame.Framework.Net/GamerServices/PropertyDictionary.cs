// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.GamerServices
{
    public class PropertyDictionary
    {
        private Dictionary<string,object> PropDictionary = new Dictionary<string, object>();
        public int GetValueInt32 (string aKey)
        {
            return (int)PropDictionary[aKey];
        }

        public DateTime GetValueDateTime (string aKey)
        {
            return (DateTime)PropDictionary[aKey];
        }

        public void SetValue(string aKey, DateTime aValue)
        {
            if(PropDictionary.ContainsKey(aKey))
            {
                PropDictionary[aKey] = aValue;
            }
            else
            {
                PropDictionary.Add(aKey,aValue);
            }
        }
    }

}

