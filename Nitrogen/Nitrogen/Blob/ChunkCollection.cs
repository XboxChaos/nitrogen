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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Nitrogen.Blob
{
    /// <summary>
    /// Specifies the chunk filtering behavior.
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// Referenced chunks are filtered.
        /// </summary>
        Exclusive,

        /// <summary>
        /// Only chunks which aren't referenced are filtered.
        /// </summary>
        Inclusive,
    }

    /// <summary>
    /// Represents a collection of <see cref="Chunks"/>.
    /// </summary>
    public class ChunkCollection
        : IEnumerable<Chunk>
    {
        private List<Chunk> collection;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkCollection"/> class containing the
        /// given chunks.
        /// </summary>
        public ChunkCollection(IEnumerable<Chunk> chunks)
            : this()
        {
            this.collection.AddRange(chunks);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkCollection"/> class with an empty
        /// chunk collection.
        /// </summary>
        protected ChunkCollection()
        {
            this.collection = new List<Chunk>();
        }

        /// <summary>
        /// Searches for a <see cref="Chunk"/> object with the specified <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature">The 4-character signature of the chunk.</param>
        /// <returns>A <see cref="Chunk"/> object if found.</returns>
        public virtual Chunk this[string signature]
        {
            get { return GetChunkFromSignature(signature); }
        }

        /// <summary>
        /// Creates a new <see cref="ChunkCollection"/> instance containing only <see cref="Chunk"/>
        /// objects matching a criteria.
        /// </summary>
        /// <param name="isInclusive">
        /// Specifies whether to exclude or include the chunks defined in <paramref name="signatures"/>.
        /// </param>
        /// <param name="signatures">An array of chunk signatures.</param>
        /// <returns>A <see cref="ChunkCollection"/> object containing the filtered chunks.</returns>
        public virtual ChunkCollection CreateFilteredCollection(FilterType filterType, params string[] signatures)
        {
            // Add qualified chunks to the filtered collection.
            var filteredCollection = new ChunkCollection();
            foreach (var chunk in this)
            {
                if ((signatures.Contains(chunk.Signature) && filterType == FilterType.Inclusive) ||
                    (!signatures.Contains(chunk.Signature) && filterType == FilterType.Exclusive))
                {
                    filteredCollection.collection.Add(chunk);
                }
            }
            return filteredCollection;
        }

        /// <summary>
        /// Checks whether this collection contains a chunk matching the specified <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature">The 4-character chunk signature.</param>
        /// <returns>true if this collection contains the chunk; otherwise, false.</returns>
        public virtual bool HasChunk(string signature)
        {
            foreach (var chunk in this)
            {
                if (chunk.Signature == signature)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Searches for a <see cref="Chunk"/> object with the specified <paramref name="signature"/>.
        /// </summary>
        /// <param name="signature">The 4-character signature of the chunk.</param>
        /// <returns>A <see cref="Chunk"/> object if found.</returns>
        public virtual Chunk GetChunkFromSignature(string signature)
        {
            Contract.Requires<ArgumentNullException>(signature != null);
            Contract.Requires<ArgumentException>(signature.Length == 4);
            Contract.Ensures(Contract.Result<Chunk>() != null);

            return (from b in this where b.Signature == signature select b).FirstOrDefault();
        }

        public IEnumerator<Chunk> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }
    }
}
