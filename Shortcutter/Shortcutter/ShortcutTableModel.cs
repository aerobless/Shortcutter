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
		private List<Shortcut> shortcutSourceList;
		private List<Shortcut> filteredShorcuts;

		private String currentFilter = "";

		//Events
		public event Action ModelChanged;
		public event Action<bool> EmptyModel;

		public ShortcutTableModel ()
		{
		}
	
		// how many rows are in the table
		public override int GetRowCount (NSTableView tableView)
		{
			if (filteredShorcuts == null) {
				return 0;
			} else {
				return filteredShorcuts.Count ();
			}
		}

		// what to draw in the table
		public override NSObject GetObjectValue (NSTableView tableView, 
		                                         NSTableColumn tableColumn, 
		                                         int row)
		{
			if (tableColumn.Identifier == "applicationColumn")
				return new NSString (filteredShorcuts [row].GetApplicationName ());

			if (tableColumn.Identifier == "descriptionColumn")
				return new NSString (filteredShorcuts [row].Description);

			if (tableColumn.Identifier == "shortcutColumn")
				return new NSString (filteredShorcuts [row].ShortcutAction);

			throw new NotImplementedException (string.Format ("{0} is not recognized", 
				tableColumn.Identifier));
		}

		public void Filter ()
		{
			Filter (currentFilter);
		}

		public void Filter (string filter)
		{
			currentFilter = filter;
			if (shortcutSourceList != null) {
				IEnumerable<Shortcut> query = shortcutSourceList.Where (s => s.Description.ToLower ().Contains (filter.ToLower ()));
				filteredShorcuts = query.ToList ();
				filteredShorcuts.Sort ();
				ModelChanged ();
				if (filteredShorcuts.Count () > 0) {
					EmptyModel (false);
				} else {
					EmptyModel (true);
				}
			} else {
				EmptyModel (true);
			}
		}

		public void AddNewShortcut (string selectedApp, Shortcut shortcut)
		{
			MainClass.AddShortcut (selectedApp, shortcut);
			Filter ();
		}

		public string RemoveShortcut (int idInFilteredList)
		{
			string appID = filteredShorcuts [idInFilteredList].parentApplication.Identifier;
			MainClass.RemoveShortcut (appID, filteredShorcuts [idInFilteredList]);
			Filter ();
			return appID;
		}

		public Shortcut GetFilteredShortcut (int filteredRowNr)
		{
			return filteredShorcuts [filteredRowNr];
		}

		public void updateShortcutSource (string selectedApp)
		{
			this.shortcutSourceList = MainClass.GetShortcutList (selectedApp);
			Filter ();
		}
	}
}