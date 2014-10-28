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
	[Register ("AddApplicationController")]
	partial class AddApplicationController
	{
		[Outlet]
		MonoMac.AppKit.NSButton cancelButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField descriptionField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField nameField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton okButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (nameField != null) {
				nameField.Dispose ();
				nameField = null;
			}

			if (descriptionField != null) {
				descriptionField.Dispose ();
				descriptionField = null;
			}

			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}

			if (okButton != null) {
				okButton.Dispose ();
				okButton = null;
			}
		}
	}

	[Register ("AddApplication")]
	partial class AddApplication
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
