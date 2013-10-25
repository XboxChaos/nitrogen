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

using Nitrogen.Content.Shared.Enums;
using System;

namespace Nitrogen.Content.Shared
{
    /// <summary>
    /// Represents a content header.
    /// </summary>
    [Synchronizable]
    public class ContentHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentHeader"/> class with default values.
        /// </summary>
        public ContentHeader()
        {
            MapId = -1;
            CategoryIndex = -1;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            CreatedBy = new XIdentity();
            ModifiedBy = new XIdentity();
            ContentName = "unnamed content";
            ContentDescription = "no description available";
            GameVariantIconIndex = -1;
        }

        [PropertyBinding("chdr", "BuildNumber")]
        [PropertyBinding("mpvr", "Megalo/Header/BuildNumber")]
        public int BuildNumber { get; set; }

        [PropertyBinding("chdr", "ContentName")]
        [PropertyBinding("mpvr", "ContentHeader/ContentName")]
        public string ContentName { get; set; }

        [PropertyBinding("chdr", "ContentDescription")]
        [PropertyBinding("mpvr", "ContentHeader/ContentDescription")]
        public string ContentDescription { get; set; }

        [PropertyBinding("chdr", "MapId")]
        [PropertyBinding("mpvr", "ContentHeader/MapId")]
        public int MapId { get; set; }

        [PropertyBinding("chdr", "CategoryIndex")]
        [PropertyBinding("mpvr", "ContentHeader/CategoryIndex")]
        [PropertyBinding("mpvr", "Megalo/Metadata/CategoryIndex", plusOneEncoding: true)]
        public sbyte CategoryIndex { get; set; }

        [PropertyBinding("chdr", "DateCreated")]
        [PropertyBinding("mpvr", "ContentHeader/DateCreated")]
        public DateTime DateCreated { get; set; }

        [PropertyBinding("chdr", "DateModified")]
        [PropertyBinding("mpvr", "ContentHeader/DateModified")]
        public DateTime DateModified { get; set; }

        [PropertyBinding("chdr", "CreatedBy")]
        [PropertyBinding("mpvr", "ContentHeader/CreatedBy")]
        public XIdentity CreatedBy { get; set; }

        [PropertyBinding("chdr", "ModifiedBy")]
        [PropertyBinding("mpvr", "ContentHeader/ModifiedBy")]
        public XIdentity ModifiedBy { get; set; }

        [PropertyBinding("chdr", "GameVariantIconIndex")]
        [PropertyBinding("mpvr", "ContentHeader/GameVariantIconIndex")]
        [PropertyBinding("mpvr", "Megalo/Metadata/GameVariantIconIndex", plusOneEncoding: true)]
        public sbyte GameVariantIconIndex { get; set; }

        [PropertyBinding("chdr", "ContentType")]
        [PropertyBinding("mpvr", "ContentHeader/ContentType", plusOneEncoding: true)]
        public ContentType ContentType { get; set; }
    }
}
