using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Metadata
{
	/// <summary>
	/// Represents an author of a user-generated content.
	/// </summary>
	public sealed class ContentAuthor
		: ISerializable<BinaryStream>, ISerializable<BitStream>
	{
		public const string DefaultAuthor = "Nitrogen";

		/*
		 * According to kornman, the XUID of the player is different based on the player's online
		 * state. The "signed into Xbox LIVE" field exists for the game to differentiate between
		 * a player's online and offline XUIDs.
		 */

		private long _xuid;
		private string _gamertag;
		private bool _signedIntoXboxLive;

		/// <summary>
		/// Creates a new instance of the <see cref="Author"/> class with the specified Gamertag.
		/// </summary>
		/// <remarks>The XUID will be set to 0 and cannot be modified.</remarks>
		/// <param name="gamertag">This author's Gamertag.</param>
		public ContentAuthor (string gamertag)
		{
			_gamertag = gamertag;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ContentAuthor"/> class with the default Gamertag.
		/// </summary>
		public ContentAuthor ()
			: this(DefaultAuthor) { }

		/// <summary>
		/// Gets or sets the Gamertag of this author.
		/// </summary>
		public string Gamertag
		{
			get { return _gamertag; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(Encoding.UTF8.GetByteCount(value) <= 16);

				_gamertag = value;
				_xuid = 0;
				_signedIntoXboxLive = false;
			}
		}

		#region ISerializable<BinaryStream> Members

		void ISerializable<BinaryStream>.SerializeObject (BinaryStream s)
		{
			s.Stream(ref _xuid);
			s.StreamString(ref _gamertag, Encoding.UTF8, length: 16);
			s.Stream(ref _signedIntoXboxLive);
			s.PadBytes(3);
		}

		#endregion

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _xuid);
			s.StreamNullTerminatedString(ref _gamertag, Encoding.UTF8);
			s.Stream(ref _signedIntoXboxLive);
		}

		#endregion
	}
}
