// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Net
{
    internal class CommandSessionStateChange : ICommand
    {
        
        NetworkSessionState newState;
        NetworkSessionState oldState;
        
        public CommandSessionStateChange (NetworkSessionState newState, NetworkSessionState oldState)
        {
            this.newState = newState;
            this.oldState = oldState;
        }
        
        public NetworkSessionState NewState
        {
            get { return newState; }
        }
        
        public NetworkSessionState OldState
        {
            get { return oldState; }
        }
        
        public CommandEventType Command {
            get { return CommandEventType.SessionStateChange; }
        }		
    }
}

