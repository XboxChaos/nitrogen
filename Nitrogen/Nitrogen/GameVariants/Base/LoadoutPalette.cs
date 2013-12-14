using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
	/// <summary>
	/// Represents a loadout palette.
	/// </summary>
	public sealed class LoadoutPalette
		: ISerializable<BitStream>
	{
		/// <summary>
		/// Specifies the number of loadouts in each palette.
		/// </summary>
		public const int LoadoutCount = 5;

		private Loadout[] _loadouts;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadoutPalette"/> class with default values.
		/// </summary>
		public LoadoutPalette ()
		{
			_loadouts = new Loadout[LoadoutCount];
			for ( int i = 0; i < LoadoutCount; i++ )
				_loadouts[i] = new Loadout();
		}

		/// <summary>
		/// Gets or sets the loadout at the specified <paramref name="index"/>.
		/// </summary>
		/// <param name="index">The index in this palette.</param>
		/// <returns>A <see cref="Loadout"/> object.</returns>
		public Loadout this[int index]
		{
			get
			{
				Contract.Requires<IndexOutOfRangeException>(index >= 0 && index < LoadoutCount);
				return _loadouts[index];
			}

			set
			{
				Contract.Requires<IndexOutOfRangeException>(index >= 0 && index < LoadoutCount);
				Contract.Requires<ArgumentNullException>(value != null);
				_loadouts[index] = value;
			}
		}

		/// <summary>
		/// Gets or sets the loadouts in this palette.
		/// </summary>
		public Loadout[] Loadouts
		{
			get { return _loadouts; }
			set
			{
				Contract.Requires(value.Length == LoadoutCount);

				for ( int i = 0; i < value.Length; i++ )
				{
					if ( value[i] == null )
						value[i] = new Loadout();
				}

				_loadouts = value;
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Serialize(_loadouts, 0, LoadoutCount);
		}

		#endregion
	}
}
