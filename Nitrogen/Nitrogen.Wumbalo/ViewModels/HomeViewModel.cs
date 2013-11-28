using System.Collections.Generic;
using Nitrogen.Wumbalo.Models.Home;

namespace Nitrogen.Wumbalo.ViewModels
{
	public class HomeViewModel : Inpc
	{
		/// <summary>
		/// 
		/// </summary>
		public HomeViewModel()
		{
			Headers.Add(new Header { IsSelected = false, Name = "Recents", Tag = "Recents" });
			Headers.Add(new Header { IsSelected = true, Name = "Home", Tag = "Home" });
			Headers.Add(new Header { IsSelected = false, Name = "Stuff", Tag = "Stuff" });
		}

		/// <summary>
		/// 
		/// </summary>
		public IList<Header> Headers
		{
			get { return _headers; }
			set { SetField(ref _headers, value, "Headers"); }
		}
		private IList<Header> _headers = new List<Header>();
	}
}
