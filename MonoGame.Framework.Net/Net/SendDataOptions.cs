// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#region Using clause
using System;

#endregion Using clause

namespace Microsoft.Xna.Framework.Net
{
	public enum SendDataOptions
	{
		None,
		Reliable,
		InOrder,
		ReliableInOrder,
		Chat,
	}
}
