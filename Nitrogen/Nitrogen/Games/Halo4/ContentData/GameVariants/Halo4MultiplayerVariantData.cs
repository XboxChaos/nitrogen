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

using System;
using Nitrogen.IO;
using Nitrogen.ContentData.Metadata;
using Nitrogen.ContentData.GameVariants;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants
{
    public class Halo4MultiplayerVariantData
        : ISerializable<BitStream>
    {
        private sbyte engine;
        private MegaloMetadataHeader megaloMetadata;
        private GameVariantMetadata metadata;
        private Halo4BaseVariant baseVariant;
        private Halo4MegaloData megaloData;

        public Halo4MultiplayerVariantData()
        {
            this.engine = (sbyte)GameEngine.PVP;
            this.megaloMetadata = new MegaloMetadataHeader();
            this.metadata = new GameVariantMetadata();
            this.baseVariant = new Halo4BaseVariant();
        }

        /// <summary>
        /// Gets or sets the base variant data.
        /// </summary>
        public Halo4BaseVariant BaseVariant
        {
            get { return this.baseVariant; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.baseVariant = value;
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.engine, 4);
            GameEngine gameEngine = (GameEngine)this.engine;
            if (gameEngine == GameEngine.Forge || gameEngine == GameEngine.PVP)
            {
                s.Serialize(this.megaloMetadata);
            }

            s.Serialize(this.metadata);
            s.Serialize(this.baseVariant);

            switch (gameEngine)
            {
                case GameEngine.Forge:
                    {
                        this.megaloData = this.megaloData ?? new Halo4MegaloData();
                        // this.forgeData = this.forgeData ?? new Halo4ForgeData();

                        s.Serialize(this.megaloData);
                        // weapon tuning
                        // s.Serialize(this.forgeData);
                    }
                    break;

                case GameEngine.PVP:
                    {
                        this.megaloData = this.megaloData ?? new Halo4MegaloData();
                        s.Serialize(this.megaloData);
                        // weapon tuning
                    }
                    break;

                case GameEngine.SpartanOps:
                    {
                        // TODO: Read Sp Ops data (seriously, someone please figure out how to play offline sp ops)
                    }
                    break;

                case GameEngine.Campaign:
                    {
                        // Matt: I'm 97% sure that campaign doesn't have any settings outside the metadata.
                    }
                    break;
            }
        }

        #endregion
    }
}
