// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;

#if !WP8
namespace Microsoft.Xna.Framework.GamerServices {
#else
namespace MonoGame.Xna.Framework.GamerServices {
#endif

    public class GamerServicesComponent : GameComponent {
        private static LocalNetworkGamer lng;

        internal static LocalNetworkGamer LocalNetworkGamer { get { return lng; } set { lng = value; } }

        public GamerServicesComponent(Game game)
            : base(game)
        {
#if WP8
            var assembly = game.GetType().Assembly;
            if (assembly != null)
            {
                object[] objects = assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
                if (objects.Length > 0)
                {
                    MonoGamerPeer.applicationIdentifier = ((System.Runtime.InteropServices.GuidAttribute)objects[0]).Value;
                }
            }
#endif
            Guide.Initialise(game);
            
        }

        public override void Update (GameTime gameTime)
        {

        }
    }

    public class MonoGameGamerServicesComponent : GamerServicesComponent
    {
        public MonoGameGamerServicesComponent(Game game): base (game)
        {

        }
    }
}
