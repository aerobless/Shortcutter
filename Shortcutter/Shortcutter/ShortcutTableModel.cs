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
		List<Shortcut> allShortcuts;
		List<Shortcut> filteredShorcuts;
		NSTableView tableView;
		MainWindowController mainWindow;
		String currentFilter = "";

		public ShortcutTableModel (List<Shortcut> shortcuts, NSTableView tableView, MainWindowController mainWindow)
		{
			allShortcuts = shortcuts;
			this.filteredShorcuts = allShortcuts;
			this.tableView = tableView;
			this.mainWindow = mainWindow;
		}
	
		// how many rows are in the table
		public override int GetRowCount (NSTableView tableView)
		{
			return filteredShorcuts.Count;
		}

		// what to draw in the table
		public override NSObject GetObjectValue (NSTableView tableView, 
			NSTableColumn tableColumn, 
			int row)
		{
			if (tableColumn.Identifier == "applicationColumn")
				return new NSString (filteredShorcuts[row].ApplicationName);

			if (tableColumn.Identifier == "descriptionColumn")
				return new NSString (filteredShorcuts[row].Description);

			if (tableColumn.Identifier == "shortcutColumn")
				return new NSString (filteredShorcuts[row].ShortcutAction);

			throw new NotImplementedException (string.Format ("{0} is not recognized", 
				tableColumn.Identifier));
		}

		public void filter(string filter)
		{
			currentFilter = filter;
			IEnumerable<Shortcut> query = allShortcuts.Where(s => (s.ApplicationName.ToLower().Contains(filter.ToLower())||s.Description.ToLower().Contains(filter.ToLower())));
			filteredShorcuts = query.ToList();
			tableView.ReloadData ();

			if (filteredShorcuts.Count > 0) {
				mainWindow.enableButtons (true);
			} else {
				mainWindow.enableButtons (false);
			}
		}

		public void addNewShortcut(Shortcut shortcut)
		{
			allShortcuts.Add (shortcut);
			filter (currentFilter);
			MainClass.SaveToDisk (allShortcuts);
		}

		public void removeShortcut(int idInFilteredList)
		{
			allShortcuts.Remove (filteredShorcuts [idInFilteredList]);
			filter (currentFilter);
			MainClass.SaveToDisk (allShortcuts);
		}

		public Shortcut getFilteredShortcut(int filteredRowNr)
		{
			return filteredShorcuts [filteredRowNr];
		}
	}
}