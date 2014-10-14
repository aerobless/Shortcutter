using System;
using System.Collections.Generic;

namespace Shortcutter
{
	public class Application
	{
		public string Name{ get; set; }
		public string Description{ get; set; }
		public List<Shortcut> ShortcutList{ get; set; }

		public Application (string Name, string Description)
		{
			this.Name = Name;
			this.Description = Description;
			ShortcutList = new List<Shortcut>();
		}

		public void addShortcut(Shortcut AShortcut)
		{
			ShortcutList.Add (AShortcut);
		}
	}
}

