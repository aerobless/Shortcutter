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
		// populate this in constructor, via service, setter, etc - whatever makes sense
		//private string[] data;

		public ShortcutTableModel ()
		{
		//	data = new string[]{"test","tessddt"};
		}
	
		// how many rows are in the table
		public override int GetRowCount (NSTableView tableView)
		{
			return 2;
		}

		// what to draw in the table
		public override NSObject GetObjectValue (NSTableView tableView, 
			NSTableColumn tableColumn, 
			int row)
		{
			if (tableColumn.Identifier == "applicationColumn")
				return new NSString ("ddd");

			if (tableColumn.Identifier == "shortcutColumn")
				return new NSString ("ddd");

			throw new NotImplementedException (string.Format ("{0} is not recognized", 
				tableColumn.Identifier));
		}
	}
}