using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Shortcutter
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);

			NewWindowMenu.Activated += (object sender, EventArgs e) => {
				//Open the main window and give it focus. Does nothing if the main window is already open.
				mainWindowController.Window.MakeKeyAndOrderFront (this);
			};
		}
	}
}

