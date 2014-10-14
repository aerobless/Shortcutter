using System;

namespace Shortcutter
{
	public class Shortcut
	{
		public string Name{ get; set; }
		public string Description{ get; set; }
		public string ShortcutAction{ get; set; }

		public Shortcut (string Name, string Description, string Shortcut)
		{
			this.Name = Name;
			this.Description = Description;
			this.ShortcutAction = Shortcut;
		}
	}
}

