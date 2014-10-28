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
		SettingsController aSettingsController;

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

			ClickMeToolbar.Activated += (object sender, EventArgs e) => {
				if(aAddEntryController == null)
				{
					aAddEntryController = new AddEntryController();
				}
				Shortcut result = aAddEntryController.edit(null, this);
				if(result != null){
					tm.addNewShortcut(result.Description, result.ShortcutAction);
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
						tm.addNewShortcut(result.Description, result.ShortcutAction);
					}
				}
			};

			SettingsButton.Activated += (object sender, EventArgs e) => {
				if(aSettingsController == null)
				{
					aSettingsController = new SettingsController();
				}
				aSettingsController.editSettings(this);
			};

			tm = new ShortcutTableModel (ShortcutTable, this);
			ShortcutTable.DataSource = tm;

			//Adding a model to the sidebar and setting a delegate for notifications
			SidebarModel sm = new SidebarModel ();
			SidebarOutlineView.DataSource = sm;
			SidebarOutlineView.Delegate = new OutlineDelegate (tm, SidebarOutlineView);


			Console.Out.WriteLine ("observer set");

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
	}

	class OutlineDelegate : NSOutlineViewDelegate
	{
		private ShortcutTableModel tm;
		private NSOutlineView outlineView;

		public OutlineDelegate(ShortcutTableModel tm, NSOutlineView outlineView)
		{
			this.tm = tm;
			this.outlineView = outlineView;
		}

		public override void SelectionDidChange (NSNotification notification)
		{
			tm.setSelectedApplication (outlineView.SelectedRow);
		}
	}
}