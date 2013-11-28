using Nitrogen.Wumbalo.ViewModels;

namespace Nitrogen.Wumbalo.Models.Home
{
	public class Recent : Inpc
	{
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { SetField(ref _name, value, "Name"); }
		}
		private string _name;
	}
}
