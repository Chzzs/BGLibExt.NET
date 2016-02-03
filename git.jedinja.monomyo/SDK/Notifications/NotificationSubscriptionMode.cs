using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git.jedinja.monomyo.SDK.Notifications
{
	public enum NotificationSubscriptionMode
	{
		/// <summary>
		/// Let's you configure what is received from the armband on initialization. 
		/// Then on you only need to subscribe to events. 
		/// The SDK would automatically manage GATT configuration characteristics.  
		/// </summary>
		EasyConfigurable = 1,

		/// <summary>
		/// Nothing is automated, you need to run everything manually. 
		/// Typical sequence of actions is to subscribe to an event, issue command to Myo telling it what you need and finally 
		/// configure the GATT characteristic to send data.
		/// </summary>
		FullControl = 2
	}
}
