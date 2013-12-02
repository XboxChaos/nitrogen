namespace Nitrogen.Wumbalo.ViewModels.Dialogs
{
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
			Title = title;
			Message = message;
		}

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
