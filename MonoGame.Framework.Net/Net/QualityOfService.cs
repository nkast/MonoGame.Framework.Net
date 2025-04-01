// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#region Using clause
using System;

#endregion Using clause

namespace Microsoft.Xna.Framework.Net
{
	public class QualityOfService
	{
		#region Properties
		public TimeSpan AverageRoundtripTime 
		{ 
			get
			{ 
				return TimeSpan.MinValue;
			}
		}
		
		public int BytesPerSecondDownstream 
		{ 
			get
			{ 
				return 0;
			}
		}
		
		public int BytesPerSecondUpstream  
		{ 
			get
			{ 
				return 0;
			}
		}
		
		public bool IsAvailable 
		{ 
			get
			{ 
				return false;
			}
		}
		
		public TimeSpan MinimumRoundtripTime 
		{ 
			get
			{ 
				return TimeSpan.MinValue;
			}
		}
		#endregion Properties
		
	}
}
