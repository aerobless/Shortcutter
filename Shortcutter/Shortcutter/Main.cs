using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;


namespace Shortcutter
{
	class MainClass
	{
		private const string STORAGE_FILENAME = "shortcutter.xml";
		private static ApplicationSettings settings = new ApplicationSettings();

		private static AppTracker apptracker;

		static void Main (string[] args)
		{
			NSApplication.Init ();

			if (File.Exists (getStoragePath ()))
			{
				Console.Out.WriteLine (STORAGE_FILENAME+" found, loading existing data..");
				readFromDisk ();
			} else 
			{
				Console.Out.WriteLine (STORAGE_FILENAME+" not found, loading demo data..");
				settings.loadDemoContent ();
				SaveToDisk ();
			}
				
			apptracker = new AppTracker ();
			NSApplication.Main (args);
		}

		public static void SaveToDisk()
		{
			string savePath = Path.Combine(Directory.GetCurrentDirectory(), getStoragePath());

			var ds = new DataContractSerializer(typeof(ApplicationSettings));
			var xmlsettings = new XmlWriterSettings { Indent = true };
			using (var w = XmlWriter.Create(savePath, xmlsettings))
				ds.WriteObject(w, settings);
		}

		private static void readFromDisk()
		{
			DataContractSerializer ds = new DataContractSerializer(typeof(ApplicationSettings));
			FileStream fs = new FileStream(getStoragePath(), FileMode.Open);
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

			settings = (ApplicationSettings)ds.ReadObject(reader);
			reader.Close();
			fs.Close();

			Console.Out.WriteLine (settings.appDict["Google Chrome"].Description);
			Console.Out.WriteLine (settings.appDict["Google Chrome"].ShortcutList[1].Description);
			/*
			System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ApplicationSettings));
			System.IO.StreamReader file = new System.IO.StreamReader(getStoragePath());
			settings =(ApplicationSettings)reader.Deserialize(file);
			*/
		}

		private static String getStoragePath()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			Console.Out.WriteLine ("Storage-Location: "+Path.Combine (documents, STORAGE_FILENAME));
			return Path.Combine (documents, STORAGE_FILENAME);
		}

		public static void SendNotification (string title, string text)
		{
			NSUserNotification not = new NSUserNotification();

			not.Title = title;
			not.InformativeText = text;
			not.DeliveryDate = DateTime.Now;
			not.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;
			not.ActionButtonTitle = new NSString ("test");

			// We get the Default notification Center
			NSUserNotificationCenter center = NSUserNotificationCenter.DefaultUserNotificationCenter;

			center.DidDeliverNotification += (s, e) => 
			{
				//DeliveredColorWell.Color = NSColor.Green;
			};

			center.DidActivateNotification += (s, e) => 
			{
				//TouchedColorWell.Color = NSColor.Green;
			};

			// If we return true here, Notification will show up even if your app is TopMost.
			center.ShouldPresentNotification = (c, n) => { return true; };

			center.ScheduleNotification(not);
		}

		public static List<Shortcut> getShortcutList(string application)
		{
			return settings.getShortcutsFor (application);
		}

		public static void addShortcut(string application, string description, string shortcutText)
		{
			settings.addShortcut(application, description, shortcutText);
			SaveToDisk ();
		}

		public static void removeShortcut(string application,Shortcut shortcut)
		{
			settings.removeShortcut(application, shortcut);
			SaveToDisk ();
		}

		public static bool isNotificationEnabled()
		{
			return settings.NotificationsEnabled;
		}

		public static int getWaittimeAfterContextSwitch()
		{
			return settings.WaittimeAfterContextSwitch;
		}

		public static int getWaittimeBeforeNextNotification()
		{
			return settings.WaittimeBeforeNextNotification;
		}

		public static void UpdateSettings(bool notificationEnabled, int waittimeAfterContextSwitch, int waittimeBeforeNextNotification)
		{
			settings.NotificationsEnabled = notificationEnabled;
			settings.WaittimeAfterContextSwitch = waittimeAfterContextSwitch;
			settings.WaittimeBeforeNextNotification = waittimeBeforeNextNotification;
		}
	}
}	