// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

using Android.Net;



namespace Microsoft.Xna.Framework.GamerServices
{
    public class SignedInGamer : Gamer
    {
        private AchievementCollection gamerAchievements;
        private FriendCollection friendCollection;
        private bool isSignedInToLive = true;

        delegate void AuthenticationDelegate();

        public IAsyncResult BeginAuthentication(AsyncCallback callback, Object asyncState)
        {
            // Go off authenticate
            AuthenticationDelegate ad = DoAuthentication;

            return ad.BeginInvoke(callback, ad);
        }

        public void EndAuthentication(IAsyncResult result)
        {
            AuthenticationDelegate ad = (AuthenticationDelegate)result.AsyncState;

            ad.EndInvoke(result);
        }

        private void DoAuthentication()
        {
        }

        public SignedInGamer()
        {
            var result = BeginAuthentication(null, null);
            EndAuthentication(result);
        }

        private void AuthenticationCompletedCallback(IAsyncResult result)
        {
            EndAuthentication(result);
        }

        #region Methods
        public FriendCollection GetFriends()
        {
            if (IsSignedInToLive)
            {
                if (friendCollection == null)
                {
                    friendCollection = new FriendCollection();
                }
            }

            return friendCollection;
        }

        public bool IsFriend(Gamer gamer)
        {
            if (gamer == null)
                throw new ArgumentNullException();

            if (gamer.IsDisposed)
                throw new ObjectDisposedException(gamer.ToString());

            bool found = false;
            foreach (FriendGamer f in friendCollection)
            {
                if (f.Gamertag == gamer.Gamertag)
                {
                    found = true;
                }
            }
            return found;

        }

        delegate AchievementCollection GetAchievementsDelegate();

        public IAsyncResult BeginGetAchievements(AsyncCallback callback, Object asyncState)
        {
            // Go off and grab achievements
            GetAchievementsDelegate gad = GetAchievements;

            return gad.BeginInvoke(callback, gad);
        }

        private void GetAchievementCompletedCallback(IAsyncResult result)
        {
            // get the delegate that was used to call that method
            GetAchievementsDelegate gad = (GetAchievementsDelegate)result.AsyncState;

            // get the return value from that method call
            gamerAchievements = gad.EndInvoke(result);
        }

        public AchievementCollection EndGetAchievements(IAsyncResult result)
        {
            GetAchievementsDelegate gad = (GetAchievementsDelegate)result.AsyncState;

            gamerAchievements = gad.EndInvoke(result);

            return gamerAchievements;
        }

        public AchievementCollection GetAchievements()
        {
            if (IsSignedInToLive)
            {
                if (gamerAchievements == null)
                {
                    gamerAchievements = new AchievementCollection();
                }
            }
            return gamerAchievements;
        }

        delegate void AwardAchievementDelegate(string achievementId, double percentageComplete);

        public IAsyncResult BeginAwardAchievement(string achievementId, AsyncCallback callback, Object state)
        {
            return BeginAwardAchievement(achievementId, 100.0, callback, state);
        }

        public IAsyncResult BeginAwardAchievement(
            string achievementId,
            double percentageComplete,
            AsyncCallback callback,
            Object state)
        {
            // Go off and award the achievement
            AwardAchievementDelegate aad = DoAwardAchievement;

            return aad.BeginInvoke(achievementId, percentageComplete, callback, aad);
        }

        public void EndAwardAchievement(IAsyncResult result)
        {
            AwardAchievementDelegate aad = (AwardAchievementDelegate)result.AsyncState;

            aad.EndInvoke(result);
        }

        private void AwardAchievementCompletedCallback(IAsyncResult result)
        {
            EndAwardAchievement(result);
        }

        public void AwardAchievement(string achievementId)
        {
            AwardAchievement(achievementId, 100.0f);
        }

        public void DoAwardAchievement(string achievementId, double percentageComplete)
        {
        }

        public void AwardAchievement(string achievementId, double percentageComplete)
        {
            if (IsSignedInToLive)
            {
                BeginAwardAchievement(achievementId, percentageComplete, AwardAchievementCompletedCallback, null);
            }
        }

        public void UpdateScore(string aCategory, long aScore)
        {
            if (IsSignedInToLive)
            {
            }
        }

        public void ResetAchievements()
        {
            if (IsSignedInToLive)
            {
            }
        }

        #endregion

        #region Properties
        public GameDefaults GameDefaults
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public bool IsGuest
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public bool IsSignedInToLive
        {
            get
            {
                return isSignedInToLive;
            }
            internal set
            {
                isSignedInToLive = value;
            }
        }

        public int PartySize
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public PlayerIndex PlayerIndex
        {
            get
            {
                return PlayerIndex.One;
            }
        }

        public GamerPresence Presence
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        GamerPrivileges _privileges = new GamerPrivileges();
        public GamerPrivileges Privileges
        {
            get
            {
                return _privileges;
            }
        }
        #endregion


        protected virtual void OnSignedIn(SignedInEventArgs e)
        {
            var handler = SignedIn;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnSignedOut(SignedOutEventArgs e)
        {
            var handler = SignedOut;
            if (handler != null)
                handler(this, e);
        }

        #region Events
        public static event EventHandler<SignedInEventArgs> SignedIn;
        public static event EventHandler<SignedOutEventArgs> SignedOut;
        #endregion
    }

    public class SignedInEventArgs : EventArgs
    {
        public SignedInEventArgs(SignedInGamer gamer)
        {

        }
    }

    public class SignedOutEventArgs : EventArgs
    {
        public SignedOutEventArgs(SignedInGamer gamer)
        {

        }
    }
}
