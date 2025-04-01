// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Net
{
    /// <summary>
    /// Provides helper methods to make it easier
    /// to safely raise events.
    /// </summary>
    internal static class DebugHelpers
    {
        [System.Diagnostics.Conditional("DEBUG")]
        internal static void Log(string message)
        {
#if !WP8
            Game.Instance.Log(message);
#endif
        }
    }
}
