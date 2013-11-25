/*
 *   Nitrogen - Halo Content API
 *   Copyright © 2013 The Nitrogen Authors. All rights reserved.
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

using Nitrogen;
using Nitrogen.Games.Halo4.ContentData;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4
{
    /// <summary>
    /// Represents a campaign blob for Halo 4 maps.
    /// </summary>
    public class Halo4CampaignBlob
        : CampaignBlob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4CampaignBlob"/> class with default values.
        /// </summary>
        public Halo4CampaignBlob()
            : base(new Halo4Campaign()) { }

        /// <summary>
        /// Gets or sets the campaign.
        /// </summary>
        public new Halo4Campaign Campaign
        {
            get { return base.Campaign as Halo4Campaign; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                base.Campaign = value;
            }
        }
    }
}
