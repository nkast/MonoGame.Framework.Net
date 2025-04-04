// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.GamerServices
{
    public static class GamerServicesDispatcher
    {
        public static void Initialize ( IServiceProvider serviceProvider )
        {
            throw new NotImplementedException();   
        }

        public static void Update ()
        {            
        }

        public static bool IsInitialized { get { return false;  } }

        public static IntPtr WindowHandle { get; set; }

        public static event EventHandler<EventArgs> InstallingTitleUpdate;

        private static bool SuppressEventHandlerWarningsUntilEventsAreProperlyImplemented()
        {
            return InstallingTitleUpdate != null;
        }
    }
}
