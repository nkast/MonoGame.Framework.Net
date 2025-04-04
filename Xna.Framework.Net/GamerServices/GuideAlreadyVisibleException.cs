// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.GamerServices
{
    [DataContract]
    public class GuideAlreadyVisibleException : Exception
    {

        public GuideAlreadyVisibleException ()
        {
        }
        
        public GuideAlreadyVisibleException( string message )
        {
            
        }

        public GuideAlreadyVisibleException(string message, Exception innerException)
        {
            
        }
    }
}
