using Nitrogen.IO;
using Nitrogen.Shared;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	public sealed class UserDefinedTraits
		: ISerializable<BitStream>
	{
		private PlayerTraits _traits;
		private sbyte _nameStringIndex, _descStringIndex;
		private bool _hidden, _runtime;

		public UserDefinedTraits()
		{
			_traits = new PlayerTraits();
		}

		/// <summary>
		/// Gets or sets the index of the name string in the user-defined string table.
		/// </summary>
		public sbyte NameStringIndex
		{
			get { return _nameStringIndex; }
			set { _nameStringIndex = value; }
		}

		/// <summary>
		/// Gets or sets the index of the description string in the user-defined string table.
		/// </summary>
		public sbyte DescriptionStringIndex
		{
			get { return _descStringIndex; }
			set { _descStringIndex = value; }
		}

		/// <summary>
		/// Gets or sets whether this trait set is visible in-game.
		/// </summary>
		public bool IsHidden
		{
			get { return _hidden; }
			set { _hidden = value; }
		}

		/// <summary>
		/// Gets or sets whether the initial equipment traits are hidden in-game.
		/// </summary>
		public bool UseRuntimeTemplate
		{
			get { return _runtime; }
			set { _runtime = value; }
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _nameStringIndex);
			s.Stream(ref _descStringIndex);
			s.SerializeObject(_traits);
			s.Stream(ref _hidden);
			s.Stream(ref _runtime);
		}

		#endregion
	}
}
