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
	[Register ("AddEntryController")]
	partial class ShortcutEntryController
	{
		[Outlet]
		MonoMac.AppKit.NSMenu applicationMenu { get; set; }

		[Outlet]
		MonoMac.AppKit.NSPopUpButton applicationMenuSwitcher { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton cancelButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField descriptionField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton learnedCheckbox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton okButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField shortcutField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}

			if (descriptionField != null) {
				descriptionField.Dispose ();
				descriptionField = null;
			}

			if (learnedCheckbox != null) {
				learnedCheckbox.Dispose ();
				learnedCheckbox = null;
			}

			if (okButton != null) {
				okButton.Dispose ();
				okButton = null;
			}

			if (shortcutField != null) {
				shortcutField.Dispose ();
				shortcutField = null;
			}

			if (applicationMenu != null) {
				applicationMenu.Dispose ();
				applicationMenu = null;
			}

			if (applicationMenuSwitcher != null) {
				applicationMenuSwitcher.Dispose ();
				applicationMenuSwitcher = null;
			}
		}
	}

	[Register ("AddEntry")]
	partial class AddEntry
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
