using System;
using System.Collections.Generic;
using System.Linq;

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
			appDict.Add("Google Chrome", new Application ("Google Chrome", "A webbrowser."));

			appDict.Add ("Path Finder", new Application ("Path Finder", "A finder replacement"));

			addShortcut("Google Chrome", new Shortcut("New tab.","CMD+T"));
			addShortcut("Google Chrome", new Shortcut("New window.","CMD+N"));
			addShortcut("Google Chrome", new Shortcut("New incognito window.","CMD+Shift-N"));
			addShortcut("Google Chrome", new Shortcut("Close Tab","CMD+W"));

			addShortcut ("Path Finder", new Shortcut("Go to the next higher level in the folder hierarchy.", "CMD+↑"));
			addShortcut ("Path Finder", new Shortcut("Go to the next lower level in the folder hierarchy.", "CMD+↓"));

			Console.Out.WriteLine ("Demo-Content loaded..");
		}

		public void addShortcut(string applicationIdentifier, Shortcut shortcut)
		{
			if (appDict.ContainsKey (applicationIdentifier)) {
				appDict [applicationIdentifier].addShortcut (shortcut);
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

		public List<Application> getApplicationList()
		{
			return appDict.Values.ToList();
		}

		public void addApplication(Application application)
		{
			appDict.Add (application.Identifier, application);
		}

		public void removeApplication(string applicationIdentifier)
		{
			appDict.Remove (applicationIdentifier);
		}
	}
}