using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
	/// <summary>
	/// Represents a single personal ordnance slot.
	/// </summary>
	public sealed class OrdnanceSlot
		: ISerializable<BitStream>
	{
		/// <summary>
		/// Indicates the number of ordnance items in a slot.
		/// </summary>
		public const int Capacity = 8;

		private OrdnanceItem[] _items;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrdnanceSlot"/> class with default values.
		/// </summary>
		public OrdnanceSlot ()
		{
			_items = new OrdnanceItem[Capacity];
			for ( int i = 0; i < Capacity; i++ )
				_items[i] = new OrdnanceItem();
		}

		/// <summary>
		/// Gets or sets the possible items in this slot.
		/// </summary>
		public OrdnanceItem[] Items
		{
			get { return _items; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(value.Length != Capacity);

				_items = value;
				for ( int i = 0; i < _items.Length; i++ )
				{
					if ( _items[i] == null )
						_items[i] = new OrdnanceItem();
				}
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.SerializeObjects(_items, 0, Capacity);
		}

		#endregion
	}
}
