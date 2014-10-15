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
		List<Application> appList;

		public ShortcutTableModel (List<Application> appList)
		{
			this.appList = appList;
		}
	
		// how many rows are in the table
		public override int GetRowCount (NSTableView tableView)
		{
			return appList.Count;
		}

		// what to draw in the table
		public override NSObject GetObjectValue (NSTableView tableView, 
			NSTableColumn tableColumn, 
			int row)
		{
			if (tableColumn.Identifier == "applicationColumn")
				return new NSString (appList[row].Name);

			if (tableColumn.Identifier == "shortcutColumn")
				return new NSString ("ddd");

			throw new NotImplementedException (string.Format ("{0} is not recognized", 
				tableColumn.Identifier));
		}
	}
}