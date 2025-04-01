// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#region Using Statements
using System;
using System.Globalization;

using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Microsoft.Xna.Framework.GamerServices
{

    public sealed class GamerProfile : IDisposable
    {
        ~GamerProfile()
        {
            Dispose(false);
        }

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
        }

        #endregion

        #region Properties

        public Texture2D GamerPicture
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int GamerScore
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public GamerZone GamerZone
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsDisposed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Motto
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public RegionInfo Region
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public float Reputation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int TitlesPlayed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int TotalAchievements
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
