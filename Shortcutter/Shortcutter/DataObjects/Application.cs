using System;
using System.Collections.Generic;

namespace Shortcutter
{
	public class Application
	{
		public string DisplayName{ get; set; }
		public string Identifier{ get; set; }
		public string Description{ get; set; }
		public List<Shortcut> ShortcutList{ get; set; }

		public Application (){}

		public Application (string DisplayName, string Identifier, string Description)
		{
			this.DisplayName = DisplayName;
			this.Identifier = Identifier;
			this.Description = Description;
			ShortcutList = new List<Shortcut>();
		}

		public void addShortcut(Shortcut AShortcut)
		{
			ShortcutList.Add (AShortcut);
		}

		public void removeShortcut(Shortcut AShortcut)
		{
			ShortcutList.Remove (AShortcut);
		}
	}
}

