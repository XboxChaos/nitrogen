using Nitrogen.Blf;
using Nitrogen.GameVariants;
using Nitrogen.IO;
using Nitrogen.MapVariants;
using Nitrogen.Metadata;
using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace Nitrogen
{
	/// <summary>
	/// Provides various static methods for the creation of valid BLF files.
	/// </summary>
	public static class ContentFactory
	{
		public static void CreateGameVariant(Stream output, GameVariant gameVariant)
		{
			Contract.Requires<ArgumentNullException>(output != null && gameVariant != null);
			Contract.Requires(output.CanWrite);

			gameVariant.Metadata.ContentType = ContentType.GameVariant;
			gameVariant.Metadata.FileLength = 0x7F29;

			var contentHeader = new ContentHeader(gameVariant.Metadata);
			var multiplayerVariant = new MultiplayerVariant(gameVariant);

			var chdr = new Chunk("chdr", version: 10, flags: ChunkFlags.IsHeader, payload: contentHeader);
			var mpvr = new Chunk("mpvr", version: 132, flags: ChunkFlags.IsInitialized, payload: multiplayerVariant);

			var blob = new Blob(chdr, mpvr);

			using ( var s = new BinaryStream(output, StreamState.Write, ByteOrder.BigEndian, false) )
				s.Serialize(blob);
		}

		public static void CreateMapVariant (Stream output, MapVariant mapVariant)
		{
			Contract.Requires<ArgumentNullException>(output != null && mapVariant != null);
			Contract.Requires(output.CanWrite);

			mapVariant.Metadata.ContentType = ContentType.MapVariant;
			mapVariant.Metadata.FileLength = 0x7329;

			var contentHeader = new ContentHeader(mapVariant.Metadata);
			var usermap = new Usermap(mapVariant);

			var chdr = new Chunk("chdr", version: 10, flags: ChunkFlags.IsHeader, payload: contentHeader);
			var mvar = new Chunk("mvar", version: 50, flags: ChunkFlags.IsInitialized, payload: usermap);

			var blob = new Blob(chdr, mvar);

			using ( var s = new BinaryStream(output, StreamState.Write, ByteOrder.BigEndian, false) )
				s.Serialize(blob);
		}

		public static GameVariant ReadGameVariant (Stream input)
		{
			Contract.Requires<ArgumentNullException>(input != null);
			Contract.Requires(input.CanRead);

			input.Position = 0x2FC;
			var multiplayerVariant = new MultiplayerVariant(new GameVariant());

			using ( var s = new BinaryStream(input, StreamState.Read, ByteOrder.BigEndian, false) )
				( multiplayerVariant as ISerializable<BinaryStream> ).SerializeObject(s);

			return multiplayerVariant.VariantData;
		}
	}
}
