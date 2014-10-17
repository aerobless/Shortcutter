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
	[Register ("AppDelegate")]
	partial class AppDelegate
	{
		[Outlet]
		MonoMac.AppKit.NSMenuItem NewWindowMenu { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NewWindowMenu != null) {
				NewWindowMenu.Dispose ();
				NewWindowMenu = null;
			}
		}
	}
}
