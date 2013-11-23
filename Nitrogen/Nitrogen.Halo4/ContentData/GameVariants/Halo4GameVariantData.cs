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
using Nitrogen.Core.IO;
using Nitrogen.Core.ContentData.Metadata;
using Nitrogen.Core.ContentData.GameVariants;

namespace Nitrogen.Halo4.ContentData.GameVariants
{
    public class Halo4GameVariantData
        : ISerializable<BitStream>
    {
        private sbyte engine;
        private MegaloMetadataHeader megaloMetadata;
        private GameVariantMetadata metadata;
        private Halo4BaseVariant baseVariant;
        private Halo4MegaloData megaloData;

        public Halo4GameVariantData()
        {
            this.engine = (sbyte)GameEngine.PVP;
            this.megaloMetadata = new MegaloMetadataHeader();
            this.metadata = new GameVariantMetadata();
            this.baseVariant = new Halo4BaseVariant();
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
                        // s.Serialize(this.forgeData);
                    }
                    break;

                case GameEngine.PVP:
                    {
                        this.megaloData = this.megaloData ?? new Halo4MegaloData();
                        s.Serialize(this.megaloData);
                    }
                    break;

                case GameEngine.SpartanOps:
                    {

                    }
                    break;

                case GameEngine.Campaign:
                    {

                    }
                    break;
            }
        }

        #endregion
    }
}
