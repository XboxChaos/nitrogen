using Nitrogen.Wumbalo.Helpers;
using Nitrogen.Wumbalo.ViewModels.Dialogs;

namespace Nitrogen.Wumbalo.Modern.Dialogs
{
	/// <summary>
	/// 
	/// </summary>
	public static class ModernMessageBox
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public static void Show(string message)
		{
			Show("", message);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="title"></param>
		/// <param name="message"></param>
		public static void Show(string title, string message)
		{
			var dialog = new Controls.ModernMessageBox(new ModernMessageBoxViewModel(title, message));
			LeStorage.MainWindow.ShowDialog(dialog);
		}
	}
}
