// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.GamerServices
{
    [DataContract]
    public abstract class Gamer
    {
        static SignedInGamerCollection _signedInGamers = new SignedInGamerCollection();
        string _gamer = "MonoGame";
        Object _tag;
        bool disposed;

        LeaderboardWriter _leaderboardWriter;

        #region Methods

        public IAsyncResult BeginGetProfile( AsyncCallback callback, Object asyncState )
        {
            throw new NotImplementedException();
        }

        public GamerProfile EndGetProfile(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public GamerProfile GetProfile()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return _gamer;
        }

        internal void Dispose()
        {
            disposed = true;
        }

        #endregion
        #region Properties
        [DataMember]
        public string DisplayName 
        {
            get;
            internal set;
        }

        [DataMember]
        public string Gamertag 
        {
            get
            {
                return _gamer;
            }
            
            internal set
            {
                _gamer = value;
            }
        }

        [DataMember]
        public bool IsDisposed
        {
            get
            {
                return disposed;
            }
        }

        public Object Tag 
        {
            get
            {
                return _tag;
            }
            set
            {
                if (_tag != value)
                {
                    _tag = value;
                }
            }
        }

        public static SignedInGamerCollection SignedInGamers
        {
            get
            {
                return _signedInGamers;
            }
        }

        public LeaderboardWriter LeaderboardWriter 
        { 
            get
            {
                return _leaderboardWriter;
            }

            internal set 
            {
                _leaderboardWriter = value;
            }
        }
        #endregion
    }
}
