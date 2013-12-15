using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
	public sealed class LoadoutSettings
		: ISerializable<BitStream>
	{
		/// <summary>
		/// Indicates the number of loadout palettes available.
		/// </summary>
		public const int PaletteCount = 6;

		private bool
			_personalLoadoutsEnabled,
			_mapLoadoutsEnabled,

			// One of these has something to do with enforcing the number of loadouts available
			// based on the player's SR but it appears to work only once? - Matt
			_unknownFlag1,
			_unknownFlag2;

		private LoadoutPalette[] _palettes;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadoutSettings"/> class with default values.
		/// </summary>
		public LoadoutSettings ()
		{
			_palettes = new LoadoutPalette[PaletteCount];
			for ( int i = 0; i < PaletteCount; i++ )
				_palettes[i] = new LoadoutPalette();
		}

		/// <summary>
		/// Gets or sets whether personal loadouts are enabled.
		/// </summary>
		public bool PersonalLoadoutsEnabled
		{
			get { return _personalLoadoutsEnabled; }
			set { _personalLoadoutsEnabled = value; }
		}

		/// <summary>
		/// Gets or sets whether map loadouts are enabled.
		/// </summary>
		public bool MapLoadoutsEnabled
		{
			get { return _mapLoadoutsEnabled; }
			set { _mapLoadoutsEnabled = value; }
		}

		/// <summary>
		/// Gets or sets the loadout palettes.
		/// </summary>
		public LoadoutPalette[] Palettes
		{
			get { return _palettes; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires(value.Length == PaletteCount);

				for ( int i = 0; i < value.Length; i++ )
				{
					if ( value[i] == null )
						value[i] = new LoadoutPalette();
				}

				_palettes = value;
			}
		}

		/// <summary>
		/// Gets or sets the game loadouts palette.
		/// </summary>
		public LoadoutPalette GameLoadouts
		{
			get { return _palettes[0]; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_palettes[0] = value;
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _personalLoadoutsEnabled);
			s.Stream(ref _unknownFlag1);
			s.Stream(ref _unknownFlag2);
			s.Stream(ref _mapLoadoutsEnabled);
			s.SerializeObjects(_palettes, 0, PaletteCount);
		}

		#endregion
	}
}
