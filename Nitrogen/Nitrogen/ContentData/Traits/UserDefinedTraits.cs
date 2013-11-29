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
using System.Diagnostics.Contracts;

namespace Nitrogen.ContentData.Traits
{
    public class UserDefinedTraits<TTraits>
        : ISerializable<BitStream>
        where TTraits : ISerializable<BitStream>, new()
    {
        private TTraits traits;
        private byte nameStringIndex, descStringIndex;
        private bool hidden, runtime;

        public UserDefinedTraits()
        {
            this.traits = new TTraits();
        }

        /// <summary>
        /// Gets or sets the index of the name string in the user-defined string table.
        /// </summary>
        public byte NameStringIndex
        {
            get { return this.nameStringIndex; }
            set { this.nameStringIndex = value; }
        }

        /// <summary>
        /// Gets or sets the index of the description string in the user-defined string table.
        /// </summary>
        public byte DescriptionStringIndex
        {
            get { return this.descStringIndex; }
            set { this.descStringIndex = value; }
        }

        /// <summary>
        /// Gets or sets whether this trait set is visible in-game.
        /// </summary>
        public bool IsHidden
        {
            get { return this.hidden; }
            set { this.hidden = value; }
        }

        /// <summary>
        /// Gets or sets whether the initial equipment traits are hidden in-game.
        /// </summary>
        public bool UseRuntimeTemplate
        {
            get { return this.runtime; }
            set { this.runtime = value; }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.nameStringIndex);
            s.Stream(ref this.descStringIndex);
            s.Serialize(this.traits);
            s.Stream(ref this.hidden);
            s.Stream(ref this.runtime);
        }

        #endregion
    }
}