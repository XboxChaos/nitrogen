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

using Nitrogen.Core.ContentData;
using Nitrogen.Core.IO;
using Nitrogen.Halo4.ContentData.MapVariants;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4.ContentData
{
    /// <summary>
    /// Represents a map variant.
    /// </summary>
    /// <remarks>Represents the 'mvar' chunk in a map variant BLF file.</remarks>
    public sealed class Halo4MapVariant
        : MapVariant
    {
        private Halo4MapVariantData data;

        public Halo4MapVariant()
            : base(version: 50)
        {
            this.data = new Halo4MapVariantData();
        }

        public Halo4MapVariantData Data
        {
            get { return this.data; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.data = value;
            }
        }

        #region MapVariant Members

        protected override void SerializeData(BitStream s)
        {
            this.data.Serialize(s);
        }

        #endregion
    }
}
