// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Net
{
    internal class CommandGamerStateChange : ICommand
    {
        
        GamerStates newState;
        GamerStates oldState;
        NetworkGamer gamer;
        
        public CommandGamerStateChange (NetworkGamer gamer)
        {
            this.gamer = gamer;
            this.newState = gamer.State;
            this.oldState = gamer.OldState;
        }
        
        public NetworkGamer Gamer 
        {
            get { return gamer; }
        }
        public GamerStates NewState
        {
            get { return newState; }
        }
        
        public GamerStates OldState
        {
            get { return oldState; }
        }
        
        public CommandEventType Command {
            get { return CommandEventType.GamerStateChange; }
        }		
    }
}

