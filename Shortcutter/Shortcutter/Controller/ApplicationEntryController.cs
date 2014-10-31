using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace Shortcutter
{
	public partial class ApplicationEntryController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public ApplicationEntryController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ApplicationEntryController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public ApplicationEntryController () : base ("AddApplication")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		private NSApplication NSApp = NSApplication.SharedApplication;
		private Application savedValue;
		private Boolean cancelled = false;

		//strongly typed window accessor
		public new AddApplication Window {
			get {
				return (AddApplication)base.Window;
			}
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			okButton.Activated += (object sender, EventArgs e) => {
				// save the values for later
				savedValue = new Application (nameField.StringValue, descriptionField.StringValue);
				NSApp.StopModal ();
			};

			cancelButton.Activated += (object sender, EventArgs e) => {
				NSApp.StopModal ();
				cancelled = true;
			};
		}

		public Application Edit (MainWindowController sender)
		{

			NSWindow window = this.Window;

			NSApp.BeginSheet (window, sender.Window);
			NSApp.RunModalForWindow (window);
			nameField.StringValue = "";
			descriptionField.StringValue = "";
			// sheet is up here.....

			// when StopModal is called will continue here ....
			NSApp.EndSheet (window);
			window.OrderOut (this);
			if (cancelled) {
				return null;
			} else {
				return savedValue;
			}
		}
	}
}