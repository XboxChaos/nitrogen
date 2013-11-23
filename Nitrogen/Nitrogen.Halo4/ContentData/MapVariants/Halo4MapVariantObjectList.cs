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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nitrogen.Core.ContentData.MapVariants;
using Nitrogen.Core.IO;

namespace Nitrogen.Halo4.ContentData.MapVariants
{
    /// <summary>
    /// An object list in a Halo 4 map variant.
    /// </summary>
    public class Halo4MapVariantObjectList
        : MapVariantObjectList<Halo4MapVariantObject>
    {
        private const int Halo4MaxObjects = 651;
        private const int Halo4SuggestedVectorSize = 21;

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4MapVariantObjectList"/> class.
        /// </summary>
        public Halo4MapVariantObjectList()
            : base(Halo4MaxObjects)
        {
        }

        /// <summary>
        /// Gets or sets the boundaries of the map to use to serialize objects.
        /// </summary>
        internal float[] Boundaries { get; set; }

        public override void Serialize(BitStream s)
        {
            VectorSerializationInfo vectorInfo = new VectorSerializationInfo(this.Boundaries, Halo4SuggestedVectorSize);

            if (s.State == StreamState.Read)
                ReadObjects(s, vectorInfo);
            else if (s.State == StreamState.Write)
                WriteObjects(s, vectorInfo);
        }

        /// <summary>
        /// Deserializes the object table from a stream.
        /// </summary>
        /// <param name="s">The <see cref="BitStream"/> to read from.</param>
        /// <param name="vectorInfo">Vector component info to use when serializing position vectors.</param>
        private void ReadObjects(BitStream s, VectorSerializationInfo vectorInfo)
        {
            for (int i = 0; i < Halo4MaxObjects; i++)
            {
                bool objectExists = false;
                s.Stream(ref objectExists);

                if (objectExists)
                {
                    Halo4MapVariantObjectHeader header = new Halo4MapVariantObjectHeader();
                    header.VectorInfo = vectorInfo;
                    header.Serialize(s);

                    Halo4MapVariantObject obj = ConstructObject(header);
                    obj.Serialize(s);

                    this.AddObject(obj);
                }
            }
        }

        /// <summary>
        /// Serializes the object table to a stream.
        /// </summary>
        /// <param name="s">The <see cref="BitStream"/> to write to.</param>
        /// <param name="vectorInfo">Vector component info to use when serializing position vectors.</param>
        private void WriteObjects(BitStream s, VectorSerializationInfo vectorInfo)
        {
            // Write existing objects
            int remaining = Halo4MaxObjects;
            foreach (Halo4MapVariantObject obj in this)
            {
                bool exists = true;
                s.Stream(ref exists);
                obj.Header.VectorInfo = vectorInfo;
                obj.Serialize(s);
                remaining--;
            }

            if (remaining < 0)
                throw new InvalidOperationException("remaining < 0");

            // Write that the rest of the objects don't exist
            for (int i = 0; i < remaining; i++)
            {
                bool exists = false;
                s.Stream(ref exists);
            }
        }

        /// <summary>
        /// Given an object header that was read, constructs the concrete object type corresponding to the header.
        /// </summary>
        /// <param name="header">The object header to use.</param>
        /// <returns>The constructed <see cref="Halo4MapVariantObject"/>.</returns>
        private static Halo4MapVariantObject ConstructObject(Halo4MapVariantObjectHeader header)
        {
            Contract.Requires<ArgumentNullException>(header != null);

            switch (header.Type)
            {
                case ObjectType.InitialOrdnanceDropPoint:
                    return new InitialOrdnanceDrop(header);
                case ObjectType.RandomOrdnanceDropPoint:
                    return new RandomOrdnanceDrop(header);
                case ObjectType.ObjectiveOrdnanceDropPoint:
                    return new ObjectiveOrdnanceDrop(header);
                case ObjectType.PersonalOrdnanceDropPoint:
                    return new PersonalOrdnanceDrop(header);
                case ObjectType.TraitZone:
                    return new TraitZoneObject(header);
                case ObjectType.NamedLocation:
                    return new NamedLocation(header);
                case ObjectType.Weapon:
                    return new Weapon(header);
                case ObjectType.Dispenser:
                    return new VehiclePad(header);
                default:
                    if ((int)header.Type > (int)ObjectType.NamedLocation)
                        return new SpecialObject(header);
                    else
                        return new SimpleObject(header);
            }
        }
    }
}
