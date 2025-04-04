// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;

using UIKit;
using GameKit;

using Microsoft.Xna.Framework.GamerServices;

namespace Microsoft.Xna.Framework.Net
{
    [CLSCompliant(false)]
    public class MonoGamePeerPickerControllerDelegate : GameKit.GKPeerPickerControllerDelegate
    {
        private GKSession gkSession;
        private EventHandler<GKDataReceivedEventArgs> receivedData;

        [CLSCompliant(false)]
        public MonoGamePeerPickerControllerDelegate(GKSession aSession, EventHandler<GKDataReceivedEventArgs> aReceivedData)
        {
            gkSession = aSession;
            receivedData = aReceivedData;
        }

        [CLSCompliant(false)]
        public override void ConnectionTypeSelected(GKPeerPickerController picker, GKPeerPickerConnectionType type)
        {
#if DEBUG			
            Console.WriteLine("User Selected a ConnectionType of : " + type);
#endif
            if (type == GKPeerPickerConnectionType.Online)
            {

                picker.Dismiss();
                picker.Delegate = null;

                // Implement your own internet user interface here.

            }
        }

        /*public override GKSession GetSession(GKPeerPickerController picker, GKPeerPickerConnectionType forType)
        {		
            Console.WriteLine("GetSession");
            
            return gkSession;
        }*/
        [CLSCompliant(false)]
        public override void PeerConnected(GKPeerPickerController picker, string peerId, GKSession toSession)
        {
#if DEBUG
            Console.WriteLine("Peer ID " + peerId + " Connected to Session ID : " + toSession.SessionID);
#endif

            // Use a retaining property to take ownership of the session.
            this.gkSession = toSession;

            // Assumes our object will also become the session's delegate.
            gkSession.Delegate = new MonoGameSessionDelegate();
            gkSession.ReceiveData += new EventHandler<GKDataReceivedEventArgs>(receivedData);

            picker.Dismiss();

            // Remove the picker.
            picker.Delegate = null;

            // Start your game.

        }

        [CLSCompliant(false)]
        public override void ControllerCancelled(GKPeerPickerController picker)
        {
#if DEBUG
            Console.WriteLine( "ControllerCancelled");
#endif
            picker.Delegate = null;
        }
    }
}
