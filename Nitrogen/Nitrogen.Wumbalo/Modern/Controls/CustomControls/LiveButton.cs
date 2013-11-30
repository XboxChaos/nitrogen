using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nitrogen.Wumbalo.Modern.Controls.CustomControls
{
	/// <summary>
	/// 
	/// </summary>
	public class LiveButton : Button
	{
		public static DependencyProperty BackgroundImageProperty;
		public static DependencyProperty DescriptionProperty;
		public static DependencyProperty TitleProperty;

		/// <summary>
		/// 
		/// </summary>
		static LiveButton()
		{
			BackgroundImageProperty = DependencyProperty.Register("BackgroundImage", typeof(ImageSource), typeof(LiveButton));
			DescriptionProperty = DependencyProperty.Register("Description", typeof(String), typeof(LiveButton));
			TitleProperty = DependencyProperty.Register("Title", typeof(String), typeof(LiveButton));
		}

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public ImageSource BackgroundImage
		{
			get { return (ImageSource)GetValue(BackgroundImageProperty); }
			set { SetValue(BackgroundImageProperty, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public String Description
		{
			get { return (String)GetValue(DescriptionProperty); }
			set { SetValue(DescriptionProperty, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public String Title
		{
			get { return (String)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		#endregion
	}
}
