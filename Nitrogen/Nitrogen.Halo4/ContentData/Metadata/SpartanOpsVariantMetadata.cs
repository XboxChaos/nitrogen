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

using Nitrogen.Core.ContentData.Metadata;
using Nitrogen.Core.IO;
using System;

namespace Nitrogen.Halo4.ContentData.Metadata
{
    /// <summary>
    /// Provides metadata for a Spartan Ops game variant.
    /// </summary>
    public class SpartanOpsVariantMetadata
        : ContentMetadata
    {
        private byte difficulty;
        private int skulls;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpartanOpsVariantMetadata"/> class with
        /// default values.
        /// </summary>
        public SpartanOpsVariantMetadata()
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

            if (Engine == GameEngine.SpartanOps)
            {
                s.Stream(ref this.difficulty);
                s.Position += 3;
                s.Stream(ref this.skulls);
            }
        }

        #endregion

        #region IBitStreamSerializable Members

        public override void Serialize(BitStream s)
        {
            base.Serialize(s);

            if (Engine == GameEngine.SpartanOps)
            {
                s.Stream(ref this.difficulty, 2);
                s.Stream(ref this.skulls);
            }
        }

        #endregion
    }
}