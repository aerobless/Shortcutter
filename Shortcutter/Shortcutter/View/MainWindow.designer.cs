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
		MonoMac.AppKit.NSToolbarItem AddShortcutButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSToolbarItem EditButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSToolbarItem RemoveButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSSearchField SearchField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSToolbarItem SettingsButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSToolbarItem shareButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView ShortcutTable { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextFieldCell SidebarCell { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableColumn SidebarColumn { get; set; }

		[Outlet]
		MonoMac.AppKit.NSOutlineView SidebarOutlineView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSScrollView SidebarScroller { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddShortcutButton != null) {
				AddShortcutButton.Dispose ();
				AddShortcutButton = null;
			}

			if (EditButton != null) {
				EditButton.Dispose ();
				EditButton = null;
			}

			if (RemoveButton != null) {
				RemoveButton.Dispose ();
				RemoveButton = null;
			}

			if (SearchField != null) {
				SearchField.Dispose ();
				SearchField = null;
			}

			if (SettingsButton != null) {
				SettingsButton.Dispose ();
				SettingsButton = null;
			}

			if (shareButton != null) {
				shareButton.Dispose ();
				shareButton = null;
			}

			if (ShortcutTable != null) {
				ShortcutTable.Dispose ();
				ShortcutTable = null;
			}

			if (SidebarCell != null) {
				SidebarCell.Dispose ();
				SidebarCell = null;
			}

			if (SidebarColumn != null) {
				SidebarColumn.Dispose ();
				SidebarColumn = null;
			}

			if (SidebarOutlineView != null) {
				SidebarOutlineView.Dispose ();
				SidebarOutlineView = null;
			}

			if (SidebarScroller != null) {
				SidebarScroller.Dispose ();
				SidebarScroller = null;
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
