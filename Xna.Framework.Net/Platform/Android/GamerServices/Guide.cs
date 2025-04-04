// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
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
            string result = null;
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            IsVisible = true;

            //TODO: get Activity
            Activity activity = null; // Game.Activity;

            activity.RunOnUiThread(() =>
            {
                var alert = new AlertDialog.Builder(activity);

                alert.SetTitle(title);
                alert.SetMessage(description);

                var input = new EditText(activity) { Text = defaultText };
                if (defaultText != null)
                {
                    input.SetSelection(defaultText.Length);
                }
                if (usePasswordMode)
                {
                    input.InputType = Android.Text.InputTypes.ClassText | Android.Text.InputTypes.TextVariationPassword;
                }
                alert.SetView(input);

                alert.SetPositiveButton("Ok", (dialog, whichButton) =>
                {
                    result = input.Text;
                    IsVisible = false;
                    waitHandle.Set();
                });

                alert.SetNegativeButton("Cancel", (dialog, whichButton) =>
                {
                    result = null;
                    IsVisible = false;
                    waitHandle.Set();
                });
                alert.SetCancelable(false);
                alert.Show();

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
            if (IsVisible)
                throw new GuideAlreadyVisibleException("The function cannot be completed at this time: the Guide UI is already active. Wait until Guide.IsVisible is false before issuing this call.");

            IsVisible = true;

            ShowKeyboardInputDelegate ski = ShowKeyboardInput;

            return ski.BeginInvoke(player, title, description, defaultText, usePasswordMode, callback, ski);
        }

        public static string EndShowKeyboardInput(IAsyncResult result)
        {
            try
            {
                return (result.AsyncState as ShowKeyboardInputDelegate).EndInvoke(result);
            }
            finally
            {
                IsVisible = false;
            }
        }

        delegate Nullable<int> ShowMessageBoxDelegate(
            string title,
            string text,
            IEnumerable<string> buttons,
            int focusButton,
            MessageBoxIcon icon);

        public static Nullable<int> ShowMessageBox(
            string title,
            string text,
            IEnumerable<string> buttons,
            int focusButton,
            MessageBoxIcon icon)
        {
            Nullable<int> result = null;

            IsVisible = true;
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            //TODO: get Activity
            Activity activity = null; // Game.Activity;

            activity.RunOnUiThread(() =>
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(activity);

                alert.SetTitle(title);
                alert.SetMessage(text);

                alert.SetPositiveButton(buttons.ElementAt(0), (dialog, whichButton) =>
                {
                    result = 0;
                    IsVisible = false;
                    waitHandle.Set();
                });

                if (buttons.Count() == 2)
                    alert.SetNegativeButton(buttons.ElementAt(1), (dialog, whichButton) =>
                    {
                        result = 1;
                        IsVisible = false;
                        waitHandle.Set();
                    });
                alert.SetCancelable(false);

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
            int focusButton,
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

        public static Nullable<int> EndShowMessageBox(IAsyncResult result)
        {
            try
            {
                return (result.AsyncState as ShowMessageBoxDelegate).EndInvoke(result);
            }
            finally
            {
                IsVisible = false;
            }
        }

        public static void ShowMarketplace(PlayerIndex player)
        {
            //TODO: get Activity
            Activity activity = null; // Game.Activity;

            string packageName = activity.PackageName;
            try
            {
                Intent intent = new Intent(Intent.ActionView);
                intent.SetData(Android.Net.Uri.Parse("market://details?id=" + packageName));
                intent.SetFlags(ActivityFlags.NewTask);
                activity.StartActivity(intent);
            }
            catch (ActivityNotFoundException)
            {
                Intent intent = new Intent(Intent.ActionView);
                intent.SetData(Android.Net.Uri.Parse("http://play.google.com/store/apps/details?id=" + packageName));
                intent.SetFlags(ActivityFlags.NewTask);
                activity.StartActivity(intent);
            }
        }

        public static void Show()
        {
            ShowSignIn(1, false);
        }

        public static void ShowSignIn(int paneCount, bool onlineOnly)
        {
            if (paneCount != 1)
            {
                new ArgumentException("paneCount Can only be 1 on iPhone");
                return;
            }

            MonoGameGamerServicesHelper.ShowSigninSheet();

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
            if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers[0].IsSignedInToLive))
            {

            }
        }

        public static void ShowAchievements()
        {
            if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers[0].IsSignedInToLive))
            {

            }
        }

        public static void ShowPeerPicker()
        {
            if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers[0].IsSignedInToLive))
            {

            }
        }


        public static void ShowMatchMaker()
        {
            if ((Gamer.SignedInGamers.Count > 0) && (Gamer.SignedInGamers[0].IsSignedInToLive))
            {

            }
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
                return isTrialMode;
            }
            set
            {
                isTrialMode = value;
            }
        }

        public static bool IsVisible
        {
            get
            {
                return isVisible;
            }
            internal set
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

        [CLSCompliant(false)]
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
