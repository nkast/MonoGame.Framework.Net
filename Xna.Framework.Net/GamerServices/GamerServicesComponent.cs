// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;

namespace Microsoft.Xna.Framework.GamerServices
{
    public class GamerServicesComponent : GameComponent
    {
        private static LocalNetworkGamer lng;

        internal static LocalNetworkGamer LocalNetworkGamer { get { return lng; } set { lng = value; } }

        public GamerServicesComponent(Game game)
            : base(game)
        {
            Guide.Initialise(game);

        }

        public override void Update(GameTime gameTime)
        {

        }
    }

    public class MonoGameGamerServicesComponent : GamerServicesComponent
    {
        public MonoGameGamerServicesComponent(Game game) : base(game)
        {

        }
    }
}
