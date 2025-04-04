// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.GamerServices
{
    public class SignedInGamerCollection : List<SignedInGamer>
    {
        #region Properties
        // Indexer to get and set words of the containing document:
        public SignedInGamer this [PlayerIndex index] { 
            get {
                if (this.Count == 0 || (int)index > this.Count - 1)
                    return null;

                return this [(int)index];
            }
        }
        #endregion
    }


}
