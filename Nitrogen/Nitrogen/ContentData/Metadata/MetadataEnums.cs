/*
 *   Nitrogen - Halo Content API
 *   Copyright (c) 2013 Matt Saville and Aaron Dierking
 * 
 *   This file is part of Nitrogen.
 *
 *   Nitrogen is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   Nitrogen is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with Nitrogen.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace Nitrogen.Core.ContentData.Metadata
{
    /// <summary>
    /// Specifies a type of a content.
    /// </summary>
    public enum ContentType
        : sbyte
    {
        NotApplicable = -1,
        DLC,
        CampaignSave,
        Screenshot,
        Film,
        FilmClip,
        MapVariant,
        GameVariant,
        Playlist,
        Unlocks,
    }

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
    /// Specifies a game engine.
    /// </summary>
    public enum GameEngine
        : sbyte
    {
        NotApplicable = -1,
        Unspecified,
        Forge,
        PVP,
        Campaign,
        Firefight,
        SpartanOps,
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
    /// Specifies a game activity.
    /// </summary>
    internal enum GameActivity
        : sbyte
    {
        NotApplicable = -1,
        Unspecified,
        Unknown1,
        Matchmaking,

        // TODO: Finish this enum.
    }

    /// <summary>
    /// Specifies a game mode (or a lobby, generally speaking).
    /// </summary>
    internal enum GameMode
        : sbyte
    {
        NotApplicable = -1,
        Unspecified,
        Campaign,
        Firefight,
        PVP,
        Forge,
        Theater,
        SpartanOps,
    }
}
