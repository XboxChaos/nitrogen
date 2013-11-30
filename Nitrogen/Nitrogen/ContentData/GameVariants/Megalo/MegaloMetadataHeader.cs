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

using Nitrogen.IO;
using System;

namespace Nitrogen.ContentData.GameVariants.Megalo
{
    public class MegaloMetadataHeader
        : ISerializable<BitStream>
    {
        private int encodingVersion;
        private uint buildNumber;

        public int EncodingVersion
        {
            get { return this.encodingVersion; }
            set { this.encodingVersion = value; }
        }

        public ushort BuildNumber
        {
            get { return (ushort)this.buildNumber; }
            set { this.buildNumber = (ushort)value; }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.encodingVersion);
            s.Stream(ref this.buildNumber);
        }

        #endregion
    }
}
