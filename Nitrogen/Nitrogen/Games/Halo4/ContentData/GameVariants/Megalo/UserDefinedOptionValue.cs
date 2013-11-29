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

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo
{
    public class UserDefinedOptionValue
        : IUserDefinedOptionValue
    {
        private short value;
        private byte nameStringIndex, descStringIndex;

        public byte NameStringIndex
        {
            get { return this.nameStringIndex; }
            set { this.nameStringIndex = value; }
        }

        public byte DescriptionStringIndex
        {
            get { return this.descStringIndex; }
            set { this.descStringIndex = value; }
        }

        #region IUserDefinedOptionValue Members

        public short Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.value, 10);
            s.Stream(ref this.nameStringIndex);
            s.Stream(ref this.descStringIndex);
        }

        #endregion
    }

    public class UserDefinedOptionRangeEndpoint
        : IUserDefinedOptionValue
    {
        private short value;

        #region IUserDefinedOptionValue Members

        public short Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.value, 10);
        }

        #endregion
    }

    public interface IUserDefinedOptionValue
        : ISerializable<BitStream>
    {
        short Value { get; set; }
    }
}
