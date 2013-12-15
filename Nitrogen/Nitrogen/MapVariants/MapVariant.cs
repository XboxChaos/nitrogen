using Nitrogen.IO;
using Nitrogen.Metadata;
using Nitrogen.Shared;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.MapVariants
{
	public sealed class MapVariant
		: ISerializable<BitStream>
	{
		private const int MaxObjectTypes = 256;

		private ContentMetadata _metadata;
		private byte _encodingVersion;
		private int _unknownInt1, _unknownInt2, _unknownInt3, _budget, _map;
		private short _objectTypeCount;
		private bool _unknownFlag1, _unknownFlag2;
		private float[] _boundaries;
		private StringTable _stringTable;
		private ObjectTypeCount[] _objectTypeCountTable;

		public MapVariant ()
		{
			_metadata = new ContentMetadata { ContentType = ContentType.MapVariant };
			_boundaries = new float[6];
			_stringTable = new StringTable();
			_objectTypeCountTable = new ObjectTypeCount[MaxObjectTypes];
			for ( int i = 0; i < MaxObjectTypes; i++ )
				_objectTypeCountTable[i] = new ObjectTypeCount();

			/*this.Objects = new TMapObjectList();*/
		}

		/// <summary>
		/// Gets or sets the metadata of this map variant.
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

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.SerializeObject(_metadata);
			s.Stream(ref _encodingVersion);
			s.Stream(ref _unknownInt1);
			s.Stream(ref _unknownInt2);
			s.Stream(ref _objectTypeCount, bits: 9);
			s.Stream(ref _map);
			s.Stream(ref _unknownFlag1);
			s.Stream(ref _unknownFlag2);
			for ( int i = 0; i < _boundaries.Length; i++ )
				s.Stream(ref _boundaries[i]);
			s.Stream(ref _budget);
			s.Stream(ref _unknownInt3);
			_stringTable.Serialize(s, offsetBitLength: 12, lengthBitLength: 13, countBitLength: 9);

			/*
           
            // Object Table
            this.Objects.SerializeObjects(s);

            // Object Type Count Table
            for (int i = 0; i < this.objectTypeCountTable.Length; i++)
            {
                if (i < this.objectTypeCount)
                {
                    s.SerializeObjects(this.objectTypeCountTable[i]);
                }
            }*/
		}

		#endregion
	}
}
