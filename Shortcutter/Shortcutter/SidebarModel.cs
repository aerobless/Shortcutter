using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Shortcutter
{
	public class SidebarModel  : NSOutlineViewDataSource
	{
		public SidebarModel ()
		{
		}
			

		public override int GetChildrenCount (NSOutlineView outlineView, NSObject item)
		{
			return 3;
		}

		public override NSObject GetObjectValue (NSOutlineView outlineView, NSTableColumn forTableColumn, NSObject byItem)
		{
			return (NSString)"TEST";
		}

		public override NSObject GetChild (NSOutlineView outlineView, int childIndex, NSObject ofItem)
		{
			return (NSString)"TEST";
		}

		public override bool ItemExpandable (NSOutlineView outlineView, NSObject item)
		{
			return false;
		}

	}
}

