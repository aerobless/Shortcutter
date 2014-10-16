using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Shortcutter
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		protected int numberOfTimesClicked = 0;
		ShortcutTableModel tm;

		internal static NSString APPLICATION = new NSString("applicationColumn");
		internal static NSString SHORTCUT = new NSString("shortcutColumn");

		internal static List<NSString> Keys = new List<NSString> { APPLICATION, SHORTCUT};			
		internal const int APPLICATION_IDX = 0;
		internal const int SHORTCUT_IDX = 1;

		#region Constructors
		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		// Shared initialization code
		void Initialize ()
		{
		}
		#endregion

		AddEntryController aAddEntryController;

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			RemoveButton.Activated += (object sender, EventArgs e) => {
				int selectedRow = ShortcutTable.SelectedRow;
				if(selectedRow>=0)
				{
					tm.removeShortcut(selectedRow);
				}
			};
			Console.Out.WriteLine ("here blah");
			ClickMeToolbar.Activated += (object sender, EventArgs e) => {
				if(aAddEntryController == null)
				{
					aAddEntryController = new AddEntryController();
				}
				Shortcut result = aAddEntryController.edit(null, this);
				if(result != null){
					tm.addNewShortcut(result);
					SendNotification ("Shortcutter","New Shortcut added.");
				}
			};

			EditButton.Activated += (object sender, EventArgs e) => {
				int selectedRow = ShortcutTable.SelectedRow;
				if(selectedRow>=0)
				{
					if(aAddEntryController == null)
					{
						aAddEntryController = new AddEntryController();
					}
					Shortcut result = aAddEntryController.edit(tm.getFilteredShortcut(selectedRow), this);
					if(result != null){
						tm.removeShortcut(selectedRow);
						tm.addNewShortcut(result);
					}
				}
			};

			tm = new ShortcutTableModel (MainClass.Shortcuts, ShortcutTable, this);
			ShortcutTable.DataSource = tm;
			SearchField.Changed += searchEvent;
		}

		void searchEvent(object sender, EventArgs e)
		{
			tm.filter (SearchField.StringValue);
		}

		public void enableButtons(bool shouldEnable)
		{
			EditButton.Enabled = shouldEnable;
			RemoveButton.Enabled = shouldEnable;
			if(shouldEnable){
				RemoveButton.Validate ();
				EditButton.Validate ();
			}
		}

		public void SendNotification (string title, string text)
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