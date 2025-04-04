// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.GamerServices
{
    public sealed class LeaderboardWriter : IDisposable
    {
        public LeaderboardWriter()
        {
        }

        public LeaderboardEntry GetLeaderboard(LeaderboardIdentity aLeaderboardIdentity)
        {
            throw new NotImplementedException();
        }

        #region IDisposable implementation

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

