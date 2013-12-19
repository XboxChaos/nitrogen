using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;
using System.Text;
using System.Threading;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class MetadataViewModel
		: Inpc
	{
		private Timer _dateModifiedTimer;

		private ContentMetadata _metadata;
		private GameVariant _variant;

		public MetadataViewModel (GameVariant variant)
		{
			_variant = variant;
			_metadata = _variant.Metadata;

			_dateModifiedTimer = new Timer((object state) =>
			{
				( state as MetadataViewModel ).DateModified = DateTime.Now;
			}, this, 0, 1000);
		}

		public string Name
		{
			get { return _metadata.Name; }
			set
			{
				_metadata.Name = value;
				if ( _variant.EngineData as MegaloData != null )
				{
					var megalo = _variant.EngineData as MegaloData;
					megalo.Name.Set(value);
				}

				OnPropertyChanged();
			}
		}

		public string Description
		{
			get { return _metadata.Description; }
			set
			{
				_metadata.Description = value;
				if ( _variant.EngineData as MegaloData != null )
				{
					var megalo = _variant.EngineData as MegaloData;
					megalo.Description.Set(value);
				}

				OnPropertyChanged();
			}
		}

		public string Creator
		{
			get { return _metadata.CreatedBy.Gamertag; }
			set
			{
				_metadata.CreatedBy.Gamertag = value;
				OnPropertyChanged();
			}
		}

		public string Modifier
		{
			get { return _metadata.ModifiedBy.Gamertag; }
			set
			{
				_metadata.ModifiedBy.Gamertag = value;
				OnPropertyChanged();
			}
		}

		public DateTime DateCreated { get { return _metadata.DateCreated; } }
		public DateTime DateModified
		{
			get { return _metadata.DateModified; }
			private set
			{
				_metadata.DateModified = value;
				OnPropertyChanged();
			}
		}

		public bool IsCampaign
		{
			get { return _metadata.Engine == GameEngine.Campaign; }
		}

		public bool IsMegalo
		{
			get { return _metadata.Engine == GameEngine.Forge || _metadata.Engine == GameEngine.PVP; }
		}

		public VariantIcon Icon
		{
			get { return _metadata.VariantIcon; }
			set
			{
				_metadata.VariantIcon = value;
				OnPropertyChanged();
			}
		}

		public GameEngine Engine
		{
			get { return _metadata.Engine; }
		}

		public string Category
		{
			get
			{
				if ( !IsMegalo )
					return "";
				else
					return ( _variant.EngineData as MegaloData ).CategoryName.Get(Language.English);
			}
		}

		public string CategoryLocalized
		{
			get
			{
				if ( !IsMegalo )
					return "";

				var category = ( _variant.EngineData as MegaloData ).CategoryName;
				var builder = new StringBuilder();
				foreach ( Language language in Enum.GetValues(typeof(Language)) )
				{
					builder.AppendLine(language + "\t\t" + category.Get(language));
				}
				return builder.ToString();
			}
		}

	}
}
