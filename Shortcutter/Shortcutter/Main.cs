using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;

namespace Shortcutter
{
	class MainClass
	{
		public static List<Application> Shortcuts = new List<Application> ();

		static void Main (string[] args)
		{
			loadDemoContent ();
			NSApplication.Init ();
			NSApplication.Main (args);
		}

		static void loadDemoContent()
		{
			Application chrome = new Application ("Chrome", "A webbrowser from Google");
			chrome.addShortcut (new Shortcut ("New tab","Opens a new tab.","crtl+T"));
			Shortcuts.Add (chrome);

			Console.Out.WriteLine ("Demo-Content loaded..");
		}
	}
}	

