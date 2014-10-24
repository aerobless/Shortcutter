using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace Shortcutter
{
	public class ICloudAccessor
	{
		public ICloudAccessor ()
		{
		}

		public void accessUbiquityContainer()
		{
			var docsFolder = Path.Combine(iCloudUrl.Path, "Documents"); // NOTE: Documents folder is user-accessible in Settings
			var docPath = Path.Combine (docsFolder, monkeyDocFilename);
			var ubiq = new NSUrl(docPath, false);
			var doc = new MonkeyDocument(ubiq); // gets the 'default content'
			doc.Save (doc.FileUrl, UIDocumentSaveOperation.ForCreating
				, (saveSuccess) => { // completion handler #1
					if (saveSuccess) {
						doc.Open ((openSuccess) => { // completion handler #2
							if (openSuccess)
								viewController.DisplayDocument (doc); // document is opened
							else 
								Console.WriteLine ("couldn't open");
						});
					} else {
						Console.WriteLine ("couldn't save");
					}
				});
		}
	}
}

