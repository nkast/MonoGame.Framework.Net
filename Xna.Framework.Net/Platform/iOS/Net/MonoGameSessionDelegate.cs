// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;

using Foundation;
using UIKit;
using GameKit;

using Microsoft.Xna.Framework.GamerServices;

namespace Microsoft.Xna.Framework.Net
{

    [CLSCompliant(false)]
    public class MonoGameSessionDelegate : GKSessionDelegate
    {
        List<LocalNetworkGamer> gamerList;
        public override void PeerChangedState(GKSession session, string peerID, GKPeerConnectionState state)
        {
            LocalNetworkGamer lng = new LocalNetworkGamer();
            
            switch (state)
            {
                case GKPeerConnectionState.Available :
                    break;
                case GKPeerConnectionState.Connected :			    
                    if ( !gamerList.Contains(lng) )
                    {
                        gamerList.Add(lng);
                    }
                    break;
                case GKPeerConnectionState.Connecting :
                    break;
                case GKPeerConnectionState.Disconnected :
                    if ( gamerList.Contains(lng) )
                    {
                        gamerList.Remove(lng);
                    }
                    break;
                case GKPeerConnectionState.Unavailable :
                    break;
            }
        }

        public override void PeerConnectionRequest(GKSession session, string peerID)
        {
#if DEBUG			
            Console.WriteLine( " Session : " + session.SessionID + " PeerID : " + peerID );
#endif
        }

        public override void PeerConnectionFailed(GKSession session, string peerID, NSError error)
        {
#if DEBUG			
            Console.WriteLine( " Session : " + session.SessionID + " PeerID : " + peerID +" PeerConnectionFailed : " + error );
#endif
        }

        public override void FailedWithError(GKSession session, NSError error)
        {
#if DEBUG
            Console.WriteLine( " Session : " + session.SessionID + " FailedWithError : " + error );
#endif
        }
    }
}
