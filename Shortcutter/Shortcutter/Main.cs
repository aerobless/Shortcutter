using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml;
using System.Runtime.InteropServices;

namespace Shortcutter
{
	class MainClass
	{
		private const string STORAGE_FILENAME = "shortcutter.xml";
		private static ApplicationSettings settings = new ApplicationSettings ();

		private static AppTracker apptracker;

		static void Main (string[] args)
		{
			NSApplication.Init ();

			if (File.Exists (getStoragePath ())) {
				Console.Out.WriteLine (STORAGE_FILENAME + " found, loading existing data..");
				readFromDisk ();
			} else {
				Console.Out.WriteLine (STORAGE_FILENAME + " not found, loading demo data..");
				settings.loadDemoContent ();
				SaveToDisk ();
			}
				
			apptracker = new AppTracker ();
			apptracker.Run ();
			NSApplication.Main (args);
		}

		public static void SaveToDisk ()
		{
			string savePath = Path.Combine (Directory.GetCurrentDirectory (), getStoragePath ());

			var ds = new DataContractSerializer (typeof(ApplicationSettings));
			var xmlsettings = new XmlWriterSettings { Indent = true };
			using (var w = XmlWriter.Create (savePath, xmlsettings))
				ds.WriteObject (w, settings);
		}

		private static void readFromDisk ()
		{
			DataContractSerializer ds = new DataContractSerializer (typeof(ApplicationSettings));
			FileStream fs = new FileStream (getStoragePath (), FileMode.Open);
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader (fs, new XmlDictionaryReaderQuotas ());

			settings = (ApplicationSettings)ds.ReadObject (reader);
			reader.Close ();
			fs.Close ();

			Console.Out.WriteLine (settings.appDict ["Google Chrome"].Description);
			Console.Out.WriteLine (settings.appDict ["Google Chrome"].ShortcutList [1].Description);
		}

		private static String getStoragePath ()
		{
			Console.Out.WriteLine ("Storage-Location: " + Path.Combine (ContainerDirectory, STORAGE_FILENAME));
			return Path.Combine (ContainerDirectory, STORAGE_FILENAME);
		}

		public static void SendNotification (string title, string text)
		{
			NSUserNotification not = new NSUserNotification ();

			not.Title = title;
			not.InformativeText = text;
			not.DeliveryDate = DateTime.Now;
			not.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;
			not.ActionButtonTitle = new NSString ("test");

			// We get the Default notification Center
			NSUserNotificationCenter center = NSUserNotificationCenter.DefaultUserNotificationCenter;

			center.DidDeliverNotification += (s, e) => {
				//DeliveredColorWell.Color = NSColor.Green;
			};

			center.DidActivateNotification += (s, e) => {
				//TouchedColorWell.Color = NSColor.Green;
			};

			// If we return true here, Notification will show up even if your app is TopMost.
			center.ShouldPresentNotification = (c, n) => {
				return true;
			};

			center.ScheduleNotification (not);
		}

		public static List<Shortcut> GetShortcutList (string application)
		{
			return settings.getShortcutsFor (application);
		}

		public static void AddShortcut (string application, Shortcut shortcut)
		{
			settings.addShortcut (application, shortcut);
			SaveToDisk ();
		}

		public static void RemoveShortcut (string application, Shortcut shortcut)
		{
			settings.removeShortcut (application, shortcut);
			SaveToDisk ();
		}

		public static bool IsNotificationEnabled ()
		{
			return settings.NotificationsEnabled;
		}

		public static int GetWaittimeAfterContextSwitch ()
		{
			return settings.WaittimeAfterContextSwitch;
		}

		public static int GetWaittimeBeforeNextNotification ()
		{
			return settings.WaittimeBeforeNextNotification;
		}

		public static void UpdateSettings (bool notificationEnabled, int waittimeAfterContextSwitch, int waittimeBeforeNextNotification)
		{
			settings.NotificationsEnabled = notificationEnabled;
			settings.WaittimeAfterContextSwitch = waittimeAfterContextSwitch;
			settings.WaittimeBeforeNextNotification = waittimeBeforeNextNotification;
		}

		public static List<Application> GetApplicationList ()
		{
			return settings.getApplicationList ();
		}

		public static void AddApplication (Application application)
		{
			settings.addApplication (application);
		}

		public static void RemoveApplication (string applicationIdentifier)
		{
			settings.removeApplication (applicationIdentifier);
		}

		//Needs: "using System.Runtime.InteropServices;" to work!
		[DllImport (MonoMac.Constants.FoundationLibrary)]
		public static extern IntPtr NSHomeDirectory ();

		public static string ContainerDirectory {
			get {
				return ((NSString)Runtime.GetNSObject (NSHomeDirectory ())).ToString ();
			}
		}
	}
}