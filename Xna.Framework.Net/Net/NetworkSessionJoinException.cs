// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.Net
{
#if WINDOWS_UAP
    [DataContract]
#else
    [Serializable]
#endif
    public class NetworkSessionJoinException : NetworkException
    {
        #region Methods
        public NetworkSessionJoinException()
        {
        }
        
        public NetworkSessionJoinException( string message ): base(message)
        {
            
        }
        
        public NetworkSessionJoinException (string message, Exception innerException) : base( message, innerException)
        {
            
        }
        
        public NetworkSessionJoinException (string message, NetworkSessionJoinError joinError ):base(message)
        {
            _JoinError = JoinError;
        }
        #endregion
        
        #region Properties
        NetworkSessionJoinError _JoinError;
        public NetworkSessionJoinError JoinError 
        { 
            get
            {
                return _JoinError;
            }
            
            set
            {
                if (_JoinError != value)
                    JoinError = value;
            }
        }
        #endregion
    }
}
