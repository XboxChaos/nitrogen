using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nitrogen.Wumbalo.Modern.Controls.CustomControls
{
	/// <summary>
	/// 
	/// </summary>
	public class ModernButton : Button
	{
		public static DependencyProperty ButtonNameProperty;
		public static DependencyProperty ButtonHoverBackgroundProperty;

		/// <summary>
		/// 
		/// </summary>
		static ModernButton()
		{
			ButtonNameProperty = DependencyProperty.Register("ButtonName", typeof(String), typeof(ModernButton));
			ButtonHoverBackgroundProperty = DependencyProperty.Register("ButtonHoverBackground", typeof(SolidColorBrush), typeof(ModernButton));
		}

		/// <summary>
		/// 
		/// </summary>
		public String ButtonName
		{
			get { return (String)GetValue(ButtonNameProperty); }
			set { SetValue(ButtonNameProperty, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public SolidColorBrush ButtonHoverBackground
		{
			get { return (SolidColorBrush)GetValue(ButtonHoverBackgroundProperty); }
			set { SetValue(ButtonHoverBackgroundProperty, value); }
		}
	}
}
