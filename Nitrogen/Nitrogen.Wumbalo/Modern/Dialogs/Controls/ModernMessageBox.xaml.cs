using System;
using System.Windows;
using Nitrogen.Wumbalo.ViewModels.Dialogs;

namespace Nitrogen.Wumbalo.Modern.Dialogs.Controls
{
	/// <summary>
	/// Interaction logic for ModernMessageBox.xaml
	/// </summary>
	public partial class ModernMessageBox
	{
		private readonly ModernMessageBoxViewModel _viewModel;

		public ModernMessageBox(ModernMessageBoxViewModel viewModel)
		{
			InitializeComponent();

			_viewModel = viewModel;
			DataContext = _viewModel;
		}

		public event EventHandler OnClose;

		private void OkayButton_OnClick(object sender, RoutedEventArgs e)
		{
			OnClose(null, null);
		}
	}
}
