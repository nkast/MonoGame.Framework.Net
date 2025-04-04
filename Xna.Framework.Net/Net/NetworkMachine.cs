// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.Xna.Framework.GamerServices;

namespace Microsoft.Xna.Framework.Net
{
    // Represents a physical machine that is participating in a multiplayer session. 
    // It can be used to detect when more than one NetworkGamer is playing on the same actual machine. 	
    public sealed class NetworkMachine
    {
        private GamerCollection<NetworkGamer> gamers;
        
        #region Constructors
        public NetworkMachine ()
        {
            gamers = new GamerCollection<NetworkGamer>();
        }
        #endregion
        
        #region Methods

        public void RemoveFromSession ()
        {
            throw new NotImplementedException();
        }

        #endregion
        
        #region Methods
        public GamerCollection<NetworkGamer> Gamers 
        { 
            get
            {
                return gamers;
            }
        }
        #endregion
    }
}
