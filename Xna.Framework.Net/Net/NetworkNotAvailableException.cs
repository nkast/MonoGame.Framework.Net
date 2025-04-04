// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.Net
{
#if WINDOWS_UAP || WINRT || WP8
    [DataContract]
#else
    [Serializable]
#endif
    public class NetworkNotAvailableException : NetworkException
    {
        public NetworkNotAvailableException()
        {
            
        }
        
        public NetworkNotAvailableException( string message ) : base(message)
        {
            
        }
        
        public NetworkNotAvailableException (string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
