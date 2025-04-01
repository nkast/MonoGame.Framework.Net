// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;


namespace Microsoft.Xna.Framework.Net
{
	public enum NetworkSessionType
	{
		Local,    		// Does not involve any networking traffic, but can be used for split-screen gaming on a single Xbox 360 console. Creating a local network session may also make it easier to share code between local and online game modes. 

		SystemLink,	// Connect multiple Xbox 360 consoles or computers over a local subnet. These machines do not require a connection to Xbox LIVE or any LIVE accounts. However, connection to machines on different subnets is not allowed.
				// If you are a Creators Club developer testing your game, you can use this type to connect an Xbox 360 console to a computer. However, cross-platform networking is not supported in games distributed to non-–Creators Club community players.

		PlayerMatch,	// Uses the Xbox LIVE servers. This enables connection to other machines over the Internet. It requires a LIVE Silver Membership for Windows-based games or a LIVE Gold membership for Xbox 360 games. Games in development will also require an XNA Creators Club premium membership. While in trial mode, Indie games downloaded from Xbox LIVE Markeplace will not have access to LIVE matchmaking.
		Ranked,		// All session matches are ranked. This option is available only for commercial games that have passed Xbox LIVE certification. Due to the competitive nature of the gameplay, this session type does not support join-in-progress.
	}
}