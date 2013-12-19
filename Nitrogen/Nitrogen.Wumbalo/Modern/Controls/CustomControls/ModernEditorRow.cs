using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Nitrogen.Wumbalo.Modern.Controls.CustomControls
{
	[ContentProperty("EditorContent")]
	public class ModernEditorRow : UserControl
	{
		public static DependencyProperty TitleProperty, DescriptionProperty, EditorContentProperty;

		static ModernEditorRow ()
		{
			TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ModernEditorRow));
			DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(ModernEditorRow));
			EditorContentProperty = DependencyProperty.Register("EditorContent", typeof(object), typeof(ModernEditorRow));
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
