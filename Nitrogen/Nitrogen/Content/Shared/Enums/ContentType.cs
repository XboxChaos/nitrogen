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

namespace Nitrogen.Content.Shared.Enums
{
    /// <summary>
    /// Specifies the type of this content.
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// Indicates that the content type isn't applicable to this content.
        /// </summary>
        NotApplicable,

        /// <summary>
        /// Indicates that this content is downloadable content.
        /// </summary>
        DownloadableContent,

        /// <summary>
        /// Indicates that this content is a campaign save.
        /// </summary>
        CampaignSave,

        /// <summary>
        /// Indicates that this content is a screenshot.
        /// </summary>
        Screenshot,

        /// <summary>
        /// Indicates that this content is a feature film.
        /// </summary>
        Film,

        /// <summary>
        /// Indicates that this content is a film clip.
        /// </summary>
        FilmClip,

        /// <summary>
        /// Indicates that this content is a map variant.
        /// </summary>
        MapVariant,

        /// <summary>
        /// Indicates that this content is a game variant.
        /// </summary>
        GameVariant,

        /// <summary>
        /// Indicates that this content is a playlist.
        /// </summary>
        Playlist,

        /// <summary>
        /// Indicates that this content is a cache file.
        /// </summary>
        Cache,
    }
}
