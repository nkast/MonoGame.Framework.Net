// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#region Using clause
using System;

#endregion Using clause


namespace Microsoft.Xna.Framework.Net
{


	public enum NetworkSessionState
	{
		Lobby,	 // The local machine joins the session, waiting in the pregame lobby. The GameStarted event is raised when gameplay begins.
		Playing, // The local machine joins the session, currently in the middle of gameplay. The GameEnded event is raised when the session returns to the lobby.
		Ended,	 // The local machine has left the current session or the session has ended. The SessionEnded event is raised at this time. The event's arguments describe the reason for the session ending.
	}
}
