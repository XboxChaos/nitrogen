using System;
using System.Windows;
using System.Windows.Controls;

namespace Nitrogen.Wumbalo.Modern.Controls.CustomControls
{
	/// <summary>
	/// 
	/// </summary>
	public class ModernButton : Button
	{
		public static DependencyProperty ButtonNameProperty;

		/// <summary>
		/// 
		/// </summary>
		static ModernButton()
		{
			ButtonNameProperty = DependencyProperty.Register("ButtonName", typeof(String), typeof(ModernButton));
		}

		/// <summary>
		/// 
		/// </summary>
		public String ButtonName
		{
			get { return (String)GetValue(ButtonNameProperty); }
			set { SetValue(ButtonNameProperty, value); }
		}
	}
}
