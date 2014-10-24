// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace Shortcutter
{
	[Register ("SettingsController")]
	partial class SettingsController
	{
		[Outlet]
		MonoMac.AppKit.NSButton ButtonCancel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton ButtonSave { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton CheckEnableNotifications { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField FieldContextSwitchWaittime { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField FieldNextNotificationWaitTime { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CheckEnableNotifications != null) {
				CheckEnableNotifications.Dispose ();
				CheckEnableNotifications = null;
			}

			if (FieldContextSwitchWaittime != null) {
				FieldContextSwitchWaittime.Dispose ();
				FieldContextSwitchWaittime = null;
			}

			if (FieldNextNotificationWaitTime != null) {
				FieldNextNotificationWaitTime.Dispose ();
				FieldNextNotificationWaitTime = null;
			}

			if (ButtonSave != null) {
				ButtonSave.Dispose ();
				ButtonSave = null;
			}

			if (ButtonCancel != null) {
				ButtonCancel.Dispose ();
				ButtonCancel = null;
			}
		}
	}

	[Register ("Settings")]
	partial class Settings
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
