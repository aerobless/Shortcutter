using System;
using System.Runtime.Serialization;

namespace Shortcutter
{
	public class Shortcut:IComparable<Shortcut>
	{
		[DataMember ()]
		public Application parentApplication{ get; set; }

		[DataMember ()]
		public string Description{ get; set; }

		[DataMember ()]
		public string ShortcutAction{ get; set; }

		[DataMember ()]
		public Boolean learnedShortcut{ get; set; }

		[DataMember ()]
		public int nofShowed;

		public Shortcut ()
		{
		}

		public Shortcut (string Description, string Shortcut)
		{
			this.Description = Description;
			this.ShortcutAction = Shortcut;
			this.nofShowed = 0;
			this.learnedShortcut = false;
		}

		public void SetParentApplication (Application parentApplication)
		{
			this.parentApplication = parentApplication;
		}

		public void IncrementNofShowed ()
		{
			++nofShowed;
		}

		public void ResetNofShowed ()
		{
			nofShowed = 0;
		}

		public string GetApplicationName ()
		{
			return parentApplication.Identifier;
		}

		public string GetApplicationIdentifier ()
		{
			return parentApplication.Identifier;
		}

		public int CompareTo (Shortcut shortcut)
		{
			return String.Compare (this.Description, shortcut.Description);
		}
	}
}