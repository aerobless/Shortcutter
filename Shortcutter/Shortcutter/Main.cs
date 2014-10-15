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

		public static void SaveToDisk(List<Shortcut> shortcutList)
		{
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

		static String getStoragePath()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			Console.Out.WriteLine ("Storage-Location: "+Path.Combine (documents, "shortcuts.xml"));
			return Path.Combine (documents, "shortcuts.xml");
		}
	}
}	

