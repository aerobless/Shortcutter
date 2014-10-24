using System;

namespace Shortcutter
{
	public class Shortcut
	{
		public string ApplicationName{ get; set;}
		public string Description{ get; set; }
		public string ShortcutAction{ get; set; }
		public Boolean learnedShortcut{ get; set;}
		private int nofShowed;

		public Shortcut ()
		{
		}

		public Shortcut (string ApplicationName, string Description, string Shortcut)
		{
			this.ApplicationName = ApplicationName;
			this.Description = Description;
			this.ShortcutAction = Shortcut;
			this.nofShowed = 0;
			this.learnedShortcut = false;
		}

		public void IncrementNofShowed()
		{
			++nofShowed;
		}

		public void ResetNofShowed()
		{
			nofShowed = 0;
		}
	}
}