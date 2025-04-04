// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Microsoft.Xna.Framework.Net
{
    public sealed class AvailableNetworkSessionCollection : ReadOnlyCollection<AvailableNetworkSession>, IDisposable
    {
        public AvailableNetworkSessionCollection( IList<AvailableNetworkSession> list ) : base(list)
        {
        }
        
        public void Dispose()
        {
            
        }
    }
}
