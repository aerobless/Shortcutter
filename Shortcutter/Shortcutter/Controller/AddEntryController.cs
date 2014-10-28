using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace Shortcutter
{
	/*
	 * Not2FutureSelf: Modal Window: Be sure to set the UI.xib's "Visible At Start" to false! Otherwise
	 * it won't appear modal but rather disconnected from the main window.
	 */
	public partial class AddEntryController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public AddEntryController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public AddEntryController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public AddEntryController () : base ("AddEntry")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}
		#endregion

		private bool cancelled{ get; set;}
		private Shortcut savedValues;
		private NSApplication NSApp = NSApplication.SharedApplication;
		private MainWindowController mainWindow;

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			okButton.Activated += (object sender, EventArgs e) => {
				// save the values for later
				savedValues = new Shortcut(descriptionField.StringValue, shortcutField.StringValue);
				savedValues.learnedShortcut = Convert.ToBoolean(learnedCheckbox.IntValue); 
				NSApp.StopModal();
			};

			cancelButton.Activated += (object sender, EventArgs e) => {
				NSApp.StopModal();
				cancelled = true;
			};
		}

		//strongly typed window accessor
		public new AddEntry Window {
			get {
				return (AddEntry)base.Window;
			}
		}

		private void createAppMenu()
		{
			applicationMenu.RemoveAllItems ();
			MainClass.getApplicationList().ForEach(app => applicationMenu.AddItem (new NSMenuItem (app.Identifier)));
			applicationMenu.AddItem (new NSMenuItem ("New application ..."));
		}

		public ShortcutResponse edit(Shortcut editingAShortcut, MainWindowController sender)
		{

			NSWindow window = this.Window;
			mainWindow = sender;
			cancelled = false;
			createAppMenu ();
			if (editingAShortcut != null)
			{
				descriptionField.StringValue = editingAShortcut.Description;
				shortcutField.StringValue = editingAShortcut.ShortcutAction;
				learnedCheckbox.IntValue = Convert.ToInt32(editingAShortcut.learnedShortcut);
			}
			else
			{
				// we are adding a new entry,
				// make sure the form fields are empty due to the fact that this controller is recycled
				// each time the user opens the sheet -				
				descriptionField.StringValue = string.Empty;
				shortcutField.StringValue = string.Empty;
				learnedCheckbox.IntValue = 0;
			}

			NSApp.BeginSheet(window,sender.Window);
			NSApp.RunModalForWindow(window);
			// sheet is up here.....

			// when StopModal is called will continue here ....
			NSApp.EndSheet(window);
			window.OrderOut(this);
			if (cancelled) {
				return null;
			} else {
				string selected = applicationMenuSwitcher.SelectedItem.Title;
				bool newAppModal = false;
				if(selected.Equals("New application ...")){newAppModal = true;}
				return new ShortcutResponse(savedValues, newAppModal, selected);
			}
		}
	}
}