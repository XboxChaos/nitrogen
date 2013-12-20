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
	/// Interaction logic for TraitsEditor.xaml
	/// </summary>
	public partial class TraitsEditor : UserControl
	{
		public static DependencyProperty IsRespawnTraitsProperty, UseRuntimeTemplateProperty;

		static TraitsEditor ()
		{
			IsRespawnTraitsProperty = DependencyProperty.Register("IsRespawnTraits", typeof(bool), typeof(TraitsEditor));
			UseRuntimeTemplateProperty = DependencyProperty.Register("UseRuntimeTemplate", typeof(bool), typeof(TraitsEditor));
		}

		public bool IsRespawnTraits
		{
			get { return (bool) GetValue(IsRespawnTraitsProperty); }
			set { SetValue(IsRespawnTraitsProperty, value); }
		}

		public bool UseRuntimeTemplate
		{
			get { return (bool) GetValue(UseRuntimeTemplateProperty); }
			set { SetValue(UseRuntimeTemplateProperty, value); }
		}

		public TraitsEditor ()
		{
			InitializeComponent();
		}
	}
}
