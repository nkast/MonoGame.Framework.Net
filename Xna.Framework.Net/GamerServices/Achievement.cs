// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.GamerServices
{
    [DataContract]
    public class Achievement
    {
        public Achievement ()
        {
        }

        [DataMember]
        public string Description { get; internal set; }

        [DataMember]
        public bool DisplayBeforeEarned { get; internal set; }

        [DataMember]
        public DateTime EarnedDateTime { get; internal set; }

        [DataMember]
        public bool EarnedOnline { get; internal set; }

        [DataMember]
        public int GamerScore { get; internal set; }

        [DataMember]
        public string HowToEarn { get; internal set; }

        [DataMember]
        public bool IsEarned { get; internal set; }

        [DataMember]
        public string Key { get; internal set; }

        [DataMember]
        public string Name { get; internal set; }

        public Stream GetPicture ()
        {
            throw new NotImplementedException();
        }
    }
}

