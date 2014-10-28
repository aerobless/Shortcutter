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
	partial class AddEntryController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField applicationField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton cancelButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField descriptionField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSForm editForm { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton learnedCheckbox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton okButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField shortcutField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (applicationField != null) {
				applicationField.Dispose ();
				applicationField = null;
			}

			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}

			if (descriptionField != null) {
				descriptionField.Dispose ();
				descriptionField = null;
			}

			if (editForm != null) {
				editForm.Dispose ();
				editForm = null;
			}

			if (okButton != null) {
				okButton.Dispose ();
				okButton = null;
			}

			if (shortcutField != null) {
				shortcutField.Dispose ();
				shortcutField = null;
			}

			if (learnedCheckbox != null) {
				learnedCheckbox.Dispose ();
				learnedCheckbox = null;
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
