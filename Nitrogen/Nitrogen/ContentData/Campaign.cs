/*
 *   Nitrogen - Halo Content API
 *   Copyright (c) 2013 Collin Dillinger, Matt Saville and Aaron Dierking
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

using Nitrogen.Core.Blf;
using Nitrogen.Core.ContentData.Localization;
using Nitrogen.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Core.ContentData
{
    /// <summary>
    /// Provides a base implementation of an overview of a campaign.
    /// </summary>
    /// <remarks>Represents the 'cmpn' chunk in a map info BLF file.</remarks>
    public abstract class Campaign
        : Chunk
    {
        private int unk0, unk1;
        private LocalizedString name;
        private LocalizedString description;

        /// <summary>
        /// Initializes a new instance of the <see cref="Campaign"/> class with the specified version
        /// and default values.
        /// </summary>
        /// <param name="version">The version of this instance.</param>
        public Campaign(short version)
            : base("cmpn", version)
        {
            this.unk0 = 1;
            this.unk1 = 0;
        }

        /// <summary>
        /// Gets or sets the localized description of this campaign.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public LocalizedString Description
        {
            get { return this.description; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.description = value;
            }
        }

        /// <summary>
        /// Gets or sets the localized name of this campaign.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public LocalizedString Name
        {
            get { return this.name; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.name = value;
            }
        }

        #region Campaign Members

        protected override void SerializeEndianStreamData(IO.EndianStream s)
        {
            s.Stream(ref this.unk0);
            s.Stream(ref this.unk1);
            this.name.Serialize(s);
            this.description.Serialize(s);
        }

        #endregion
    }
}
