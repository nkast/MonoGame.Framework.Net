// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Net
{
    internal class CommandReceiveData : ICommand
    {
        internal byte[] data;
        internal long remoteUniqueIdentifier = -1;
        internal NetworkGamer gamer;
        
        public CommandReceiveData (long remoteUniqueIdentifier, byte[] data)
        {
            this.remoteUniqueIdentifier = remoteUniqueIdentifier;
            this.data = data;
                
        }
        
        public CommandEventType Command {
            get { return CommandEventType.ReceiveData; }
        }
    }
}

