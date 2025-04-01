// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.GamerServices
{
	public enum GamerPrivilegeSetting
	{
		Blocked,		// This privilege is not available for the current gamer profile.
		Everyone,		// This privilege is available for the current gamer profile.
		FriendsOnly,	// This privilege is only available for friends of the current gamer profile. Use the IsFriend method to check which gamers are friends. 	
	}
}
