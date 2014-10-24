using System;
using System.Collections.Generic;

namespace Shortcutter
{
	public class ApplicationSettings
	{
		public Boolean NotificationsEnabled{ get; set;}
		public int WaittimeAfterContextSwitch{ get; set;}
		public int WaittimeBeforeNextNotification{ get; set;}
		//public List<Shortcut> Shortcuts { get; set;}
		public Dictionary<string,Application> appDict { get; set;}

		public ApplicationSettings ()
		{
		}
			
		public void loadDemoContent()
		{
			NotificationsEnabled = true;
			WaittimeAfterContextSwitch = 30;
			WaittimeBeforeNextNotification = 3600;

			appDict = new Dictionary<string,Application> ();
			appDict.Add("Google Chrome", new Application ("Chrome", "Google Chrome", "A webbrowser."));

			addShortcut("Google Chrome", "New tab.","CMD+T");
			addShortcut("Google Chrome", "New window.","CMD+N");
			addShortcut("Google Chrome", "New incognito window.","CMD+Shift-N");
			addShortcut("Google Chrome", "Close Tab","CMD+W");

			Console.Out.WriteLine ("Demo-Content loaded..");
		}

		public void addShortcut(string applicationIdentifier, string shortcutDescription, string shortcutText)
		{
			if (appDict.ContainsKey (applicationIdentifier)) {
				appDict [applicationIdentifier].addShortcut (shortcutDescription, shortcutText);
			} else {
				Console.Out.WriteLine ("Error: "+applicationIdentifier+" does not exist in AppDict.");
			}
		}

		public void removeShortcut(string applicationIdentifier, Shortcut shortcut)
		{
			if (appDict.ContainsKey (applicationIdentifier)) {
				appDict [applicationIdentifier].removeShortcut (shortcut);
			} else {
				Console.Out.WriteLine ("Error: "+applicationIdentifier+" does not exist in AppDict.");
			}
		}

		public List<Shortcut> getShortcutsFor(string applicationIdentifier)
		{
			if (appDict.ContainsKey (applicationIdentifier)) {
				return appDict [applicationIdentifier].getShortcutList();
			} else {
				Console.Out.WriteLine ("Error: "+applicationIdentifier+" does not exist in AppDict.");
				return null;
			}
		}
	}
}