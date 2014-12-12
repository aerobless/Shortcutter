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
		private int selectedPosition = 0;

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

		public void RemoveShortcut ()
		{
			if (selectedPosition >= 0 && selectedPosition <= filteredShorcuts.Count ()) {
				string application = filteredShorcuts [selectedPosition].parentApplication.Identifier;
				MainClass.RemoveShortcut (application, filteredShorcuts [selectedPosition]);
				Filter ();

				if (shortcutSourceList.Count () == 0) {
					MainClass.RemoveApplication (application);
				}
			}
				
			/*
			if (selectedRow >= 0) {
				string selectedApplication = shortcutTableModel.RemoveShortcut (selectedRow);
				if (ShortcutTable.RowCount == 0) {
					MainClass.RemoveApplication (selectedApplication);
					SidebarOutlineView.ReloadData ();
					selectFirstApplicationIfPossible ();
				}

			return appID;*/
		}

		public Shortcut GetFilteredShortcut ()
		{
			return filteredShorcuts [selectedPosition];
		}

		public void updateShortcutSource (string selectedApp)
		{
			this.shortcutSourceList = MainClass.GetShortcutList (selectedApp);
			Filter ();
		}

		public void UpdateSelection (int pos)
		{
			selectedPosition = pos;
		}
	}
}