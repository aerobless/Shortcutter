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
		MonoMac.AppKit.NSToolbarItem ClickMeToolbar { get; set; }

		[Outlet]
		MonoMac.AppKit.NSSearchField SearchField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView ShortcutTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ClickMeToolbar != null) {
				ClickMeToolbar.Dispose ();
				ClickMeToolbar = null;
			}

			if (SearchField != null) {
				SearchField.Dispose ();
				SearchField = null;
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
