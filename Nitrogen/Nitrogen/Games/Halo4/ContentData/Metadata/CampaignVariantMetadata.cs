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

using Nitrogen.ContentData.Metadata;
using Nitrogen.IO;
using System;

namespace Nitrogen.Games.Halo4.ContentData.Metadata
{
    /// <summary>
    /// Provides metadata for a campaign game variant.
    /// </summary>
    public class CampaignVariantMetadata
        : ContentMetadata
    {
        private byte campaignId;
        private byte difficulty;
        private byte scoringMode;
        private int insertionPoint, skulls;

        /// <summary>
        /// Initializes a new instance of the <see cref="CampaignVariantMetadata"/> class with
        /// default values.
        /// </summary>
        public CampaignVariantMetadata()
            : base(ContentType.GameVariant) { }

        /// <summary>
        /// Gets or sets the selected difficulty level.
        /// </summary>
        public Difficulty Difficulty
        {
            get { return (Difficulty)this.difficulty; }
            set { this.difficulty = (byte)value; }
        }

        /// <summary>
        /// Gets or sets the metagame scoring mode.
        /// </summary>
        public ScoringMode MetagameScoring
        {
            get { return (ScoringMode)this.scoringMode; }
            set { this.scoringMode = (byte)value; }
        }

        /// <summary>
        /// Gets or sets the initial insertion point in the level.
        /// </summary>
        public int InsertionPoint
        {
            get { return this.insertionPoint; }
            set { this.insertionPoint = value; }
        }

        /// <summary>
        /// Gets or sets the active skulls.
        /// </summary>
        public Halo4Skulls ActiveSkulls
        {
            get { return (Halo4Skulls)this.skulls; }
            set { this.skulls = (int)value; }
        }

        #region IEndianStreamSerializable Members
        public override void Serialize(EndianStream s)
        {
            base.Serialize(s);

            if (Engine == GameEngine.Campaign)
            {
                s.Stream(ref this.campaignId);
                s.Stream(ref this.difficulty);
                s.Stream(ref this.scoringMode);
                s.Position += 1;
                s.Stream(ref this.insertionPoint);
                s.Stream(ref this.skulls);
            }
        }
        #endregion

        #region IBitStreamSerializable Members

        public override void Serialize(BitStream s)
        {
            base.Serialize(s);

            if (Engine == GameEngine.Campaign)
            {
                s.Stream(ref this.campaignId);
                s.Stream(ref this.difficulty, 2);
                s.Stream(ref this.scoringMode, 2);
                s.Stream(ref this.insertionPoint, 8);
                s.Stream(ref this.skulls);
            }
        }

        #endregion
    }
}
