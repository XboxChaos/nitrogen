using Nitrogen.Wumbalo.ViewModels;

namespace Nitrogen.Wumbalo.Models.Home
{
	public enum HeaderType
	{
		Recents,
		Home,
		Onyx,
		Devices,
		Development
	}

	public class Header : Inpc
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

		/// <summary>
		/// 
		/// </summary>
		public string Tag
		{
			get { return _tag; }
			set { SetField(ref _tag, value, "Tag"); }
		}
		private string _tag;

		/// <summary>
		/// 
		/// </summary>
		public bool IsSelected
		{
			get { return _isSelected; }
			set { SetField(ref _isSelected, value, "IsSelected"); }
		}
		private bool _isSelected;

		/// <summary>
		/// 
		/// </summary>
		public HeaderType HeaderType
		{
			get { return _headerType; }
			set { SetField(ref _headerType, value, "HeaderType"); }
		}
		private HeaderType _headerType;
	}
}
