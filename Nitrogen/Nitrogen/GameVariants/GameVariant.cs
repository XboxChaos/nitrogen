using Nitrogen.GameVariants.Base;
using Nitrogen.Metadata;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants
{
	/// <summary>
	/// Represents a base game variant without engine-specific data.
	/// </summary>
	public class GameVariant
		: ISerializable<BitStream>
	{
		private ContentMetadata _metadata;
		private bool _unknownFlag1, _unknownFlag2;
		private GeneralSettings _general;
		private PrototypeSettings _prototype;
		private RespawnSettings _respawn;
		private SocialSettings _social;
		private MapOverrides _mapOverrides;
		private RequisitionSettings _requisition;
		private TeamSettings _teams;
		private LoadoutSettings _loadouts;
		private OrdnanceSettings _ordnance;
		private ISerializable<BitStream> _engineData;

		/// <summary>
		/// Initializes a new instance of the <see cref="GameVariant"/> class with default values.
		/// </summary>
		public GameVariant ()
		{
			_metadata = new ContentMetadata { ContentType = ContentType.GameVariant };
			_general = new GeneralSettings();
			_prototype = new PrototypeSettings();
			_respawn = new RespawnSettings();
			_social = new SocialSettings();
			_mapOverrides = new MapOverrides();
			_requisition = new RequisitionSettings();
			_teams = new TeamSettings();
			_loadouts = new LoadoutSettings();
			_ordnance = new OrdnanceSettings();
		}

		/// <summary>
		/// Gets or sets the metadata of this game variant.
		/// </summary>
		public ContentMetadata Metadata
		{
			get { return _metadata; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_metadata = value;
			}
		}

		/// <summary>
		/// Gets or sets the general settings for this game variant.
		/// </summary>
		public GeneralSettings GeneralSettings
		{
			get { return _general; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_general = value;
			}
		}

		/// <summary>
		/// Gets or sets whether this game variant contains weapon tuning data.
		/// </summary>
		public bool HasWeaponTuning
		{
			get { return _prototype.Mode == 1; }
			set { _prototype.Mode = (byte) ( value ? 1 : 0 ); }
		}

		/// <summary>
		/// Gets or sets the respawn settings for this game variant.
		/// </summary>
		public RespawnSettings RespawnSettings
		{
			get { return _respawn; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_respawn = value;
			}
		}

		/// <summary>
		/// Gets or sets the map overrides settings for this game variant.
		/// </summary>
		public MapOverrides MapOverrides
		{
			get { return _mapOverrides; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_mapOverrides = value;
			}
		}

		/// <summary>
		/// Gets or sets the team settings for this game variant.
		/// </summary>
		public TeamSettings TeamSettings
		{
			get { return _teams; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_teams = value;
			}
		}

		/// <summary>
		/// Gets or sets the loadout settings for this game variant.
		/// </summary>
		public LoadoutSettings LoadoutSettings
		{
			get { return _loadouts; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_loadouts = value;
			}
		}

		/// <summary>
		/// Gets or sets the ordnance settings for this game variant.
		/// </summary>
		public OrdnanceSettings OrdnanceSettings
		{
			get { return _ordnance; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_ordnance = value;
			}
		}

		protected ISerializable<BitStream> EngineData
		{
			get { return _engineData; }
			set { _engineData = value; }
		}

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Serialize(_metadata);
			s.Stream(ref _unknownFlag1);
			s.Stream(ref _unknownFlag2);
			s.Serialize(_general);
			s.Serialize(_prototype);
			s.Serialize(_respawn);
			s.Serialize(_social);
			s.Serialize(_mapOverrides);
			s.Serialize(_requisition);
			s.Serialize(_teams);
			s.Serialize(_loadouts);
			s.Serialize(_ordnance);

			if ( _engineData != null )
				_engineData.SerializeObject(s);
		}
	}
}
