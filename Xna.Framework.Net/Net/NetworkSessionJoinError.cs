// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Net
{


    public enum NetworkSessionJoinError
    {
        SessionNotFound, 	// The session could not be found. Occurs if the session has ended after the matchmaking query but before the client joined, of if there is no network connectivity between the client and session host machines.
        SessionNotJoinable,	// The session exists but is not joinable. Occurs if the session is in progress but does not allow gamers to join a session in progress.
        SessionFull,		// The session exists but does not have any open slots for local signed-in gamers.
    }
}
