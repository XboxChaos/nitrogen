using System;
using System.Windows;
using Nitrogen.Wumbalo.Modern.Controls.CustomControls;
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

		private void ActionButton_OnClick(object sender, RoutedEventArgs e)
		{
			var button = sender as ModernButton;
			if (button == null) return;

			var buttonResult = button.Tag as ModernMessageBoxButton?;
			if (buttonResult == null) return;

			OnClose(buttonResult, null);
		}
	}
}
