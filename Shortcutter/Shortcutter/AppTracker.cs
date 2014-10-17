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
				MainClass.SendNotification ("Shortcutter","You've switched to "+currentlyActiveApp);
			});
		}

		public string GetActiveApp()
		{
			return currentlyActiveApp;
		}
	}
}

