using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Shortcutter
{
	public class AppTracker
	{
		NSWorkspace workspace = NSWorkspace.SharedWorkspace;
		String currentlyActiveApp = "";

		public AppTracker ()
		{
			//workspace.ActiveApplication;
			Console.WriteLine ("Add the sleep/wake observers");
			NSWorkspace.Notifications.ObserveDidActivateApplication ((object sender, NSWorkspaceApplicationEventArgs e) => {
				currentlyActiveApp = workspace.ActiveApplication.ValueForKey(new NSString("NSApplicationName")).ToString();
				findShortcut(currentlyActiveApp);
			});
		}

		public string GetActiveApp()
		{
			return currentlyActiveApp;
		}

		private void findShortcut(string applicationName)
		{
			IEnumerable<Shortcut> query = MainClass.Shortcuts.Where(s => (s.ApplicationName.ToLower().Contains(applicationName.ToLower())));
			List<Shortcut> filteredShorcuts = query.ToList();

			if(filteredShorcuts.Count>0){
				Shortcut randomShortcut = filteredShorcuts [randomInRange (0, filteredShorcuts.Count - 1)];
				MainClass.SendNotification ("Try: "+randomShortcut.Description,"Press: "+randomShortcut.ShortcutAction);
			}
		}

		private int randomInRange(int start, int end)
		{
			Random r = new Random();
			return r.Next(start, end); //for ints
		}
	}
}

