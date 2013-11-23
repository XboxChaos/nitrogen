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

using Nitrogen.Core;
using Nitrogen.Core.ContentData;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Core
{
    /// <summary>
    /// Represents a campaign blob for pre-Halo 4 maps.
    /// </summary>
    public class CampaignBlobShared
        : CampaignBlob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignBlobShared"/> class with default values.
        /// </summary>
        public CampaignBlobShared()
            : base(new CampaignShared()) { }

        /// <summary>
        /// Gets or sets the campaign.
        /// </summary>
        public new CampaignShared Campaign
        {
            get { return base.Campaign as CampaignShared; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                base.Campaign = value;
            }
        }
    }
}
