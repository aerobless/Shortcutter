using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Shortcutter
{
	//HowTo: http://stackoverflow.com/questions/12465528/creating-a-simple-nsoutlineview-datasource-with-monomac
	public class SidebarModel  : NSOutlineViewDataSource
	{
		List<Application> appList;

		public SidebarModel ()
		{
			appList = MainClass.GetApplicationList ();

			MainClass.ApplicationListChanged += () => {
				refreshAppList ();
			};
		}

		public override int GetChildrenCount (NSOutlineView outlineView, NSObject item)
		{
			// If the item is not null, return the child count of our item
			if (item != null)
				return 0;
			return appList.Count ();
		}

		public override NSObject GetObjectValue (NSOutlineView outlineView, NSTableColumn forTableColumn, NSObject byItem)
		{
			if (byItem != null) {
				return byItem;
			}
			return (NSString)"Error in GetObjectValue()";
		}

		public override NSObject GetChild (NSOutlineView outlineView, int childIndex, NSObject ofItem)
		{
			return (NSString)appList [childIndex].Identifier;
		}

		public override bool ItemExpandable (NSOutlineView outlineView, NSObject item)
		{
			return false;
		}

		public string GetApplicationForPos (int pos)
		{
			try {
				return appList [pos].Identifier;
			} catch (ArgumentOutOfRangeException) {
				return "";
			}
		}

		public void refreshAppList ()
		{
			List<Application> updatedAppList = MainClass.GetApplicationList ();
			if (updatedAppList != null) {
				appList = updatedAppList;
			} else {
				Console.Out.WriteLine ("AppList is null.. error");
			}
		}
	}
}