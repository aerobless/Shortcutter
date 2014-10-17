using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;
using System.IO;

namespace Shortcutter
{
	class MainClass
	{
		public static List<Shortcut> Shortcuts = new List<Shortcut> ();

		static void Main (string[] args)
		{
			NSApplication.Init ();
			if (File.Exists (getStoragePath ()))
			{
				Console.Out.WriteLine ("shortcuts.xml found, loading existing data..");
				Shortcuts = readFromDisk ();
			} else 
			{
				Console.Out.WriteLine ("shortcuts.xml not found, loading demo data..");
				loadDemoContent ();
				SaveToDisk (Shortcuts);
			}
				
			AppTracker test = new AppTracker ();


			NSApplication.Main (args);
		}

		private static void loadDemoContent()
		{
			Shortcuts.Add (new Shortcut ("Google Chrome","New tab.","CMD+T"));
			Shortcuts.Add (new Shortcut ("Google Chrome","New window.","CMD+N"));
			Shortcuts.Add (new Shortcut ("Google Chrome","New incognito window.","CMD+Shift-N"));
			Shortcuts.Add (new Shortcut ("Google Chrome","Close Tab","CMD+W"));

			Console.Out.WriteLine ("Demo-Content loaded..");
		}

		public static void SaveToDisk(List<Shortcut> shortcutList)
		{
			//TODO: maybe ugly, need a better way to keep in sync or only use one List
			Shortcuts = shortcutList;

			string savePath = Path.Combine(Directory.GetCurrentDirectory(), getStoragePath());
			System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Shortcut>));

			System.IO.StreamWriter file = new System.IO.StreamWriter(savePath);
			writer.Serialize(file, shortcutList);
			file.Close();
		}

		private static List<Shortcut> readFromDisk()
		{
			System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Shortcut>));
			System.IO.StreamReader file = new System.IO.StreamReader(getStoragePath());
			return (List<Shortcut>)reader.Deserialize(file);
		}

		private static String getStoragePath()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			Console.Out.WriteLine ("Storage-Location: "+Path.Combine (documents, "shortcuts.xml"));
			return Path.Combine (documents, "shortcuts.xml");
		}

		public static void SendNotification (string title, string text)
		{
			// First we create our notification and customize as needed
			NSUserNotification not = new NSUserNotification();

			not.Title = title;
			not.InformativeText = text;
			not.DeliveryDate = DateTime.Now;
			not.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;

			// We get the Default notification Center
			NSUserNotificationCenter center = NSUserNotificationCenter.DefaultUserNotificationCenter;

			center.DidDeliverNotification += (s, e) => 
			{
				Console.WriteLine("Notification Delivered");
				//DeliveredColorWell.Color = NSColor.Green;
			};

			center.DidActivateNotification += (s, e) => 
			{
				Console.WriteLine("Notification Touched");
				//TouchedColorWell.Color = NSColor.Green;
			};


			// If we return true here, Notification will show up even if your app is TopMost.
			center.ShouldPresentNotification = (c, n) => { return true; };

			center.ScheduleNotification(not);

		}
	}
}	

