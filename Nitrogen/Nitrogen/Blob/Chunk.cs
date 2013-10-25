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

using Nitrogen.Utilities;
using System;
using System.Collections.Generic;

namespace Nitrogen.Blob
{
    /// <summary>
    /// Specifies additional properties for a <see cref="Chunk"/> instance.
    /// </summary>
    [Flags]
    public enum ChunkFlags
    {
        /// <summary>
        /// Indicates that there are no flags associated with the chunk.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that this chunk has been initialized.
        /// </summary>
        /// <remarks>
        /// Every chunk except header chunks has this flag. kornman claims it's an "Is Initialized"
        /// flag, and who am I to say otherwise?
        /// </remarks>
        IsInitialized = 1 << 0,

        /// <summary>
        /// Indicates that this chunk is a header of some sort.
        /// </summary>
        IsHeader = 1 << 1,
    }

    /// <summary>
    /// Represents a chunk in a blob.
    /// </summary>
    public class Chunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class with the specified properties.
        /// </summary>
        /// <param name="signature">The 4-character signature of the chunk.</param>
        /// <param name="version">The chunk version.</param>
        /// <param name="flags">The chunk flags.</param>
        /// <param name="data">
        /// A <see cref="DataTable"/> object containing the deserialized chunk data.
        /// </param>
        public Chunk(string signature, int version, ChunkFlags flags, DataTable data)
        {
            Signature = signature;
            Version = version;
            Flags = flags;
            Data = data;
        }

        /// <summary>
        /// Gets the 4-character signature of this <see cref="Chunk"/> instance.
        /// </summary>
        public virtual string Signature { get; protected set; }

        /// <summary>
        /// Gets the version of this <see cref="Chunk"/> instance.
        /// </summary>
        public virtual int Version { get; protected set; }

        /// <summary>
        /// Gets the flags associated with this <see cref="Chunk"/> instance.
        /// </summary>
        public virtual ChunkFlags Flags { get; protected set; }

        /// <summary>
        /// Gets the data contained in this <see cref="Chunk"/> instance.
        /// </summary>
        public virtual DataTable Data { get; protected set; }

        /// <summary>
        /// Gets or sets the offset of this chunk in a stream.
        /// </summary>
        internal long Offset { get; set; }
    }
}
