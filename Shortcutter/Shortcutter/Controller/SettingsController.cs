
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
	public partial class SettingsController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public SettingsController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public SettingsController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public SettingsController () : base ("Settings")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion
		private NSApplication NSApp = NSApplication.SharedApplication;

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			ButtonSave.Activated += (object sender, EventArgs e) => {
				bool enabled = Convert.ToBoolean(CheckEnableNotifications.IntValue);
				int waitCon;
				int.TryParse(FieldContextSwitchWaittime.StringValue, out waitCon);
				int waitNext;
				int.TryParse(FieldNextNotificationWaitTime.StringValue,out waitNext);

				MainClass.UpdateSettings(enabled, waitCon, waitNext);
				NSApp.StopModal();
			};

			ButtonCancel.Activated += (object sender, EventArgs e) => {
				NSApp.StopModal();
			};
		}

		//strongly typed window accessor
		public new Settings Window {
			get {
				return (Settings)base.Window;
			}
		}

		public void editSettings(MainWindowController sender)
		{
			NSWindow window = this.Window;
		
			CheckEnableNotifications.IntValue = Convert.ToInt32(MainClass.isNotificationEnabled());
			FieldContextSwitchWaittime.StringValue = ""+MainClass.getWaittimeAfterContextSwitch();
			FieldNextNotificationWaitTime.StringValue = ""+MainClass.getWaittimeBeforeNextNotification();

			NSApp.BeginSheet(window,sender.Window);
			NSApp.RunModalForWindow(window);
			// sheet is up here.....

			// when StopModal is called will continue here ....
			NSApp.EndSheet(window);
			window.OrderOut(this);
		}
	}
}

