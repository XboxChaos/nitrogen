using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using Nitrogen.Wumbalo.Helpers.Native;
using Nitrogen.Wumbalo.ViewModels;

namespace Nitrogen.Wumbalo
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly MainViewModel _viewModel = new MainViewModel();

		public MainWindow()
		{
			InitializeComponent();

			// Dropshadow
			DwmDropShadow.DropShadowToWindow(this);

			DataContext = _viewModel;

			// Setup Events
			StateChanged += Window_StateChanged;
			Window_StateChanged(this, null);
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			var handle = (new WindowInteropHelper(this)).Handle;
			var hwndSource = HwndSource.FromHwnd(handle);
			if (hwndSource != null)
				hwndSource.AddHook(WindowProc);
		}

		#region Recreating Normal Window Actions

		// Resizing
		private void ResizeDrop_DragDelta(object sender, DragDeltaEventArgs e)
		{
			var yadjust = Height + e.VerticalChange;
			var xadjust = Width + e.HorizontalChange;

			if (xadjust > MinWidth)
				Width = xadjust;
			if (yadjust > MinHeight)
				Height = yadjust;
		}
		private void ResizeRight_DragDelta(object sender, DragDeltaEventArgs e)
		{
			var xadjust = Width + e.HorizontalChange;

			if (xadjust > MinWidth)
				Width = xadjust;
		}
		private void ResizeBottom_DragDelta(object sender, DragDeltaEventArgs e)
		{
			var yadjust = Height + e.VerticalChange;

			if (yadjust > MinHeight)
				Height = yadjust;
		}
		private void Window_StateChanged(object sender, EventArgs e)
		{
			switch (WindowState)
			{
				case WindowState.Normal:
					FrameBorder.BorderThickness = new Thickness(1, 1, 1, 23);
					RestoreApplicationButton.Visibility = Visibility.Collapsed;
					MaxamizeApplicationButton.Visibility =
						ResizeDropVector.Visibility =
							ResizeDrop.Visibility = ResizeRight.Visibility = ResizeBottom.Visibility = Visibility.Visible;
					break;
				case WindowState.Maximized:
					FrameBorder.BorderThickness = new Thickness(0, 0, 0, 23);
					RestoreApplicationButton.Visibility = Visibility.Visible;
					MaxamizeApplicationButton.Visibility =
						ResizeDropVector.Visibility =
							ResizeDrop.Visibility = ResizeRight.Visibility = ResizeBottom.Visibility = Visibility.Collapsed;
					break;
			}
			/*
			 * ResizeDropVector
			 * ResizeDrop
			 * ResizeRight
			 * ResizeBottom
			 */
		}

		// Window Management
		private void MinimizeApplicationButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}
		private void RestoreApplicationButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Normal;
		}
		private void MaxamizeApplicationButton_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Maximized;
		}
		private void CloseApplicationButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		#region Maximize Workspace Workarounds

		// Multi-Monitor Support
		private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			switch (msg)
			{
				case 0x0024:
					WmGetMinMaxInfo(hwnd, lParam);
					handled = true;
					break;
			}
			return IntPtr.Zero;
		}
		private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
		{
			var mmi = (MonitorWorkarea.MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MonitorWorkarea.MINMAXINFO));

			// Adjust the maximized size and position to fit the work area of the correct monitor
			const int monitorDefaulttonearest = 0x00000002;
			var monitor = MonitorWorkarea.MonitorFromWindow(hwnd, monitorDefaulttonearest);

			if (monitor != IntPtr.Zero)
			{
				var monitorInfo = new MonitorWorkarea.MONITORINFO();
				MonitorWorkarea.GetMonitorInfo(monitor, monitorInfo);
				var rcWorkArea = monitorInfo.rcWork;
				var rcMonitorArea = monitorInfo.rcMonitor;
				mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
				mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
				mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
				mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
			}

			Marshal.StructureToPtr(mmi, lParam, true);
		}

		#endregion
		
		#endregion
	}
}