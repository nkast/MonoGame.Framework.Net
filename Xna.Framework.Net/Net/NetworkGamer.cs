// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.ComponentModel;

using Microsoft.Xna.Framework.GamerServices;

namespace Microsoft.Xna.Framework.Net
{
	public class NetworkGamer : Gamer, INotifyPropertyChanged
	{
		
		private byte id;
		NetworkSession session; 
		//bool isHost;
		//bool isLocal;
		//bool hasVoice;
		long remoteUniqueIdentifier = -1;
		GamerStates gamerState;
		GamerStates oldGamerState;
		
		// Declare the event
		public event PropertyChangedEventHandler PropertyChanged;
		
		
		public NetworkGamer ( NetworkSession session, byte id, GamerStates state)
		{
			this.id = id;
			this.session = session;
			this.gamerState = state;
			// We will modify these HasFlags to inline code because MonoTouch does not support
			// the HasFlag method.  Also after reading this : http://msdn.microsoft.com/en-us/library/system.enum.hasflag.aspx#2
			// it just might be better to inline it anyway.
			//this.isHost = (state & GamerStates.Host) != 0; // state.HasFlag(GamerStates.Host);
			//this.isLocal = (state & GamerStates.Local) != 0; // state.HasFlag(GamerStates.Local);
			//this.hasVoice = (state & GamerStates.HasVoice) != 0; //state.HasFlag(GamerStates.HasVoice);
			
			// *** NOTE TODO **
			// This whole state stuff need to be looked at again.  Maybe we should not be using local
			//  variables here and instead just use the flags within the gamerState.
			
			this.gamerState = state;
			this.oldGamerState = state;
		}
		
		internal long RemoteUniqueIdentifier
		{
			get { return remoteUniqueIdentifier; }
			set { remoteUniqueIdentifier = value; }
		}
		
		public bool HasLeftSession 
		{ 
			get
			{
				return false;
			}
		}
		
		public bool HasVoice 
		{ 
			get
			{
				return (gamerState & GamerStates.HasVoice) != 0;
			}
		}
		
		public byte Id 
		{ 
			get
			{
				return id;
			}
		}
		
		public bool IsGuest 
		{ 
			get
			{
				return (gamerState & GamerStates.Guest) != 0;
			}
		}
		
		public bool IsHost 
		{ 
			get
			{
				return (gamerState & GamerStates.Host) != 0;
			}
		}
		
		public bool IsLocal 
		{ 
			get
			{
				return (gamerState & GamerStates.Local) != 0;
			}
		}
		
		public bool IsMutedByLocalUser 
		{ 
			get
			{
				return true;
			}
		}
		
		public bool IsPrivateSlot 
		{ 
			get
			{
				return false;
			}
		}
		
		public bool IsReady 
		{ 
			get
			{
				return (gamerState & GamerStates.Ready) != 0;
			}
			set
			{
				if (((gamerState & GamerStates.Ready) != 0) != value) {
					if (value)
						gamerState |= GamerStates.Ready;
					else
						gamerState &= ~GamerStates.Ready;
					OnPropertyChanged("Ready");
				}
			}
		}
		
		public bool IsTalking 
		{ 
			get
			{
				return false;
			}
		}
		
		private NetworkMachine _Machine;
		public NetworkMachine Machine 
		{ 
			get
			{
				return _Machine;
			}
			set
			{
				if (_Machine != value )
					_Machine = value;
			}
		}
		
		public TimeSpan RoundtripTime 
		{ 
			get
			{
				return TimeSpan.MinValue;
			}
		}
		
		public NetworkSession Session 
		{ 
			get
			{
				return session;
			}
		} 
		
		internal GamerStates State {
			get { return gamerState; }
			set { gamerState = value; }
		}
		
		internal GamerStates OldState {
			get { return oldGamerState; }
		}		
		
		// Create the OnPropertyChanged method to raise the event
		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}
		
	}
}
