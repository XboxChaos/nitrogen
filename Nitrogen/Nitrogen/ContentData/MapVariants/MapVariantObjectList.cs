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
using Nitrogen.Core.IO;

namespace Nitrogen.Core.ContentData.MapVariants
{
    public abstract class MapVariantObjectList<TObject>
        : ISerializable<BitStream>, IEnumerable<TObject>
    {
        private List<TObject> objects;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapVariantObjectList{TObject}"/> class.
        /// The object list is initialized to a fixed amount of null objects.
        /// </summary>
        /// <param name="maxObjects">The maximum number of objects in the list.</param>
        public MapVariantObjectList(int maxObjects)
        {
            Contract.Requires<ArgumentOutOfRangeException>(maxObjects > 0);
            this.objects = new List<TObject>();
            this.MaxCount = maxObjects;
        }

        /// <summary>
        /// Adds an object to the object list.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        /// <returns><c>true</c> if the object was successfully added.</returns>
        public bool AddObject(TObject obj)
        {
            Contract.Requires<ArgumentNullException>(obj != null);

            if (this.objects.Count < this.MaxCount)
            {
                this.objects.Add(obj);
                return true;
            }
            return false; // No room
        }

        /// <summary>
        /// Removes an object from the object list.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        /// <returns><c>true</c> if the object was successfully removed.</returns>
        public bool RemoveObject(TObject obj)
        {
            return this.objects.Remove(obj);
        }

        /// <summary>
        /// Gets the maximum number of objects that the list can hold.
        /// </summary>
        public int MaxCount { get; private set; }

        /// <summary>
        /// Determines whether or not the object list is full.
        /// </summary>
        /// <returns><c>true</c> if the list is full and no more objects can be added.</returns>
        public bool IsFull
        {
            get { return this.objects.Count == this.MaxCount; }
        }

        public abstract void Serialize(BitStream stream);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TObject> GetEnumerator()
        {
            return this.objects.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)this.objects).GetEnumerator();
        }
    }
}
