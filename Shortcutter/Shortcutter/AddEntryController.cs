
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

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			okButton.Activated += (object sender, EventArgs e) => {
				// save the values for later
				savedValues = new Shortcut(applicationField.StringValue, descriptionField.StringValue, shortcutField.StringValue);
				NSApp.StopModal();
			};

			cancelButton.Activated += (object sender, EventArgs e) => {
				NSApp.StopModal();
				cancelled = true;
			};
		}

		private bool cancelled{ get; set;}
		private Shortcut savedValues;
		private NSApplication NSApp = NSApplication.SharedApplication;

		//strongly typed window accessor
		public new AddEntry Window {
			get {
				return (AddEntry)base.Window;
			}
		}

		public Shortcut edit(Shortcut editingAShortcut, MainWindowController sender)
		{

			NSWindow window = this.Window;

			cancelled = false;

			if (editingAShortcut != null)
			{
				applicationField.StringValue = editingAShortcut.ApplicationName;
				descriptionField.StringValue = editingAShortcut.Description;
				shortcutField.StringValue = editingAShortcut.ShortcutAction;
			}
			else
			{
				// we are adding a new entry,
				// make sure the form fields are empty due to the fact that this controller is recycled
				// each time the user opens the sheet -				
				applicationField.StringValue = string.Empty;
				descriptionField.StringValue = string.Empty;
				shortcutField.StringValue = string.Empty;
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
				return savedValues;
			}
		}
	}
}