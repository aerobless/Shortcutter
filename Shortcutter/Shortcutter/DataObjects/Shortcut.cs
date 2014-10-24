using System;
using System.Runtime.Serialization;

namespace Shortcutter
{
	public class Shortcut
	{
		[DataMember()]
		public Application parentApplication{ get; set;}

		[DataMember()]
		public string Description{ get; set; }

		[DataMember()]
		public string ShortcutAction{ get; set; }

		[DataMember()]
		public Boolean learnedShortcut{ get; set;}

		[DataMember()]
		public int nofShowed;

		public Shortcut ()
		{
		}

		public Shortcut (Application parentApplication, string Description, string Shortcut)
		{
			this.parentApplication = parentApplication;
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

		public string getApplicationName(){
			return parentApplication.DisplayName;
		}

		public string getApplicationIdentifier(){
			return parentApplication.Identifier;
		}
	}
}