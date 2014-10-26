using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shortcutter
{
	[DataContract(IsReference = true)]
	public class Application
	{
		[DataMember()]
		public string DisplayName{ get; set; }
		[DataMember()]
		public string Identifier{ get; set; }
		[DataMember()]
		public string Description{ get; set; }
		[DataMember()]
		public List<Shortcut> ShortcutList{ get; set; }

		public Application (){}
		 
		public Application (string DisplayName, string Identifier, string Description)
		{
			this.DisplayName = DisplayName;
			this.Identifier = Identifier;
			this.Description = Description;
			ShortcutList = new List<Shortcut>();
		}

		public void addShortcut(string description, string shortcutText)
		{
			ShortcutList.Add (new Shortcut(this, description, shortcutText));
		}

		public void removeShortcut(Shortcut AShortcut)
		{
			ShortcutList.Remove (AShortcut);
		}

		public List<Shortcut> getShortcutList()
		{
			return ShortcutList;
		}
	}
}