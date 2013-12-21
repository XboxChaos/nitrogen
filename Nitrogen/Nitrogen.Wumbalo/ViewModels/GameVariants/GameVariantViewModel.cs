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
			Social = new SocialSettingsViewModel(_variant);
			MapOverrides = new MapOverridesViewModel(_variant);
			Loadouts = new LoadoutSettingsViewModel(_variant);
			Ordnance = new OrdnanceSettingsViewModel(_variant);
			Teams = new TeamsViewModel(_variant);
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
		/// Gets or sets the social settings view model.
		/// </summary>
		public SocialSettingsViewModel Social
		{
			get { return Get(); }
			set { Set(value); }
		}

		/// <summary>
		/// Gets or sets the map overrides view model.
		/// </summary>
		public MapOverridesViewModel MapOverrides
		{
			get { return Get(); }
			set { Set(value); }
		}

		/// <summary>
		/// Gets or sets the loadout settings view model.
		/// </summary>
		public LoadoutSettingsViewModel Loadouts
		{
			get { return Get(); }
			set { Set(value); }
		}

		/// <summary>
		/// Gets or sets the ordnance settings view model.
		/// </summary>
		public OrdnanceSettingsViewModel Ordnance
		{
			get { return Get(); }
			set { Set(value); }
		}

		/// <summary>
		/// Gets or sets the team settings view model.
		/// </summary>
		public TeamsViewModel Teams
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