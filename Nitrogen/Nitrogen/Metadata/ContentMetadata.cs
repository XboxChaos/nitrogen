using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Nitrogen.Metadata
{
	/// <summary>
	/// Represents the metadata of a Halo 4 content.
	/// </summary>
	public class ContentMetadata
		: ISerializable<BinaryStream>, ISerializable<BitStream>
	{
		public const int AllocatedStringLength = 256;
		private const int AllocatedMetadataLength = 688;

		private sbyte _contentType;
		private int _fileLength;
		private long[] _contentIds;
		private sbyte _activity;
		private sbyte _mode;
		private sbyte _engine;
		private int _map;
		private sbyte _category;
		private ContentAuthor _createdBy, _modifiedBy;
		private DateTime _dateCreated, _dateModified;
		private string _name, _description;

		private uint _screenshotNumber;
		private uint _filmDuration;
		private sbyte _variantIcon;
		private short _hopperId;

		private byte _campaignId;
		private byte _difficulty;
		private byte _scoringMode;
		private int _insertionPoint;
		private int _skulls;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentMetadata"/> class with default values.
		/// </summary>
		public ContentMetadata ()
		{
			_dateCreated = DateTime.Now;
			_dateModified = DateTime.Now;
			_contentIds = new long[4];
			_contentIds = new long[] { -1, -1, -1, 0 };
			_category = -1;
			_map = -1;
			_name = "";
			_description = "";
			_variantIcon = -1;
			_createdBy = new ContentAuthor();
			_modifiedBy = new ContentAuthor();
			_engine = -1;
		}

		#region Public Properties

		/// <summary>
		/// Gets or sets the name of this content.
		/// </summary>
		public string Name
		{
			get { return _name; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(Encoding.BigEndianUnicode.GetByteCount(value) <= AllocatedStringLength);

				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the description of this content.
		/// </summary>
		public string Description
		{
			get { return _description; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(Encoding.BigEndianUnicode.GetByteCount(value) <= AllocatedStringLength);

				_description = value;
			}
		}

		/// <summary>
		/// Gets or sets the target map of this content.
		/// </summary>
		public int Map
		{
			get { return _map; }
			set { _map = value; }
		}

		/// <summary>
		/// Gets or sets the initial creator of this content.
		/// </summary>
		public ContentAuthor CreatedBy
		{
			get { return _createdBy; }
			set { _createdBy = value; }
		}

		/// <summary>
		/// Gets or sets the person who last modified this content.
		/// </summary>
		public ContentAuthor ModifiedBy
		{
			get { return _modifiedBy; }
			set { _modifiedBy = value; }
		}

		/// <summary>
		/// Gets or sets the icon of this game variant.
		/// </summary>
		public sbyte VariantIcon
		{
			get { return _variantIcon; }
			set { _variantIcon = value; }
		}

		/// <summary>
		/// Gets or sets the category of this game variant.
		/// </summary>
		public sbyte Category
		{
			get { return _category; }
			set { _category = value; }
		}

		/// <summary>
		/// Gets or sets the creation timestamp of this content.
		/// </summary>
		public DateTime DateCreated { get { return _dateCreated; } }

		/// <summary>
		/// Gets or sets the modification timestamp of this content.
		/// </summary>
		public DateTime DateModified { get { return _dateModified; } }

		/// <summary>
		/// Gets the underlying game engine of this content.
		/// </summary>
		public GameEngine Engine
		{
			get { return (GameEngine) _engine; }
			set
			{
				Contract.Requires(value.IsDefined());
				_engine = (sbyte) value;
			}
		}

		/// <summary>
		/// Gets or sets the selected difficulty level for Campaign or Spartan Ops.
		/// </summary>
		public Difficulty Difficulty
		{
			get { return (Difficulty) _difficulty; }
			set { _difficulty = (byte) value; }
		}

		/// <summary>
		/// Gets or sets the active skulls for Campaign or Spartan Ops.
		/// </summary>
		public Skulls ActiveSkulls
		{
			get { return (Skulls) _skulls; }
			set { _skulls = (int) value; }
		}

		/// <summary>
		/// Gets or sets the metagame scoring mode for Campaign.
		/// </summary>
		public ScoringMode MetagameScoring
		{
			get { return (ScoringMode) _scoringMode; }
			set { _scoringMode = (byte) value; }
		}

		/// <summary>
		/// Gets or sets the insertion point in a level for Campaign.
		/// </summary>
		public int InsertionPoint
		{
			get { return _insertionPoint; }
			set { _insertionPoint = value; }
		}

		internal int FileLength
		{
			get { return _fileLength; }
			set { _fileLength = value; }
		}

		#endregion

		#region Internal Properties

		/// <summary>
		/// Gets the type of this content.
		/// </summary>
		internal ContentType ContentType
		{
			get { return (ContentType) _contentType; }
			set
			{
				Contract.Requires(value.IsDefined());
				_contentType = (sbyte) value;
			}
		}

		/// <summary>
		/// Gets the game activity of this content.
		/// </summary>
		internal GameActivity Activity
		{
			get { return (GameActivity) _activity; }
			set
			{
				Contract.Requires(value.IsDefined());
				_activity = (sbyte) value;
			}
		}

		/// <summary>
		/// Gets the underlying game mode of this content.
		/// </summary>
		internal GameMode Mode
		{
			get { return (GameMode) _mode; }
			set
			{
				Contract.Requires(value.IsDefined());
				_mode = (sbyte) value;
			}
		}

		#endregion

		#region ISerializable<BinaryStream> Members

		void ISerializable<BinaryStream>.SerializeObject (BinaryStream s)
		{
			Action<BinaryStream> streamData = (BinaryStream d) =>
			{
				d.Stream(ref _contentType);
				d.PadBytes(3);
				d.Stream(ref _fileLength);
				for ( int i = 0; i < _contentIds.Length; i++ ) { d.Stream(ref _contentIds[i]); }
				d.Stream(ref _activity);
				d.Stream(ref _mode);
				d.Stream(ref _engine);
				d.PadBytes(1);
				d.Stream(ref _map);
				d.Stream(ref _category);
				d.PadBytes(7);
				d.Stream(ref _dateCreated);
				d.Serialize(_createdBy);
				d.Stream(ref _dateModified);
				d.Serialize(_modifiedBy);
				d.StreamString(ref _name, Encoding.BigEndianUnicode, length: AllocatedStringLength);
				d.StreamString(ref _description, Encoding.BigEndianUnicode, length: AllocatedStringLength);

				switch ( (ContentType) _contentType )
				{
					case ContentType.Screenshot:
						d.Stream(ref _screenshotNumber);
						break;

					case ContentType.Film:
					case ContentType.FilmClip:
						d.Stream(ref _filmDuration);
						break;

					case ContentType.GameVariant:
						d.Stream(ref _variantIcon);
						d.PadBytes(3);
						break;
				}

				if ( (GameActivity) _activity == GameActivity.Matchmaking )
					d.Stream(ref _hopperId);

				GameEngine engine = (GameEngine) _engine;
				if ( engine == GameEngine.Campaign )
				{
					d.Stream(ref _campaignId);
					d.Stream(ref _difficulty);
					d.Stream(ref _scoringMode);
					d.PadBytes(1);
					d.Stream(ref _insertionPoint);
					d.Stream(ref _skulls);
				}
				else if ( engine == GameEngine.SpartanOps )
				{
					d.Stream(ref _difficulty);
					d.PadBytes(3);
					d.Stream(ref _skulls);
				}
			};

			ByteOrder endianness = s.State == StreamState.Read ? s.Reader.Endianness : s.Writer.Endianness;
			using ( var ms = new MemoryStream(new byte[AllocatedMetadataLength]) )
			using ( var bufferStream = new BinaryStream(ms, s.State, endianness, leaveOpen: true) )
			{
				streamData(bufferStream);

				if ( s.State == StreamState.Write )
					s.BaseStream.Write(ms.ToArray(), 0, (int) ms.Length);
			}
		}

		#endregion

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _contentType, 4);
			s.Stream(ref _fileLength);
			for ( int i = 0; i < _contentIds.Length; i++ ) { s.Stream(ref _contentIds[i]); }
			s.Stream(ref _activity, 2);
			s.Stream(ref _mode, 3);
			s.Stream(ref _engine, 3);
			s.Stream(ref _map);
			s.Stream(ref _category);
			s.Stream(ref _dateCreated);
			s.Serialize(_createdBy);
			s.Stream(ref _dateModified);
			s.Serialize(_modifiedBy);
			s.StreamNullTerminatedString(ref _name, Encoding.BigEndianUnicode);
			s.StreamNullTerminatedString(ref _description, Encoding.BigEndianUnicode);

			switch ( (ContentType) _contentType )
			{
				case ContentType.Screenshot:
					s.Stream(ref _screenshotNumber);
					break;

				case ContentType.Film:
					s.Stream(ref _filmDuration);
					break;

				case ContentType.GameVariant:
					s.Stream(ref _variantIcon);
					break;
			}

			if ( (GameActivity) _activity == GameActivity.Matchmaking )
				s.Stream(ref _hopperId);

			GameEngine engine = (GameEngine) _engine;
			if (engine == GameEngine.Campaign)
			{
				s.Stream(ref _campaignId);
				s.Stream(ref _difficulty, 2);
				s.Stream(ref _scoringMode, 2);
				s.Stream(ref _insertionPoint, 8);
				s.Stream(ref _skulls);
			}
			else if (engine == GameEngine.SpartanOps)
			{
				s.Stream(ref _difficulty, 2);
				s.Stream(ref _skulls);
			}
		}

		#endregion
	}
}