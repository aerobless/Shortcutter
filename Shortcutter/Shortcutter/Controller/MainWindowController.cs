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
			Console.Out.WriteLine ("Initialized MainWindowController");
			sidebarModel = new SidebarModel ();
			shortcutTableModel = new ShortcutTableModel ();
		}

		#endregion

		//Models
		private SidebarModel sidebarModel;
		private ShortcutTableModel shortcutTableModel;

		//Controllers
		private ShortcutEntryController shortcutEntryController;
		private ApplicationEntryController applicationEntryController;
		private SettingsController settingsController;

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			//Adding a model to the sidebar and setting a delegate for notifications
			SidebarOutlineView.DataSource = sidebarModel;
			OutlineDelegate sidebarDelegate = new OutlineDelegate ();
			SidebarOutlineView.Delegate = sidebarDelegate;

			sidebarDelegate.SelectionChanged += () => {
				List<Shortcut> updatedShortcutList = MainClass.GetShortcutList (getSelectedApplicationInSidebar ());
				shortcutTableModel.updateShortcutSource (updatedShortcutList);
			};
	
			//Adding the model for the shortcut-table
			ShortcutTable.DataSource = shortcutTableModel;
			shortcutTableModel.ModelChanged += () => {
				ShortcutTable.ReloadData ();
			};

			shortcutTableModel.EmptyModel += (empty) => {
				EnableButtons (!empty);
			};

			//Select the first entry
			if (SidebarOutlineView.RowCount >= 1) {
				SidebarOutlineView.SelectRow (0, false);
			} else {
				//Trigger the empty events right away.
				shortcutTableModel.Filter ();
			}

			AddShortcutButton.Activated += (object sender, EventArgs e) => {
				if (shortcutEntryController == null) {
					shortcutEntryController = new ShortcutEntryController ();
				}
				ShortcutResponse result = shortcutEntryController.Edit (getSelectedApplicationInSidebar (), this);
				if ((result != null) && (result.NewAppModal == false)) {
					shortcutTableModel.AddNewShortcut (result.ApplicationIdentifier, result.Shortcut);
				} else if ((result != null) && (result.NewAppModal == true)) {
					addNewApplicationAndShortcut (result.Shortcut);
				}
			};

			RemoveButton.Activated += (object sender, EventArgs e) => {
				int selectedRow = ShortcutTable.SelectedRow;
				if (selectedRow >= 0) {
					string selectedApplication = shortcutTableModel.RemoveShortcut (selectedRow);
					if (ShortcutTable.RowCount == 0) {
						MainClass.RemoveApplication (selectedApplication);
						SidebarOutlineView.ReloadData ();
					}
				}
			};

			EditButton.Activated += (object sender, EventArgs e) => {
				int selectedRow = ShortcutTable.SelectedRow;
				if (selectedRow >= 0) {
					if (shortcutEntryController == null) {
						shortcutEntryController = new ShortcutEntryController ();
					}
					ShortcutResponse result = shortcutEntryController.Edit (shortcutTableModel.GetFilteredShortcut (selectedRow), this);
					if (result != null && (result.NewAppModal == false)) {
						shortcutTableModel.RemoveShortcut (selectedRow);
						shortcutTableModel.AddNewShortcut (result.ApplicationIdentifier, result.Shortcut);
					} else if ((result != null) && (result.NewAppModal == true)) {
						addNewApplicationAndShortcut (result.Shortcut);
					}
				}
			};

			shareButton.Activated += (object sender, EventArgs e) => {
				/* 
				 * NSSavePanel HowTo Sandbox properly:
				 * In order for NSSavePanel to work in OS X Sandbox / AppStore-Mode
				 * we need to instantiate a new NSSavePanel() instead of just
				 * using NSSavePanel.SavePanel. (<-- which works in debug mode, but not sandboxed)
				 * 
				 * The lack of documentation of xamarin.mac is seriously annoying.
				 * I hope other devs having the same problem will find my code on github.
				 */
				NSSavePanel savePanel = new NSSavePanel ();
				//savePanel.Message = .;
				//int result = savePanel.RunModal ();
				NSSavePanelComplete complete = new NSSavePanelComplete (r => Console.Out.WriteLine ("DDD"));
				savePanel.BeginSheet (this.Window, complete);

				//NSApp.BeginSheet (window, sender.Window);
				//NSApp.RunModalForWindow (window);
			};

			SettingsButton.Activated += (object sender, EventArgs e) => {
				if (settingsController == null) {
					settingsController = new SettingsController ();
				}
				settingsController.EditSettings (this);
			};

			SearchField.Changed += searchEvent;
		}

		private void addNewApplicationAndShortcut (Shortcut shortcut)
		{
			if (applicationEntryController == null) {
				applicationEntryController = new ApplicationEntryController ();
			}
			Application newlyCreatedAndSelctedApp = applicationEntryController.Edit (this);
			MainClass.AddApplication (newlyCreatedAndSelctedApp);
			shortcutTableModel.AddNewShortcut (newlyCreatedAndSelctedApp.Identifier, shortcut);
			SidebarOutlineView.ReloadData ();
		}

		private void searchEvent (object sender, EventArgs e)
		{
			shortcutTableModel.Filter (SearchField.StringValue);
		}

		public void EnableButtons (bool shouldEnable)
		{
			EditButton.Enabled = shouldEnable;
			RemoveButton.Enabled = shouldEnable;
			shareButton.Enabled = shouldEnable;
		}

		public string getSelectedApplicationInSidebar ()
		{
			return sidebarModel.GetApplicationForPos (SidebarOutlineView.SelectedRow);
		}
	}

	class OutlineDelegate : NSOutlineViewDelegate
	{
		public event Action SelectionChanged;

		public OutlineDelegate ()
		{
		}

		public override void SelectionDidChange (NSNotification notification)
		{
			SelectionChanged ();
		}
	}
}