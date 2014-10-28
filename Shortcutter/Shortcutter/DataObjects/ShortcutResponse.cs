using System;

namespace Shortcutter
{
	public class ShortcutResponse
	{
		public Shortcut Shortcut{ get; set;}
		public Boolean NewAppModal{ get; set;}
		public string ApplicationIdentifier{ get; set;}

		public ShortcutResponse (Shortcut Shortcut,Boolean NewAppModal,string ApplicationIdentifier )
		{
			this.Shortcut = Shortcut;
			this.NewAppModal = NewAppModal;
			this.ApplicationIdentifier = ApplicationIdentifier;
		}
	}
}