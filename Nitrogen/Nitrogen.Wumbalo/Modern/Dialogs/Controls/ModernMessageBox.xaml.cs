using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
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

			// Events
			KeyUp += OnKeyUp;
			IsVisibleChanged += LoginControl_IsVisibleChanged;

			// Setup Buttons - I know this is really hacky and horrible, but it's the only way
			// to do it and be able to give the first button focus, without some stupid xaml
			// workaround that is 100 times worse than shit method. Tell me about it.
			foreach (var button in _viewModel.Buttons)
				switch (button)
				{
					case ModernMessageBoxButton.Okay:
						OkayButton.Visibility = Visibility.Visible;
						break;
					case ModernMessageBoxButton.Yes:
						YesButton.Visibility = Visibility.Visible;
						break;
					case ModernMessageBoxButton.No:
						NoButton.Visibility = Visibility.Visible;
						break;
					case ModernMessageBoxButton.Cancel:
						CancelButton.Visibility = Visibility.Visible;
						break;
					case ModernMessageBoxButton.Aite:
						AiteButton.Visibility = Visibility.Visible;
						break;
				}
		}

		#region Events

		void LoginControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!(bool) e.NewValue) return;

			Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(() => ButtonPanel.Children[0].Focus()));
		}  

		private void OnKeyUp(object sender, KeyEventArgs keyEventArgs)
		{
			if (keyEventArgs.Key == Key.Escape)
				OnClose(ModernMessageBoxButton.Cancel, null);
		}

		private void ActionButton_OnClick(object sender, RoutedEventArgs e)
		{
			var button = sender as ModernButton;
			if (button == null) return;

			var tag = button.Tag as string;
			if (tag == null) return;

			var enumResult = (ModernMessageBoxButton)Enum.Parse(typeof(ModernMessageBoxButton), tag);
			if (Enum.IsDefined(typeof (ModernMessageBoxButton), enumResult))
				OnClose(enumResult, null);
		}

		#endregion

		public event EventHandler OnClose;
	}
}
