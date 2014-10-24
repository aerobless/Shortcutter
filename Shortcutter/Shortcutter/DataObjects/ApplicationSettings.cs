using System;
using System.Collections.Generic;

namespace Shortcutter
{
	public class ApplicationSettings
	{
		public Boolean NotificationsEnabled{ get; set;}
		public int WaittimeAfterContextSwitch{ get; set;}
		public int WaittimeBeforeNextNotification{ get; set;}
		public List<Shortcut> Shortcuts { get; set;}

		public ApplicationSettings ()
		{
		}
			
		public void loadDemoContent()
		{
			if(Shortcuts == null){Shortcuts = new List<Shortcut> ();}
			Shortcuts.Add (new Shortcut ("Google Chrome","New tab.","CMD+T"));
			Shortcuts.Add (new Shortcut ("Google Chrome","New window.","CMD+N"));
			Shortcuts.Add (new Shortcut ("Google Chrome","New incognito window.","CMD+Shift-N"));
			Shortcuts.Add (new Shortcut ("Google Chrome","Close Tab","CMD+W"));
			Console.Out.WriteLine ("Demo-Content loaded..");
		}
	}
}

