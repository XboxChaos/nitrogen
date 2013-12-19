using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nitrogen.Wumbalo.Modern.AppControls
{
	/// <summary>
	/// Interaction logic for EditorRow.xaml
	/// </summary>
	[ContentProperty("EditorContent")]
	public partial class EditorRow : UserControl
	{
		public static DependencyProperty TitleProperty, DescriptionProperty, EditorContentProperty;

		static EditorRow ()
		{
			TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(EditorRow));
			DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(EditorRow));
			EditorContentProperty = DependencyProperty.Register("EditorContent", typeof(object), typeof(EditorRow));
		}

		public EditorRow ()
		{
			InitializeComponent();
		}

		public string Title
		{
			get { return (string) GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public string Description
		{
			get { return (string) GetValue(DescriptionProperty); }
			set { SetValue(DescriptionProperty, value); }
		}

		public object EditorContent
		{
			get { return (object) GetValue(EditorContentProperty); }
			set { SetValue(EditorContentProperty, value); }
		}
	}
}
