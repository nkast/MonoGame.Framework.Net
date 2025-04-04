// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.GamerServices;


namespace Microsoft.Xna.Framework.Net
{
    public class NetworkSessionProperties : List<Nullable<int>>
    {

        // The NetworkSessionProperies can contain up to eight interger values
        //  from all the documentation I can find as well as tests that have been done
        //  to confirm this.
        public NetworkSessionProperties() : base(8)
        {
            this.Add(null);
            this.Add(null);
            this.Add(null);
            this.Add(null);
            this.Add(null);
            this.Add(null);
            this.Add(null);
            this.Add(null);

        }

        public static void WriteProperties(NetworkSessionProperties properties, int[] propertyData)
        {

            for (int x = 0; x < 8; x++)
            {
                if ((properties != null) && properties[x].HasValue)
                {
                    // flag it as having a value
                    propertyData[x * 2] = 1;
                    propertyData[x * 2 + 1] = properties[x].Value;

                }
                else
                {
                    // flag it as not having a value
                    propertyData[x * 2] = 0;
                    propertyData[x * 2 + 1] = 0;

                }
            }
        }

        public static void ReadProperties(NetworkSessionProperties properties, int[] propertyData)
        {
            for (int x = 0; x < 8; x++)
            {
                // set it to null to start
                properties[x] = null;
                // and only if the flag is turned on do we have a value.
                if (propertyData[x * 2] > 0)
                    properties[x] = propertyData[x * 2 + 1];
            }
        }
    }
}