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
		public static List<Shortcut> Shortcuts = new List<Shortcut> ();

		static void Main (string[] args)
		{
			loadDemoContent ();
			NSApplication.Init ();
			NSApplication.Main (args);
		}

		static void loadDemoContent()
		{
			Shortcuts.Add (new Shortcut ("Chrome","New tab.","CMD+T"));
			Shortcuts.Add (new Shortcut ("Chrome","New window.","CMD+N"));
			Shortcuts.Add (new Shortcut ("Chrome","New incognito window.","CMD+Shift-N"));
			Shortcuts.Add (new Shortcut ("Chrome","Close Tab","CMD+W"));

			Console.Out.WriteLine ("Demo-Content loaded..");
		}
	}
}	

