// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Runtime.Remoting.Messaging;

using Microsoft.Xna.Framework.Net;


namespace Microsoft.Xna.Framework.GamerServices
{
	public static class Guide
	{
		private static bool isScreenSaverEnabled;
		private static bool isTrialMode;
		internal static bool isVisible;
		private static bool simulateTrialMode;
		
		internal static void Initialise(Game game) {
			Guide.Window = game.Window;
		}
		delegate string ShowKeyboardInputDelegate (
			PlayerIndex player, 
			string title, 
			string description, 
			string defaultText, 
			bool usePasswordMode);

		public static string ShowKeyboardInput (
			PlayerIndex player, 
			string title, 
			string description, 
			string defaultText,
			bool usePasswordMode)
		{
			string result = defaultText; 

			return result;
		}

		public static IAsyncResult BeginShowKeyboardInput (
			PlayerIndex player,
			string title,
			string description,
			string defaultText,
			AsyncCallback callback,
			Object state)
		{
			return BeginShowKeyboardInput (player, title, description, defaultText, callback, state, false);
		}

		public static IAsyncResult BeginShowKeyboardInput (
			PlayerIndex player,
			string title,
			string description,
			string defaultText,
			AsyncCallback callback,
			Object state,
			bool usePasswordMode)
		{
			isVisible = true;

			ShowKeyboardInputDelegate ski = ShowKeyboardInput; 

			return ski.BeginInvoke (player, title, description, defaultText, usePasswordMode, callback, ski);
		}

		public static string EndShowKeyboardInput (IAsyncResult result)
		{
			try {
				ShowKeyboardInputDelegate ski = (ShowKeyboardInputDelegate)result.AsyncState; 

				return ski.EndInvoke (result);
			} finally {
				isVisible = false;
			}			
		}

		delegate Nullable<int> ShowMessageBoxDelegate (string title,
			string text,
			IEnumerable<string> buttons,
			int focusButton,
			MessageBoxIcon icon);

		public static Nullable<int> ShowMessageBox (string title,
				string text,
				IEnumerable<string> buttons,
				int focusButton,
				MessageBoxIcon icon)
		{
			Nullable<int> result = null;

			isVisible = true;			

			return result;
		}

		public static IAsyncResult BeginShowMessageBox (
				PlayerIndex player,
				string title,
				string text,
				IEnumerable<string> buttons,
				int focusButton,
				MessageBoxIcon icon,
				AsyncCallback callback,
				Object state)
		{	
			isVisible = true;

			ShowMessageBoxDelegate smb = ShowMessageBox; 

			return smb.BeginInvoke (title, text, buttons, focusButton, icon, callback, smb);			
		}

		public static IAsyncResult BeginShowMessageBox (
				string title,
				string text,
				IEnumerable<string> buttons,
				int focusButton,
				MessageBoxIcon icon,
				AsyncCallback callback,
				Object state)
		{
			return BeginShowMessageBox (PlayerIndex.One, title, text, buttons, focusButton, icon, callback, state);
		}

		public static Nullable<int> EndShowMessageBox (IAsyncResult result)
		{
			try {
				ShowMessageBoxDelegate smbd = (ShowMessageBoxDelegate)result.AsyncState; 

				return smbd.EndInvoke (result);
			} finally {
				isVisible = false;
			}
		}

		public static void ShowMarketplace (PlayerIndex player)
		{

		}

		public static void Show ()
		{
			/*GKPeerPickerController ppc = new GKPeerPickerController();
			ppc.ConnectionTypesMask = GKPeerPickerConnectionType.Nearby;
			ppc.Show();*/
			ShowSignIn (1, false);
		}

		public static void ShowSignIn (int paneCount, bool onlineOnly)
		{
//			if (paneCount != 1) {
//				new ArgumentException ("paneCount Can only be 1 on iPhone");
//				return;
//			}
			if (isVisible)
				return;
			isVisible = true;
			
			MonoGameGamerServicesHelper.ShowSigninSheet();
			
			if (GamerServicesComponent.LocalNetworkGamer == null) {
				GamerServicesComponent.LocalNetworkGamer = new LocalNetworkGamer ();
			} else {
				GamerServicesComponent.LocalNetworkGamer.SignedInGamer.BeginAuthentication (null, null);
			}

		}

		public static void ShowLeaderboard ()
		{
			if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers [0].IsSignedInToLive)) {	

			}
		}

		public static void ShowAchievements ()
		{
			if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers [0].IsSignedInToLive)) {

			}
		}

		#region Properties
		public static bool IsScreenSaverEnabled { 
			get {
				return isScreenSaverEnabled;
			}
			set {
				isScreenSaverEnabled = value;
			}
		}

		public static bool IsTrialMode { 
			get {
				return isTrialMode;
			}
			set {
				isTrialMode = value;
			}
		}

		public static bool IsVisible { 
			get {
				return isVisible;
			}
			set {
				isVisible = value;
			}
		}

		public static bool SimulateTrialMode { 
			get {
				return simulateTrialMode;
			}
			set {
				simulateTrialMode = value;
			}
		}

		[CLSCompliant(false)]
		public static GameWindow Window { 
			get;
			set;
		}
		#endregion
	}
}
