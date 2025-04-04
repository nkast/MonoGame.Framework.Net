// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Net
{
    public enum CommandEventType
    {
        GamerJoined,
        GamerLeft,
        SessionStateChange,
        SendData,
        ReceiveData,
        GamerStateChange,
    }
}

