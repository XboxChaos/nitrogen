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
		public GameVariantViewModel ()
			: this(new GameVariant())
		{
			IsOriginal = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GameVariantViewModel"/> class based on the
		/// specified <paramref name="variant"/>.
		/// </summary>
		/// <param name="variant">The gametype data.</param>
		public GameVariantViewModel (GameVariant variant)
		{
			_variant = variant;
			Metadata = new MetadataViewModel(_variant);
			General = new GeneralSettingsViewModel(_variant);
			Respawn = new RespawnSettingsViewModel(_variant);
		}

		/// <summary>
		/// Gets or sets the metadata view model.
		/// </summary>
		public MetadataViewModel Metadata
		{
			get { return Get(); }
			set { Set(value); }
		}

		/// <summary>
		/// Gets or sets the general settings view model.
		/// </summary>
		public GeneralSettingsViewModel General
		{
			get { return Get(); }
			set { Set(value); }
		}

		/// <summary>
		/// Gets or sets the respawn settings view model.
		/// </summary>
		public RespawnSettingsViewModel Respawn
		{
			get { return Get(); }
			set { Set(value); }
		}

		/// <summary>
		/// Gets a value indicating whether the variant is original.
		/// </summary>
		public bool IsOriginal
		{
			get { return Get(); }
			set { Set(value); }
		}
	}
}