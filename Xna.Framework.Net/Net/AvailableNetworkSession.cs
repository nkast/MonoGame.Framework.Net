// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Net;


namespace Microsoft.Xna.Framework.Net
{
    public sealed class AvailableNetworkSession
    {

        public AvailableNetworkSession ()
        {
            _QualityOfService = new QualityOfService();
        }
        
        int _currentGameCount;
        public int CurrentGamerCount 
        { 
            get
            {
                return _currentGameCount;
            }
            
            internal set { _currentGameCount = value; }
        }
        
        string _hostGamertag;
        public string HostGamertag 
        { 
            get
            {
                return _hostGamertag;
            }
            
            internal set { _hostGamertag = value; }
        }
        
        int _openPrivateGamerSlots;
        public int OpenPrivateGamerSlots 
        { 
            get
            {
                return _openPrivateGamerSlots;
            }
            
            internal set { _openPrivateGamerSlots = value; }			
        }
        
        int _openPublicGamerSlots; 
        public int OpenPublicGamerSlots 
        { 
            get
            {
                return _openPublicGamerSlots;
            }
            internal set { _openPublicGamerSlots = value; }			
        }
        
        private QualityOfService _QualityOfService;
        public QualityOfService QualityOfService 
        { 
            get
            {
                return _QualityOfService;
            }
            internal set { _QualityOfService = value; }			
        }
        
        NetworkSessionProperties _sessionProperties;
        public NetworkSessionProperties SessionProperties 
        { 
            get
            {
                return _sessionProperties;
            }
            internal set { _sessionProperties = value; }			
        }
        
        IPEndPoint _endPoint;
        internal IPEndPoint EndPoint 
        {
            get { return _endPoint; }
            set { _endPoint = value;}
        }
        IPEndPoint _internalendPoint;

        internal IPEndPoint InternalEndpont
        {
            get { return _internalendPoint; }
            set { _internalendPoint = value; }
        }

        internal NetworkSessionType SessionType { get; set; }
    }
}
