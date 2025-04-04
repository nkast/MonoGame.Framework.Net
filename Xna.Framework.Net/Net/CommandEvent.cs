// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Net
{
    internal class CommandEvent
    {
        CommandEventType command;
        object commandObject;
        
        public CommandEvent (CommandEventType command, object commandObject)
        {
            this.command = command;
            this.commandObject = commandObject;
        }
        
        public CommandEvent (ICommand command)
        {
            this.command = command.Command;
            this.commandObject = command;
        }		
        
        public CommandEventType Command
        {
            get { return command; }
            
        }
        
        public object CommandObject
        {
            get { return commandObject; }
        }
    }
}

