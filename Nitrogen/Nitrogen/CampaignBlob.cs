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

using Nitrogen.Blf;
using Nitrogen.ContentData;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen
{
    /// <summary>
    /// Provides a base implementation of a campaign blob.
    /// </summary>
    public abstract class CampaignBlob
        : Blob
    {
        private Campaign campaign;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignBlob"/> class with the specified
        /// <see cref="Campaign"/> object.
        /// </summary>
        /// <param name="campaign">
        /// A <see cref="Nitrogen.ContentData.Campaign"/> object to use as a default value.
        /// </param>
        public CampaignBlob(Campaign campaign)
        {
            Contract.Requires<ArgumentNullException>(campaign != null);
            this.campaign = campaign;
        }

        /// <summary>
        /// Gets or sets the campaign.
        /// </summary>
        public Campaign Campaign
        {
            get { return this.campaign; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.campaign = value;
            }
        }

        #region Blob Members

        protected override bool IsSigned { get { return true; } }

        protected override void Initialize(IList<Chunk> contents)
        {
            contents.Add(this.campaign);
        }

        #endregion
    }
}
