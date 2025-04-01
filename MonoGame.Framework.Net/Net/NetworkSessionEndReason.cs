// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#region Using clause
using System;
#endregion Using clause


namespace Microsoft.Xna.Framework.Net
{
	public enum NetworkSessionEndReason
	{
		ClientSignedOut,	// This client player has signed out of session.
		HostEndedSession,	// The host left the session, removing all active players.
		RemovedByHost,		// The host removed this client player from the session.
		Disconnected,		// Network connectivity problems ended the session
	}
}
