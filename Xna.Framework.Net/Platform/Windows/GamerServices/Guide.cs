// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

#if WINDOWS_UAP
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.System;
using Microsoft.Xna.Framework.Input;
#endif

#if !WINDOWS_UAP && !WINDOWS
using Microsoft.Xna.Framework.Net;
#endif


namespace Microsoft.Xna.Framework.GamerServices
{
    public static class Guide
    {
        private static bool isScreenSaverEnabled;
        private static bool isTrialMode = false;
        private static bool isVisible;
        private static bool simulateTrialMode;

#if WINDOWS_UAP
        private static readonly CoreDispatcher _dispatcher;
#endif 

        static Guide()
        {
#if WINDOWS_UAP
            _dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;


            var licenseInformation = CurrentApp.LicenseInformation;
            licenseInformation.LicenseChanged += () => isTrialMode = !licenseInformation.IsActive || licenseInformation.IsTrial;

            isTrialMode = !licenseInformation.IsActive || licenseInformation.IsTrial;
#endif
        }

        delegate string ShowKeyboardInputDelegate(
         PlayerIndex player,
         string title,
         string description,
         string defaultText,
         bool usePasswordMode);

        private static string ShowKeyboardInput(
         PlayerIndex player,
         string title,
         string description,
         string defaultText,
         bool usePasswordMode)
        {
#if W81
            // If SwapChainPanel is null then we are running the non-XAML template
            if (Game.Instance.graphicsDeviceManager.SwapChainPanel == null)
            {
                throw new NotImplementedException("This method works only when using the XAML template.");
            }
            
            Task<string> result = null;
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var inputDialog = new InputDialog();
                result = inputDialog.ShowAsync(title, description, defaultText, usePasswordMode);
            }).AsTask().Wait();

            result.Wait();

            return result.Result;
#else
            throw new NotImplementedException();
#endif
        }

        public static IAsyncResult BeginShowKeyboardInput(
            PlayerIndex player,
            string title,
            string description,
            string defaultText,
            AsyncCallback callback,
            Object state)
        {
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
#if !WINDOWS_UAP
            ShowKeyboardInputDelegate ski = ShowKeyboardInput;

            return ski.BeginInvoke(player, title, description, defaultText, usePasswordMode, callback, ski);
#else
            throw new NotImplementedException();
#endif
        }

        public static string EndShowKeyboardInput(IAsyncResult result)
        {
#if !WINDOWS_UAP
            ShowKeyboardInputDelegate ski = (ShowKeyboardInputDelegate)result.AsyncState;

            return ski.EndInvoke(result);
#else
            throw new NotImplementedException();
#endif
        }

        delegate Nullable<int> ShowMessageBoxDelegate(
            string title,
            string text,
            IEnumerable<string> buttons,
            int focusButton,
            MessageBoxIcon icon);

        private static Nullable<int> ShowMessageBox(
            string title,
            string text,
            IEnumerable<string> buttons,
            int focusButton,
            MessageBoxIcon icon)
        {
            int? result = null;
            IsVisible = true;

#if WINDOWS_UAP

            MessageDialog dialog = new MessageDialog(text, title);
            foreach (string button in buttons)
                dialog.Commands.Add(new UICommand(button, null, dialog.Commands.Count));

            if (focusButton < 0 || focusButton >= dialog.Commands.Count)
                throw new ArgumentOutOfRangeException("focusButton", "Specified argument was out of the range of valid values.");
            dialog.DefaultCommandIndex = (uint)focusButton;

            // The message box must be popped up on the UI thread.
            Task<IUICommand> dialogResult = null;
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                dialogResult = dialog.ShowAsync().AsTask();
            }).AsTask().Wait();

            dialogResult.Wait();
            result = (int)dialogResult.Result.Id;

#endif
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
            Object state)
        {
#if !WINDOWS_UAP
            // TODO: GuideAlreadyVisibleException
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
#else

            var tcs = new TaskCompletionSource<int?>(state);
            var task = Task.Run<int?>(() => ShowMessageBox(title, text, buttons, focusButton, icon));
            task.ContinueWith(t =>
            {
                // Copy the task result into the returned task.
                if (t.IsFaulted)
                    tcs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(t.Result);

                // Invoke the user callback if necessary.
                if (callback != null)
                    callback(tcs.Task);
            });
            return tcs.Task;
#endif
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

        public static Nullable<int> EndShowMessageBox(IAsyncResult result)
        {
#if WINDOWS_UAP
            var x = (Task<int?>)result;
            return  x.Result;
#else
            return ((ShowMessageBoxDelegate)result.AsyncState).EndInvoke(result);
#endif
        }

        public static void ShowMarketplace(PlayerIndex player)
        {
#if W81
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var uri = new Uri(@"ms-windows-store:PDP?PFN=" + Package.Current.Id.FamilyName);
                Launcher.LaunchUriAsync(uri).AsTask<bool>().Wait();
            }).AsTask();
#endif
        }

        public static void Show()
        {
            ShowSignIn(1, false);
        }

        public static void ShowSignIn(int paneCount, bool onlineOnly)
        {
            if (paneCount != 1 && paneCount != 2 && paneCount != 4)
            {
                new ArgumentException("paneCount Can only be 1, 2 or 4 on Windows");
                return;
            }

#if !WINDOWS_UAP && !WINDOWS
            Microsoft.Xna.Framework.GamerServices.MonoGameGamerServicesHelper.ShowSigninSheet();            

            if (GamerServicesComponent.LocalNetworkGamer == null)
            {
                GamerServicesComponent.LocalNetworkGamer = new LocalNetworkGamer();
            }
            else
            {
                GamerServicesComponent.LocalNetworkGamer.SignedInGamer.BeginAuthentication(null, null);
            }
#endif
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
                // If simulate trial mode is enabled then 
                // we're in the trial mode.
#if DEBUG
                return simulateTrialMode || isTrialMode;
#else
                return simulateTrialMode || isTrialMode;
#endif
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
#if !WINDOWS_UAP && !WINDOWS
            MonoGameGamerServicesHelper.Initialise(game);
#endif
        }
    }
}
