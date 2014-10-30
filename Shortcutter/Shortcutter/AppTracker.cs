using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Threading;

namespace Shortcutter
{
	public class AppTracker
	{
		private NSWorkspace workspace = NSWorkspace.SharedWorkspace;
		private String currentlyActiveApp = "";

		//Check how long an application was open. We only send notifications when a user
		//has been using his chosen application for a set period.
		Timer applicationTimer;

		public AppTracker ()
		{
		}

		public void Run ()
		{
			applicationTimer = new Timer (timerCallback, null, TimeSpan.FromSeconds (0), TimeSpan.Zero);
			Console.WriteLine ("Add the sleep/wake observers");
			NSWorkspace.Notifications.ObserveDidActivateApplication ((object sender, NSWorkspaceApplicationEventArgs e) => {
				if (MainClass.IsNotificationEnabled ()) {
					currentlyActiveApp = workspace.ActiveApplication.ValueForKey (new NSString ("NSApplicationName")).ToString ();
					//We set a new timer every time a context switch occures.. so only if a user stays x minutes in an app he gets notified
					applicationTimer.Change (TimeSpan.FromSeconds (MainClass.GetWaittimeAfterContextSwitch ()), TimeSpan.Zero);
				}
			});
		}

		private void timerCallback (object state)
		{
			Console.WriteLine ("{0}: Completed timer for: " + currentlyActiveApp, DateTime.Now);
			findShortcut (currentlyActiveApp);
		}

		public string GetActiveApp ()
		{
			return currentlyActiveApp;
		}

		private void findShortcut (string applicationName)
		{
			List<Shortcut> shortcutList = MainClass.GetShortcutList (currentlyActiveApp);

			if (validList (shortcutList)) {
				//Filter shortcuts that are already learned
				List<Shortcut> filteredShorcuts = shortcutList.Where ((shortcut) => (shortcut.learnedShortcut == false)).ToList ();
				if (filteredShorcuts.Count > 0) {
					Shortcut randomShortcut = filteredShorcuts [randomInRange (0, filteredShorcuts.Count - 1)];
					MainClass.SendNotification ("Try: " + randomShortcut.Description, "Press: " + randomShortcut.ShortcutAction);
				}
			}
		}

		private bool validList (List<Shortcut> filteredShorcuts)
		{
			return filteredShorcuts != null && filteredShorcuts.Count > 0;
		}

		private int randomInRange (int start, int end)
		{
			Random r = new Random ();
			return r.Next (start, end); //for ints
		}
	}
}