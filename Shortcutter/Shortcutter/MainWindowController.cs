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
		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			ClickMeButton.Activated += (object sender, EventArgs e) => {
				numberOfTimesClicked++;
				OutputLabel.StringValue = "Clicked " + 
					numberOfTimesClicked + " times.";
			};

			ShortcutTableModel tm = new ShortcutTableModel (MainClass.Shortcuts);

			//NSTableColumn applicationColum = ShortcutTable.FindTableColumn(APPLICATION);
			//applicationColum.Bind ("value",tm,"shortcutTable.applicationRow",null);

			ShortcutTable.DataSource = tm;

			//NSTableColumn shortcutColum = ShortcutTable.FindTableColumn(SHORTCUT);
			//shortcutColum.Bind("value",MainClass.Shortcuts,"arrangedObjects.lastname",null);

		}
	}
}