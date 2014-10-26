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
		NSWorkspace workspace = NSWorkspace.SharedWorkspace;
		String currentlyActiveApp = "";
		String selectedApplication = "Google Chrome";

		//Check how long an application was open. We only send notifications when a user
		//has been using his chosen application for a set period.
		Timer applicationTimer;

		public AppTracker ()
		{
			applicationTimer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(MainClass.getWaittimeAfterContextSwitch()), TimeSpan.Zero);
			Console.WriteLine ("Add the sleep/wake observers");
			NSWorkspace.Notifications.ObserveDidActivateApplication ((object sender, NSWorkspaceApplicationEventArgs e) => {
				currentlyActiveApp = workspace.ActiveApplication.ValueForKey(new NSString("NSApplicationName")).ToString();
				//We set a new timer every time a context switch occures.. so only if a user stays x minutes in an app he gets notified
				applicationTimer.Change(TimeSpan.FromSeconds(MainClass.getWaittimeAfterContextSwitch()), TimeSpan.Zero);
			});
		}

		private void TimerCallback(object state) {
			Console.WriteLine("{0}: Completed timer for: "+currentlyActiveApp, DateTime.Now);
			findShortcut(currentlyActiveApp);
		}


		public string GetActiveApp()
		{
			return currentlyActiveApp;
		}

		private void findShortcut(string applicationName)
		{
			IEnumerable<Shortcut> query = MainClass.getShortcutList(selectedApplication).Where(s => (s.getApplicationIdentifier().ToLower().Contains(applicationName.ToLower())));
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