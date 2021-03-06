﻿using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace Shortcutter
{
	/*
	 * Not2FutureSelf: Modal Window: Be sure to set the UI.xib's "Visible At Start" to false! Otherwise
	 * it won't appear modal but rather disconnected from the main window.
	 */
	public partial class ShortcutEntryController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public ShortcutEntryController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public ShortcutEntryController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public ShortcutEntryController () : base ("AddEntry")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}

		#endregion

		private bool cancelled{ get; set; }

		private Shortcut savedValues;
		private NSApplication NSApp = NSApplication.SharedApplication;

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			okButton.Activated += (object sender, EventArgs e) => {
				// save the values for later
				savedValues = new Shortcut (descriptionField.StringValue, shortcutField.StringValue);
				savedValues.learnedShortcut = Convert.ToBoolean (learnedCheckbox.IntValue); 
				NSApp.StopModal ();
			};

			cancelButton.Activated += (object sender, EventArgs e) => {
				NSApp.StopModal ();
				cancelled = true;
			};
		}

		//strongly typed window accessor
		public new AddEntry Window {
			get {
				return (AddEntry)base.Window;
			}
		}

		private void createAppMenu ()
		{
			applicationMenu.RemoveAllItems ();
			List<Application> appList = MainClass.GetApplicationList ();

			//Remove the "All"-category
			appList.RemoveAt (0);

			appList.ForEach (app => applicationMenu.AddItem (new NSMenuItem (app.Identifier)));
			applicationMenu.AddItem (new NSMenuItem ("New application ..."));
		}

		public ShortcutResponse Edit (Shortcut editingAShortcut, MainWindowController sender)
		{
			return Edit (editingAShortcut, null, sender);
		}

		public ShortcutResponse Edit (string selectedApp, MainWindowController sender)
		{
			return Edit (null, selectedApp, sender);
		}

		public ShortcutResponse Edit (Shortcut editingAShortcut, string selectedApp, MainWindowController sender)
		{

			NSWindow window = this.Window;
			cancelled = false;
			createAppMenu ();
			if (editingAShortcut != null) {
				int idOfApplicationInMenu = MainClass.GetApplicationList ().FindIndex (delegate(Application app) {
					return app.Identifier.Equals (editingAShortcut.GetApplicationIdentifier ());
				});
				applicationMenuSwitcher.SelectItem (idOfApplicationInMenu);
				descriptionField.StringValue = editingAShortcut.Description;
				shortcutField.StringValue = editingAShortcut.ShortcutAction;
				learnedCheckbox.IntValue = Convert.ToInt32 (editingAShortcut.learnedShortcut);
			} else {
				// empty controller for new entry
				descriptionField.StringValue = string.Empty;
				shortcutField.StringValue = string.Empty;
				learnedCheckbox.IntValue = 0;

				//setting the menu-item to the selected application in the sidebar
				int idOfApplicationInMenu = MainClass.GetApplicationList ().FindIndex (delegate(Application app) {
					return app.Identifier.Equals (selectedApp);
				});

				//Cheap fix because of "All"
				--idOfApplicationInMenu;
				if (idOfApplicationInMenu <= 0) {
					idOfApplicationInMenu = 0;
				}

				if (applicationMenuSwitcher.ItemCount == 1) {
					applicationMenuSwitcher.SelectItem (0);
				} else {
					applicationMenuSwitcher.SelectItem (idOfApplicationInMenu);
				}
			}

			NSApp.BeginSheet (window, sender.Window);
			NSApp.RunModalForWindow (window);
			// sheet is up here.....

			// when StopModal is called will continue here ....
			NSApp.EndSheet (window);
			window.OrderOut (this);
			if (cancelled) {
				return null;
			} else {
				string selected = applicationMenuSwitcher.SelectedItem.Title;
				bool newAppModal = false;
				if (selected.Equals ("New application ...")) {
					newAppModal = true;
				}
				return new ShortcutResponse (savedValues, newAppModal, selected);
			}
		}
	}
}