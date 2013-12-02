using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nitrogen.Wumbalo.ViewModels.Dialogs
{
	public enum ModernMessageBoxButton
	{
		Okay,
		Yes,
		No,
		Cancel,
		Aite
	}

	/// <summary>
	/// 
	/// </summary>
	public class ModernMessageBoxViewModel : Inpc
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		public ModernMessageBoxViewModel(string title, string message)
		{
			Init(title, message, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <param name="buttons"></param>
		public ModernMessageBoxViewModel(string title, string message, List<ModernMessageBoxButton> buttons = null)
		{
			Init(title, message, buttons);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		/// <param name="buttons"></param>
		private void Init(string title, string message, List<ModernMessageBoxButton> buttons = null)
		{
			Title = title;
			Message = message;
			if (buttons == null)
				buttons = new List<ModernMessageBoxButton> { ModernMessageBoxButton.Okay };
			Buttons = new ObservableCollection<ModernMessageBoxButton>(buttons);
		}

		/// <summary>
		/// 
		/// </summary>
		public ObservableCollection<ModernMessageBoxButton> Buttons
		{
			get { return _buttons; }
			set { SetField(ref _buttons, value, "Buttons"); }
		}
		private ObservableCollection<ModernMessageBoxButton> _buttons = new ObservableCollection<ModernMessageBoxButton>();

		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			get { return _title; }
			set { SetField(ref _title, value, "Title"); }
		}
		private string _title;

		/// <summary>
		/// 
		/// </summary>
		public string Message
		{
			get { return _message; }
			set { SetField(ref _message, value, "Message"); }
		}
		private string _message;
	}
}
