// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#region Using clause
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion Using clause

namespace Microsoft.Xna.Framework.Net
{
	public sealed class AvailableNetworkSessionCollection : ReadOnlyCollection<AvailableNetworkSession>, IDisposable
	{
		public AvailableNetworkSessionCollection( IList<AvailableNetworkSession> list ) : base(list)
		{
		}
		
		public void Dispose()
		{
			
		}
	}
}
