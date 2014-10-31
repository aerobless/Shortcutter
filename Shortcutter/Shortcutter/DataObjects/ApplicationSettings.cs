using System;
using System.Collections.Generic;
using System.Linq;

namespace Shortcutter
{
	public class ApplicationSettings
	{
		public Boolean NotificationsEnabled{ get; set; }

		public int WaittimeAfterContextSwitch{ get; set; }

		public int WaittimeBeforeNextNotification{ get; set; }

		public Dictionary<string,Application> appDict { get; set; }

		public ApplicationSettings ()
		{
		}

		public void LoadDemoContent ()
		{
			NotificationsEnabled = true;
			WaittimeAfterContextSwitch = 30;
			WaittimeBeforeNextNotification = 3600;

			appDict = new Dictionary<string,Application> ();
			appDict.Add ("Google Chrome", new Application ("Google Chrome", "A webbrowser."));
			AddShortcut ("Google Chrome", new Shortcut ("New tab.", "⌘+T"));
			AddShortcut ("Google Chrome", new Shortcut ("New window.", "⌘+N"));
			AddShortcut ("Google Chrome", new Shortcut ("New incognito window.", "⌘+Shift-N"));
			AddShortcut ("Google Chrome", new Shortcut ("Open a file from your computer in Google Chrome.", "⌘-O"));
			AddShortcut ("Google Chrome", new Shortcut ("Open a link in a new tab in the background.", "⌘-[Click link]"));
			AddShortcut ("Google Chrome", new Shortcut ("Open a link in a new tab and switch to the newly opened tab.", "⌘-Shift-[Click link]"));
			AddShortcut ("Google Chrome", new Shortcut ("Open the link in a new window.", "Shift-[Click link]"));
			AddShortcut ("Google Chrome", new Shortcut ("Reopen the last tab you've closed.", "⌘-Shift-T"));
			AddShortcut ("Google Chrome", new Shortcut ("Switch to the next tab.", "⌘-Option ->"));
			AddShortcut ("Google Chrome", new Shortcut ("Switches to the previous tab.", "⌘-Option <-"));
			AddShortcut ("Google Chrome", new Shortcut ("Closes the current tab or pop-up.", "⌘-W"));
			AddShortcut ("Google Chrome", new Shortcut ("Closes the current window.", "⌘-Shift-W"));
			AddShortcut ("Google Chrome", new Shortcut ("Goes to the previous page in your browsing history for the tab.", "Backspace"));
			AddShortcut ("Google Chrome", new Shortcut ("Goes to the next page in your browsing history for the tab.", "SHIFT-Backspace"));
			AddShortcut ("Google Chrome", new Shortcut ("Minimizes the window.", "⌘-M"));
			AddShortcut ("Google Chrome", new Shortcut ("Hides Google Chrome.", "⌘-H"));
			AddShortcut ("Google Chrome", new Shortcut ("Hides all other windows.", "⌘-ALT-H"));
			AddShortcut ("Google Chrome", new Shortcut ("Closes Google Chrome.", "⌘-Q"));

			AddShortcut ("Google Chrome", new Shortcut ("Toggles the bookmarks bar on and off.", "⌘-Shift-B"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the bookmark manager.", "⌘-ALT-B"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the Settings page.", "⌘-,"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the History page.", "⌘-Y"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the Downloads page.", "⌘-Shift-J"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the Clear Browsing Data dialog.", "⌘-Shift-Delete"));
			AddShortcut ("Google Chrome", new Shortcut ("Switch between multiple users.", "⌘-Shift-M"));

			AddShortcut ("Google Chrome", new Shortcut ("Highlights the URL.", "⌘-L"));
			AddShortcut ("Google Chrome", new Shortcut ("Deletes the key term that precedes your cursor in the address bar", "⌘-Backspace"));

			AddShortcut ("Google Chrome", new Shortcut ("Prints your current page.", "⌘-P"));
			AddShortcut ("Google Chrome", new Shortcut ("Saves your current page.", "⌘-S"));
			AddShortcut ("Google Chrome", new Shortcut ("Reloads your current page.", "⌘-R"));
			AddShortcut ("Google Chrome", new Shortcut ("Stops loading of your current page.", "ESC"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the find bar.", "⌘-F"));
			AddShortcut ("Google Chrome", new Shortcut ("Finds the next match for your input in the find bar.", "⌘-G"));
			AddShortcut ("Google Chrome", new Shortcut ("Finds the previous match for your input in the find bar.", "⌘-Shift-G"));
			AddShortcut ("Google Chrome", new Shortcut ("Use selection for find", "⌘-F"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the find bar.", "⌘-E"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the find bar.", "⌘-F"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the find bar.", "⌘-F"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the find bar.", "⌘-F"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the find bar.", "⌘-F"));
			AddShortcut ("Google Chrome", new Shortcut ("Opens the find bar.", "⌘-F"));

			appDict.Add ("Path Finder", new Application ("Path Finder", "A finder replacement"));
			AddShortcut ("Path Finder", new Shortcut ("Go to the next higher level in the folder hierarchy.", "CMD+↑"));
			AddShortcut ("Path Finder", new Shortcut ("Go to the next lower level in the folder hierarchy.", "CMD+↓"));

			appDict.Add ("Xamarin Studio", new Application ("Xamarin Studio", "IDE"));
			AddShortcut ("Xamarin Studio", new Shortcut ("Rename method, variable etc.", "CMD+R"));
			AddShortcut ("Xamarin Studio", new Shortcut ("Refactoring menu", "ALT-Enter"));

			Console.Out.WriteLine ("Demo-Content loaded..");
		}

		public void AddShortcut (string applicationIdentifier, Shortcut shortcut)
		{
			if (appDict.ContainsKey (applicationIdentifier)) {
				appDict [applicationIdentifier].AddShortcut (shortcut);
			} else {
				Console.Out.WriteLine ("Error: " + applicationIdentifier + " does not exist in AppDict.");
			}
		}

		public void RemoveShortcut (string applicationIdentifier, Shortcut shortcut)
		{
			if (appDict.ContainsKey (applicationIdentifier)) {
				appDict [applicationIdentifier].RemoveShortcut (shortcut);
			} else {
				Console.Out.WriteLine ("Error: " + applicationIdentifier + " does not exist in AppDict.");
			}
		}

		public List<Shortcut> GetShortcutsFor (string applicationIdentifier)
		{
			if (appDict.ContainsKey (applicationIdentifier)) {
				return appDict [applicationIdentifier].GetShortcutList ();
			} else {
				Console.Out.WriteLine ("Error: " + applicationIdentifier + " does not exist in AppDict.");
				return null;
			}
		}

		public List<Application> GetApplicationList ()
		{
			return appDict.Values.ToList ();
		}

		public void AddApplication (Application application)
		{
			appDict.Add (application.Identifier, application);
		}

		public void RemoveApplication (string applicationIdentifier)
		{
			appDict.Remove (applicationIdentifier);
		}
	}
}