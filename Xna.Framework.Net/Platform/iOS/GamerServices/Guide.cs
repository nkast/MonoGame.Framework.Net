﻿// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using GameKit;
using UIKit;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Input.Touch;

namespace Microsoft.Xna.Framework.GamerServices
{
    class GuideAlreadyVisibleException : Exception
    {
        public GuideAlreadyVisibleException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a parent view controller for iOS 5 (and older) Game Center view controllers.
    /// No longer used for iOS 6+ because it's unnecessary and generates runtime warnings.
    /// (See comments for ShowGuideViewController below for more information.)
    /// </summary>
    class GuideViewController : UIViewController
    {
        UIViewController _parent;

        public GuideViewController(UIViewController parent)
        {
            _parent = parent;
        }

        #region Autorotation for iOS 5 or older
        [Obsolete]
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return _parent.ShouldAutorotateToInterfaceOrientation(toInterfaceOrientation);
        }
        #endregion

        #region Autorotation for iOS 6 or newer
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return _parent.GetSupportedInterfaceOrientations();
        }

        public override bool ShouldAutorotate()
        {
            return _parent.ShouldAutorotate();
        }

        public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation()
        {
            return _parent.PreferredInterfaceOrientationForPresentation();
        }
        #endregion

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            /*
            switch(this.InterfaceOrientation)
            {
            case UIInterfaceOrientation.LandscapeLeft:
            case UIInterfaceOrientation.LandscapeRight:
                Game.View.Frame = new System.Drawing.RectangleF(this.View.Frame.Location,new System.Drawing.SizeF(this.View.Frame.Height,this.View.Frame.Width));
                break;
            default:                
                Game.View.Frame = new System.Drawing.RectangleF(this.View.Frame.Location,new System.Drawing.SizeF(this.View.Frame.Width,this.View.Frame.Height));
                break;
            }
            //Game.View.Frame = this.View.Frame;
            Console.WriteLine("Main View's Frame:" + Game.View.Frame);
            */
            base.DidRotate(fromInterfaceOrientation);
        }
    }

    public static class Guide
    {
        private static GKPeerPickerController peerPickerController;
        private static KeyboardInputViewController keyboardViewController;

        private static GuideViewController guideViewController = null; // Only used for iOS 5 and older

        private static GestureType prevGestures;
        private static bool _isInitialised = false;
        private static UIWindow _window;
        private static UIViewController _gameViewController;

        [CLSCompliant(false)]
        public static GKMatch Match { get; private set; }

        static Guide()
        {
            Initialise(Game.Instance);
        }

        internal static void Initialise(Game game)
        {
            if (!_isInitialised)
            {
                _window = (UIWindow)game.Services.GetService(typeof(UIWindow));
                if (_window == null)
                    throw new InvalidOperationException(
                        "iOSGamePlatform must add the main UIWindow to Game.Services");

                _gameViewController = (UIViewController)game.Services.GetService(typeof(UIViewController));
                if (_gameViewController == null)
                    throw new InvalidOperationException(
                        "iOSGamePlatform must add the game UIViewController to Game.Services");

                game.Exiting += Game_Exiting;

                _isInitialised = true;
            }
        }

        private static void Uninitialise(Game game)
        {
            game.Exiting -= Game_Exiting;
            _window = null;
            _gameViewController = null;
            _isInitialised = false;
        }

        #region Properties

        public static bool IsScreenSaverEnabled { get; set; }

        private static bool isTrialMode;

        public static bool IsTrialMode
        {
            get { return isTrialMode || SimulateTrialMode; }
            set { isTrialMode = value; }
        }

        public static bool IsVisible { get; internal set; }

        public static bool SimulateTrialMode { get; set; }

        public static NotificationPosition NotificationPosition { get; set; }

        #endregion

        private static void Game_Exiting(object sender, EventArgs e)
        {
            Uninitialise((Game)sender);
        }

        private static void AssertInitialised()
        {
            if (!_isInitialised)
                throw new InvalidOperationException(
                    "Gamer services functionality has not been initialized.");
        }

        delegate string ShowKeyboardInputDelegate(
            string title,
            string description,
            string defaultText,
            Object state,
            bool usePasswordMode);

        private static string ShowKeyboardInput(
            string title,
            string description,
            string defaultText,
            Object state,
            bool usePasswordMode)
        {
            string result = null;

            IsVisible = true;
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            keyboardViewController = new KeyboardInputViewController(
                title, description, defaultText, usePasswordMode, _gameViewController);

            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                _gameViewController.PresentViewController(keyboardViewController, true, null);

                keyboardViewController.View.InputAccepted += (sender, e) =>
                {
                    _gameViewController.DismissViewController(true, null);
                    result = keyboardViewController.View.Text;
                    waitHandle.Set();
                };

                keyboardViewController.View.InputCanceled += (sender, e) =>
                {
                    _gameViewController.DismissViewController(true, null);
                    waitHandle.Set();
                };
            });
            waitHandle.WaitOne();

            IsVisible = false;
            return result;
        }

        public static IAsyncResult BeginShowKeyboardInput(
            PlayerIndex player,
            string title,
            string description,
            string defaultText,
            AsyncCallback callback,
            Object state)
        {
            AssertInitialised();
            return BeginShowKeyboardInput(player, title, description, defaultText, callback, state, false);
        }

        public static IAsyncResult BeginShowKeyboardInput(
            PlayerIndex player,
            string title,
            string description,
            string defaultText,
            AsyncCallback callback,
            Object state,
            bool usePasswordMode)
        {
            AssertInitialised();

            if (IsVisible)
                throw new GuideAlreadyVisibleException("The function cannot be completed at this time: the Guide UI is already active. Wait until Guide.IsVisible is false before issuing this call.");

            ShowKeyboardInputDelegate ski = ShowKeyboardInput;

            return ski.BeginInvoke(title, description, defaultText, state, usePasswordMode, callback, ski);
        }

        public static string EndShowKeyboardInput(IAsyncResult result)
        {
            keyboardViewController = null;
            return (result.AsyncState as ShowKeyboardInputDelegate).EndInvoke(result);
        }

        delegate Nullable<nint> ShowMessageBoxDelegate(
            string title,
            string text,
            IEnumerable<string> buttons,
            nint focusButton,
            MessageBoxIcon icon);

        private static Nullable<nint> ShowMessageBox(
            string title,
            string text,
            IEnumerable<string> buttons,
            nint focusButton,
            MessageBoxIcon icon)
        {
            Nullable<nint> result = null;

            IsVisible = true;
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                UIAlertView alert = new UIAlertView();
                alert.Title = title;
                foreach (string btn in buttons)
                {
                    alert.AddButton(btn);
                }
                alert.Message = text;
                alert.Dismissed += delegate(object sender, UIButtonEventArgs e)
                { 
                    result = e.ButtonIndex;
                    waitHandle.Set();
                };
                alert.Show();
            });
            waitHandle.WaitOne();
            IsVisible = false;

            return result;
        }

        public static IAsyncResult BeginShowMessageBox(
            PlayerIndex player,
            string title,
            string text,
            IEnumerable<string> buttons,
            nint focusButton,
            MessageBoxIcon icon,
            AsyncCallback callback,
            Object state)
        {
            if (IsVisible)
                throw new GuideAlreadyVisibleException("The function cannot be completed at this time: the Guide UI is already active. Wait until Guide.IsVisible is false before issuing this call.");

            IsVisible = true;

            ShowMessageBoxDelegate smb = ShowMessageBox;

            return smb.BeginInvoke(title, text, buttons, focusButton, icon, callback, smb);         
        }

        public static IAsyncResult BeginShowMessageBox(
            string title,
            string text,
            IEnumerable<string> buttons,
            int focusButton,
            MessageBoxIcon icon,
            AsyncCallback callback,
            Object state)
        {
            return BeginShowMessageBox(PlayerIndex.One, title, text, buttons, focusButton, icon, callback, state);
        }

        public static Nullable<nint> EndShowMessageBox(IAsyncResult result)
        {
            return (result.AsyncState as ShowMessageBoxDelegate).EndInvoke(result);
        }

        public static void ShowMarketplace(PlayerIndex player)
        {
            AssertInitialised();

            string bundleName = NSBundle.MainBundle.InfoDictionary[new NSString("CFBundleName")].ToString();
            StringBuilder output = new StringBuilder();
            foreach (char c in bundleName)
            {
                // Ampersand gets converted to "and"!!
                if (c == '&')
                    output.Append("and");

                // All alphanumeric characters are added
                if (char.IsLetterOrDigit(c))
                    output.Append(c);
            }
            NSUrl url = new NSUrl("itms-apps://itunes.com/app/" + output.ToString());
            if (!UIApplication.SharedApplication.OpenUrl(url))
            {
                // Error
            }
        }

        public static void ShowSignIn(int paneCount, bool onlineOnly)
        {
            AssertInitialised();

            if (paneCount != 1)
            {
                new ArgumentException("paneCount Can only be 1 on iPhone");
                return;
            }

            if (GamerServicesComponent.LocalNetworkGamer == null)
            {
                GamerServicesComponent.LocalNetworkGamer = new LocalNetworkGamer();
            }
            else
            {
                GamerServicesComponent.LocalNetworkGamer.SignedInGamer.BeginAuthentication(null, null);
            }
        }

        /// <summary>
        /// Shows guide view controllers, e.g., Game Center view controllers.
        /// In iOS 5 and older, the guide is presented using GuideViewController.
        /// In iOS 6+, the guide is presented using the root view controller.
        /// (The iOS 5 method generates runtime warnings.)
        /// </summary>
        /// <param name="viewController">The view controller to be shown, e.g., Game Center view controllers</param>
        private static void ShowViewController(UIViewController viewController)
        {
            if (_window != null && viewController != null)
            {
                prevGestures = TouchPanel.EnabledGestures;
                TouchPanel.EnabledGestures = GestureType.None;

                if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
                {
                    // Show view controller the old way for iOS 5 and older
                    if (guideViewController == null)
                    {
                        guideViewController = new GuideViewController(_gameViewController);
                        _window.Add(guideViewController.View);
                        guideViewController.View.Hidden = true;
                    }

#pragma warning disable 618
                    // Disable PresentModalViewController warning, still need to support iOS 5 and older
                    guideViewController.PresentModalViewController(viewController, true);
#pragma warning restore 618
                }
                else
                {
                    // Show view controller the new way for iOS 6+
                    _window.RootViewController.PresentViewController(viewController, true, delegate {});
                }

                IsVisible = true;
            }
        }

        private static void HideViewController(UIViewController viewController)
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {
#pragma warning disable 618
                // Disable DismissModalViewControllerAnimated warning, still need to support iOS 5 and older
                viewController.DismissModalViewController(true);
#pragma warning restore 618
            }
            else
            {
                // Dismiss view controller for iOS 6+
                viewController.DismissViewController(true, delegate {});
            }

            IsVisible = false;
            TouchPanel.EnabledGestures = prevGestures;
        }

        public static void ShowLeaderboard()
        {
            AssertInitialised();

            if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers[0].IsSignedInToLive))
            {
                if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
                {
                    // GKLeaderboardViewController for iOS 5 and older
                    var leaderboardController = new GKLeaderboardViewController();
                    leaderboardController.DidFinish += delegate(object sender, EventArgs e)
                    {
                        HideViewController(leaderboardController);
                    };

                    ShowViewController(leaderboardController);
                }
                else
                {
                    // GKGameCenterViewController for iOS 6+
                    var gameCenterController = new GKGameCenterViewController();
                    gameCenterController.Finished += delegate(object sender, EventArgs e)
                    {
                        HideViewController(gameCenterController);
                    };

                    gameCenterController.ViewState = GKGameCenterViewControllerState.Leaderboards;
                    ShowViewController(gameCenterController);
                }
            }
            else
            {
                UIAlertView alert = new UIAlertView("Error", "You must be signed in to Game Center to view leaderboards.", null, "OK");
                alert.Show();
                ShowSignIn(1, true);
            }
        }

        public static void ShowAchievements()
        {
            AssertInitialised();

            if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers[0].IsSignedInToLive))
            {
                if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
                {
                    // GKAchievementViewController for iOS 5 and older
                    var achievementController = new GKAchievementViewController();
                    achievementController.DidFinish += delegate(object sender, EventArgs e)
                    {
                        HideViewController(achievementController);
                    };

                    ShowViewController(achievementController);
                }
                else
                {
                    // GKGameCenterViewController for iOS 6+
                    var gameCenterController = new GKGameCenterViewController();
                    gameCenterController.Finished += delegate(object sender, EventArgs e)
                    {
                        HideViewController(gameCenterController);
                    };

                    gameCenterController.ViewState = GKGameCenterViewControllerState.Achievements;
                    ShowViewController(gameCenterController);
                }
            }
            else
            {
                UIAlertView alert = new UIAlertView("Error", "You must be signed in to Game Center to view achievements.", null, "OK");
                alert.Show();
                ShowSignIn(1, true);
            }
        }

        [CLSCompliant(false)]
        public static void ShowTwitter(string tweetInitialText = null, string tweetAddUrl = null, UIImage tweetAddImage = null)
        {
            AssertInitialised();

            if (Twitter.TWTweetComposeViewController.CanSendTweet)
            {
                var tweetController = new Twitter.TWTweetComposeViewController();
                tweetController.SetCompletionHandler((Twitter.TWTweetComposeViewControllerResult r) =>
                {
                    HideViewController(tweetController);
                });

                if (!String.IsNullOrEmpty(tweetInitialText))
                    tweetController.SetInitialText(tweetInitialText);

                if (!String.IsNullOrEmpty(tweetAddUrl))
                    tweetController.AddUrl(NSUrl.FromString(tweetAddUrl));

                if (tweetAddImage != null)
                    tweetController.AddImage(tweetAddImage);

                ShowViewController(tweetController);
            }
            else
            {
                UIAlertView alert = new UIAlertView("Error", "There are no Twitter accounts configured on this iOS device.", null, "OK");
                alert.Show();
            }
        }

        [CLSCompliant(false)]
        public static void ShowPeerPicker(GKPeerPickerControllerDelegate aPeerPickerControllerDelegate)
        {
            AssertInitialised();

            if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers[0].IsSignedInToLive))
            {
                // Lazy load it
                if (peerPickerController == null)
                {
                    peerPickerController = new GKPeerPickerController();
                }

                if (peerPickerController != null)
                {           
                    peerPickerController.ConnectionTypesMask = GKPeerPickerConnectionType.Nearby;
                    peerPickerController.Delegate = aPeerPickerControllerDelegate;
                    peerPickerController.Show();                    
                }
            }
        }

        /// <summary>
        /// Displays the iOS matchmaker to the player.
        /// </summary>
        /// <remarks>
        /// Note this is not overloaded in derived classes on purpose.  This is
        /// only a reason this exists is for caching effects.
        /// </remarks>
        /// <param name="minPlayers">Minimum players to find</param>
        /// <param name="maxPlayers">Maximum players to find</param>
        /// <param name="playersToInvite">Players to invite</param>
        public static void ShowMatchMaker(int minPlayers, int maxPlayers, string[] playersToInvite)
        {
            AssertInitialised();

            if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers[0].IsSignedInToLive))
            {
                var matchmakerViewController = new GKMatchmakerViewController(new GKMatchRequest());

                matchmakerViewController.DidFailWithError += delegate(object sender, GKErrorEventArgs e)
                {
                    HideViewController(matchmakerViewController);
                };

                matchmakerViewController.DidFindMatch += delegate(object sender, GKMatchEventArgs e)
                {
                    Guide.Match = e.Match;
                };

                matchmakerViewController.DidFindPlayers += delegate(object sender, GKPlayersEventArgs e)
                {

                };

                matchmakerViewController.WasCancelled += delegate(object sender, EventArgs e)
                {
                    HideViewController(matchmakerViewController);
                };

                matchmakerViewController.MatchRequest.MinPlayers = minPlayers;
                matchmakerViewController.MatchRequest.MaxPlayers = maxPlayers;
                matchmakerViewController.MatchRequest.PlayersToInvite = playersToInvite;

                ShowViewController(matchmakerViewController);
            }
        }

        /// <summary>
        /// Displays the iOS matchmaker to the player.
        /// </summary>
        /// <remarks>
        /// Note this is not overloaded in derived classes on purpose.  This is
        /// only a reason this exists is for caching effects.
        /// </remarks>
        /// <param name="minPlayers">Minimum players to find</param>
        /// <param name="maxPlayers">Maximum players to find</param>
        public static void ShowMatchMaker(int minPlayers, int maxPlayers)
        {
            //ShowMatchMaker(minPlayers, maxPlayers, null); // Setting playersToInvite to null causes the game to crash (tested on iOS 5+)
            ShowMatchMaker(minPlayers, maxPlayers, new string[] { });
        }
    }
}
