using Nitrogen.Metadata;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class MetadataViewModel
		: Inpc
	{
		private ContentMetadata _metadata;

		public MetadataViewModel () : this(new ContentMetadata()) { }

		public MetadataViewModel (ContentMetadata metadata)
		{
			_metadata = metadata;
		}

		public string Name
		{
			get { return _metadata.Name; }
			set
			{
				_metadata.Name = value;
				OnPropertyChanged();
			}
		}

		public string Description
		{
			get { return _metadata.Description; }
			set
			{
				_metadata.Description = value;
				OnPropertyChanged();
			}
		}
	}
}
