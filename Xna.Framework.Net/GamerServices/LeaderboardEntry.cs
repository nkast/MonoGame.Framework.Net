// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.GamerServices
{
    [DataContract]
    public sealed class LeaderboardEntry
    {

        [DataMember]
        public long Rating { get; set; }

        [DataMember]
        public PropertyDictionary Columns { get; internal set; }

        [DataMember]
        public Gamer Gamer { get; internal set; }

        public LeaderboardEntry()
        {
        }
    }
}

