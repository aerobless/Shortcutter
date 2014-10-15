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
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSButton ClickMeButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField OutputLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView ShortcutTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ClickMeButton != null) {
				ClickMeButton.Dispose ();
				ClickMeButton = null;
			}

			if (OutputLabel != null) {
				OutputLabel.Dispose ();
				OutputLabel = null;
			}

			if (ShortcutTable != null) {
				ShortcutTable.Dispose ();
				ShortcutTable = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
