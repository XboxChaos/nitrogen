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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nitrogen.Wumbalo.Views.Apps.GametypeEditor
{
	/// <summary>
	/// Interaction logic for OrdnanceSettingsEditor.xaml
	/// </summary>
	public partial class OrdnanceSettingsEditor : UserControl
	{
		public OrdnanceSettingsEditor ()
		{
			InitializeComponent();
		}

		private void TextBox_TextChanged (object sender, TextChangedEventArgs e)
		{
			var textbox = sender as TextBox;
			textbox.SelectionLength = 0;
			int selection = textbox.SelectionStart;
			textbox.Text = textbox.Text.Replace(' ', '_');
			textbox.SelectionStart = selection;
		}
	}
}
