using System;

namespace Nitrogen.Metadata
{
    /// <summary>
    /// Specifies a difficulty level.
    /// </summary>
    public enum Difficulty
        : byte
    {
        Easy,
        Normal,
        Heroic,
        Legendary
    }

    /// <summary>
    /// Specifies a scoring mode for the metagame.
    /// </summary>
    public enum ScoringMode
        : byte
    {
        None,
        Team,
        Individual
    }

	/// <summary>
	/// Specifies a set of skulls.
	/// </summary>
	[Flags]
	public enum Skulls
	{
		None,
		Iron = 1 << 0,
		BlackEye = 1 << 1,
		ToughLuck = 1 << 2,
		Catch = 1 << 3,
		Fog = 1 << 4,
		Famine = 1 << 5,
		Thunderstorm = 1 << 6,
		Tilt = 1 << 7,
		Mythic = 1 << 8,
		Assassin = 1 << 9,
		Blind = 1 << 10,
		Superman = 1 << 11, // lol
		GruntBirthdayParty = 1 << 12,
		IWHBYD = 1 << 13,
		Red = 1 << 14,
		Yellow = 1 << 15,
		Blue = 1 << 16,
	}

	/// <summary>
	/// Specifies a game engine.
	/// </summary>
	public enum GameEngine
	{
		None = -1,
		Unspecified, // mainmenu?
		Forge,
		PVP,
		Campaign,
		Firefight,
		SpartanOps,
	}

	/// <summary>
	/// Specifies a game activity.
	/// </summary>
	internal enum GameActivity
	{
		None = -1,
		Unknown0,
		Unknown1,
		Matchmaking,
		Unknown3,
	}

	/// <summary>
	/// Specifies a type of a content.
	/// </summary>
	internal enum ContentType
	{
		None = -1,
		DownloadableContent,
		CampaignSave,
		Screenshot,
		Film,
		FilmClip,
		MapVariant,
		GameVariant,
		Playlist,
		UnlockableContent,
	}

	/// <summary>
	/// Specifies a game mode (or a lobby, generally speaking).
	/// </summary>
	internal enum GameMode
	{
		None = -1,
		Unspecified, // mainmenu?
		Campaign,
		Firefight,
		PVP,
		Forge,
		Theater,
		SpartanOps,
	}
}