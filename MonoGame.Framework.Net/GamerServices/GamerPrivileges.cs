// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.GamerServices
{
#if WINDOWS_UAP || WINRT || WP8
    [DataContract]
#else
    [Serializable]
#endif
    public class GamerPrivilegeException : Exception
	{
		
	}
	
    public class GamerPrivileges
    {
		#region Properties
		public GamerPrivilegeSetting AllowCommunication 
		{ 
			get
			{
				return GamerPrivilegeSetting.Everyone;
			}
		}
		
		public bool AllowOnlineSessions 
		{ 
			get
			{
				return true;
			}
		}
		
		public GamerPrivilegeSetting AllowProfileViewing 
		{ 
			get
			{
				return GamerPrivilegeSetting.Blocked;
			}
		}
		
		public bool AllowPurchaseContent
		{ 
			get
			{
				return false;
			}
		}
		
		public bool AllowTradeContent 
		{ 
			get
			{
				return false;
			} 
		}
		
		public GamerPrivilegeSetting AllowUserCreatedContent 
		{ 
			get
			{
				return GamerPrivilegeSetting.Blocked;
			} 
		}
		#endregion
		
        
    }
}
