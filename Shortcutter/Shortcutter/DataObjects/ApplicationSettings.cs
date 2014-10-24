using System;

namespace Shortcutter
{
	public class ApplicationSettings
	{
		public Boolean NotificationsEnabled{ get; set;}
		public int WaittimeAfterContextSwitch{ get; set;}
		public int WaittimeBeforeNextNotification{ get; set;}

		public ApplicationSettings ()
		{
		}
	}
}

