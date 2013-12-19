using Nitrogen.GameVariants;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class GameVariantViewModel
		: Inpc
	{
		private GameVariant _variant;

		/// <summary>
		/// Initializes a new instance of the <see cref="GameVariantViewModel"/> class.
		/// </summary>
		public GameVariantViewModel () : this(new GameVariant()) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="GameVariantViewModel"/> class based on the
		/// specified <paramref name="variant"/>.
		/// </summary>
		/// <param name="variant">The gametype data.</param>
		public GameVariantViewModel (GameVariant variant)
		{
			_variant = variant;
			//_variant = ContentFactory.ReadGameVariant(System.IO.File.OpenRead("C:/users/matt/desktop/h4_rumble_tu.game"));
			Metadata = new MetadataViewModel(_variant.Metadata);
		}

		/// <summary>
		/// Gets or sets the metadata view model.
		/// </summary>
		public MetadataViewModel Metadata
		{
			get { return Get(); }
			set { Set(value); }
		}
	}
}