using System;

namespace Shortcutter
{
	public class Shortcut
	{
		public string ApplicationName{ get; set;}
		public string Description{ get; set; }
		public string ShortcutAction{ get; set; }

		public Shortcut ()
		{
		}

		public Shortcut (string ApplicationName, string Description, string Shortcut)
		{
			this.ApplicationName = ApplicationName;
			this.Description = Description;
			this.ShortcutAction = Shortcut;
		}
	}
}

