using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
namespace Shortcutter
{
	public class ShortcutTableModel : NSTableViewDataSource
	{
		List<Shortcut> shortcuts;

		public ShortcutTableModel (List<Shortcut> shortcuts)
		{
			this.shortcuts = shortcuts;
		}
	
		// how many rows are in the table
		public override int GetRowCount (NSTableView tableView)
		{
			return shortcuts.Count;
		}

		// what to draw in the table
		public override NSObject GetObjectValue (NSTableView tableView, 
			NSTableColumn tableColumn, 
			int row)
		{
			if (tableColumn.Identifier == "applicationColumn")
				return new NSString (shortcuts[row].ApplicationName);

			if (tableColumn.Identifier == "descriptionColumn")
				return new NSString (shortcuts[row].Description);

			if (tableColumn.Identifier == "shortcutColumn")
				return new NSString (shortcuts[row].ShortcutAction);

			throw new NotImplementedException (string.Format ("{0} is not recognized", 
				tableColumn.Identifier));
		}
	}
}