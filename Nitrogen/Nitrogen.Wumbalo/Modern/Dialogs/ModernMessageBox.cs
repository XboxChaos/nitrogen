using System.Collections.Generic;
using System.Threading.Tasks;
using Nitrogen.Wumbalo.Helpers;
using Nitrogen.Wumbalo.ViewModels.Dialogs;

namespace Nitrogen.Wumbalo.Modern.Dialogs
{
	/// <summary>
	/// A custom implementation of the Message Box system .NET utilizes by default.
	/// </summary>
	public static class ModernMessageBox
	{
		/// <summary>
		/// Creates a Modern Message Box that blocks application input until specified user input.
		/// </summary>
		/// <param name="message">The message you want to show in the Message Box.</param>
		public static async void Show(string message)
		{
			await Show("", message, null);
		}

		/// <summary>
		/// Creates a Modern Message Box that blocks application input until specified user input.
		/// </summary>
		/// <param name="title">The title you want to show in the Message Box.</param>
		/// <param name="message">The message you want to show in the Message Box.</param>
		public static async void Show(string title, string message)
		{
			await Show(title, message, null);
		}

		/// <summary>
		/// Creates a Modern Message Box that blocks application input until specified user input.
		/// </summary>
		/// <param name="title">The title you want to show in the Message Box.</param>
		/// <param name="message">The message you want to show in the Message Box.</param>
		/// <param name="buttons">A list of buttons you want to show in the Message Box. Defaults to just showing 'Okay'.</param>
		/// <returns>An enum representing which button the user pressed.</returns>
		public async static Task<ModernMessageBoxButton> Show(string title, string message, List<ModernMessageBoxButton> buttons)
		{
			var tcs = new TaskCompletionSource<ModernMessageBoxButton>();
			var dialog = new Controls.ModernMessageBox(new ModernMessageBoxViewModel(title, message, buttons));

			dialog.OnClose += (sender, args) =>
			{
				LeStorage.MainWindow.HideDialog();
				tcs.SetResult((ModernMessageBoxButton)sender);
			};
			LeStorage.MainWindow.ShowDialog(dialog);

			return await tcs.Task;
		}
	}
}
