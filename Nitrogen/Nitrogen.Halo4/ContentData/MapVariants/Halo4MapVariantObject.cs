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

using Nitrogen.Core.ContentData.MapVariants;
using Nitrogen.Core.IO;
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Halo4.ContentData.MapVariants
{
    public interface IObjectProperties : ISerializable<BitStream> { }

    public class Halo4MapVariantObject
        : MapVariantObject
    {
        private byte objectType;
        private short unk0;

        public Halo4MapVariantObject()
        {
        }

        /// <summary>
        /// Gets or sets the type of this object.
        /// </summary>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
        /// Value does not exist in the <see cref="ObjectType"/> enum.
        /// </exception>
        public ObjectType ObjectType
        {
            get { return (ObjectType)this.objectType; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(ObjectType), value));
                this.objectType = (byte)value;
            }
        }

        #region MapVariantObject Members

        public override void Serialize(BitStream s)
        {
            base.Serialize(s);

            s.Stream(ref this.objectType, 6);
            s.Stream(ref this.unk0, 10);

            /*if (IsOrdnanceDropPoint)
            {
                // serializeordnancedata
            }
            else
            {
                int4 (+1 encoded)
				byte
				optional int3
				sbyte
				sbyte
				optional byte Label 1
				optional byte Label 2
				optional byte Label 3
				optional byte Label 4

                //SerializeObjectProperties(s);
            }*/
        }

        #endregion
    }
}