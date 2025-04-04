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
        private static bool isVisible;
        private static bool simulateTrialMode;		

        delegate string ShowKeyboardInputDelegate(
         PlayerIndex player,           
         string title,
         string description,
         string defaultText,
         bool usePasswordMode);

        public static string ShowKeyboardInput(
         PlayerIndex player,           
         string title,
         string description,
         string defaultText,
         bool usePasswordMode)
        {
            throw new NotImplementedException();
        }

        public static IAsyncResult BeginShowKeyboardInput (
         PlayerIndex player,
         string title,
         string description,
         string defaultText,
         AsyncCallback callback,
         Object state)
        {
            return BeginShowKeyboardInput(player, title, description, defaultText, callback, state, false );
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
            return BeginShowKeyboardInput(player, title, description, defaultText, callback, state, false );
        }

        public static string EndShowKeyboardInput (IAsyncResult result)
        {
            ShowKeyboardInputDelegate ski = (ShowKeyboardInputDelegate)result.AsyncState; 

            return ski.EndInvoke(result);			
        }

        delegate Nullable<int> ShowMessageBoxDelegate( string title,
         string text,
         IEnumerable<string> buttons,
         int focusButton,
         MessageBoxIcon icon);

        public static Nullable<int> ShowMessageBox( string title,
         string text,
         IEnumerable<string> buttons,
         int focusButton,
         MessageBoxIcon icon)
        {
            int? result = null;
            IsVisible = true;

            IsVisible = false;
            return result;
        }

        public static IAsyncResult BeginShowMessageBox(
         PlayerIndex player,
         string title,
         string text,
         IEnumerable<string> buttons,
         int focusButton,
         MessageBoxIcon icon,
         AsyncCallback callback,
         Object state
        )
        {	
            if (IsVisible)
                throw new Exception("The function cannot be completed at this time: the Guide UI is already active. Wait until Guide.IsVisible is false before issuing this call.");

            if (player != PlayerIndex.One)
                throw new ArgumentOutOfRangeException("player", "Specified argument was out of the range of valid values.");
            if (title == null)
                throw new ArgumentNullException("title", "This string cannot be null or empty, and must be less than 256 characters long.");
            if (text == null)
                throw new ArgumentNullException("text", "This string cannot be null or empty, and must be less than 256 characters long.");
            if (buttons == null)
                throw new ArgumentNullException("buttons", "Value can not be null.");

            ShowMessageBoxDelegate smb = ShowMessageBox;

            return smb.BeginInvoke(title, text, buttons, focusButton, icon, callback, smb);		
        }

        public static IAsyncResult BeginShowMessageBox (
         string title,
         string text,
         IEnumerable<string> buttons,
         int focusButton,
         MessageBoxIcon icon,
         AsyncCallback callback,
         Object state
        )
        {
            return BeginShowMessageBox(PlayerIndex.One, title, text, buttons, focusButton, icon, callback, state);
        }

        public static Nullable<int> EndShowMessageBox (IAsyncResult result)
        {
            return ((ShowMessageBoxDelegate)result.AsyncState).EndInvoke(result);
        }


        public static void ShowMarketplace (PlayerIndex player )
        {
            
        }

        public static void Show ()
        {
            ShowSignIn(1, false);
        }

        public static void ShowSignIn (int paneCount, bool onlineOnly)
        {
            if ( paneCount != 1 && paneCount != 2 && paneCount != 4)
            {
                new ArgumentException("paneCount Can only be 1, 2 or 4 on Windows");
                return;
            }

            Microsoft.Xna.Framework.GamerServices.MonoGameGamerServicesHelper.ShowSigninSheet();
            if (GamerServicesComponent.LocalNetworkGamer == null)
            {
                GamerServicesComponent.LocalNetworkGamer = new LocalNetworkGamer();
            }
            else
            {
                GamerServicesComponent.LocalNetworkGamer.SignedInGamer.BeginAuthentication(null, null);
            }
        }

        public static void ShowLeaderboard()
        {
            //if ( ( Gamer.SignedInGamers.Count > 0 ) && ( Gamer.SignedInGamers[0].IsSignedInToLive ) )
            //{
            //    // Lazy load it
            //    if ( leaderboardController == null )
            //    {			    	
            //        leaderboardController = new GKLeaderboardViewController();
            //    }

            //    if (leaderboardController != null)			
            //    {
            //        leaderboardController.DidFinish += delegate(object sender, EventArgs e) 
            //        {
            //            leaderboardController.DismissModalViewControllerAnimated(true);
            //            isVisible = false;
            //        };

            //        if (Window !=null)
            //        {						
            //            if(viewController == null)
            //            {
            //                viewController = new UIViewController();
            //                Window.Add(viewController.View);
            //                viewController.View.Hidden = true;
            //            }

            //            viewController.PresentModalViewController(leaderboardController, true);
            //            isVisible = true;
            //        }
            //    }
            //}
        }

        public static void ShowAchievements()
        {
            //if ( ( Gamer.SignedInGamers.Count > 0 ) && ( Gamer.SignedInGamers[0].IsSignedInToLive ) )
            //{
            //    // Lazy load it
            //    if ( achievementController == null )
            //    {
            //        achievementController = new GKAchievementViewController();
            //    }

            //    if (achievementController != null)		
            //    {					
            //        achievementController.DidFinish += delegate(object sender, EventArgs e) 
            //        {									 
            //            leaderboardController.DismissModalViewControllerAnimated(true);
            //            isVisible = false;
            //        };

            //        if (Window !=null)
            //        {
            //            if(viewController == null)
            //            {
            //                viewController = new UIViewController();
            //                Window.Add(viewController.View);
            //                viewController.View.Hidden = true;
            //            }

            //            viewController.PresentModalViewController(achievementController, true);						
            //            isVisible = true;
            //        }
            //    }
            //}
        }

        #region Properties
        public static bool IsScreenSaverEnabled 
        { 
            get
            {
                return isScreenSaverEnabled;
            }
            set
            {
                isScreenSaverEnabled = value;
            }
        }

        public static bool IsTrialMode 
        { 
            get
            {
                return simulateTrialMode || isTrialMode;
            }
        }

        public static bool IsVisible 
        { 
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
            }
        }

        public static bool SimulateTrialMode 
        { 
            get
            {
                return simulateTrialMode;
            }
            set
            {
                simulateTrialMode = value;
            }
        }

        public static GameWindow Window 
        { 
            get;
            set;
        }
        #endregion

        internal static void Initialise(Game game)
        {
            MonoGameGamerServicesHelper.Initialise(game);
        }
    }
}