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
		List<Shortcut> filteredShorcuts;
		NSTableView tableView;
		MainWindowController mainWindow;
		String currentFilter = "";
		String selectedApplication = "Google Chrome";

		public ShortcutTableModel (NSTableView tableView, MainWindowController mainWindow)
		{
			this.filteredShorcuts = MainClass.getShortcutList(selectedApplication);
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
				return new NSString (filteredShorcuts[row].getApplicationName());

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
			IEnumerable<Shortcut> query = MainClass.getShortcutList(selectedApplication).Where(s => (s.getApplicationName().ToLower().Contains(filter.ToLower())||s.Description.ToLower().Contains(filter.ToLower())));
			filteredShorcuts = query.ToList();
			filteredShorcuts.Sort ();
			tableView.ReloadData ();

			if (filteredShorcuts.Count > 0) {
				mainWindow.enableButtons (true);
			} else {
				mainWindow.enableButtons (false);
			}
		}

		public void addNewShortcut(string selectedApp, Shortcut shortcut)
		{
			MainClass.addShortcut(selectedApp, shortcut);
			filter (currentFilter);
		}

		public string removeShortcut(int idInFilteredList)
		{
			MainClass.removeShortcut(selectedApplication, filteredShorcuts [idInFilteredList]);
			filter (currentFilter);
			return selectedApplication;
		}

		public Shortcut getFilteredShortcut(int filteredRowNr)
		{
			return filteredShorcuts [filteredRowNr];
		}

		public void setSelectedApplication(int id)
		{
			//Sorta ugly hack to prevent this from going to -1 when removing a category
			if (id < 0) {
				id = 0;
			}
			selectedApplication = MainClass.getApplicationList () [id].Identifier;
			filter (currentFilter);
		}
	}
}