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
using System.Diagnostics.Contracts;
using System.IO;

namespace Nitrogen.Blob.Transport
{
    /// <summary>
    /// Provides an interface between a <see cref="ChunkCollection"/> instance and serialized data.
    /// </summary>
    public interface IBlobTransport
        : IDisposable
    {
        /// <summary>
        /// Writes the given chunk collection in its serialized form back to the underlying stream.
        /// </summary>
        /// <param name="chunks">
        /// A <see cref="ChunkCollection"/> instance containing the chunks to serialize.
        /// </param>
        void Serialize(ChunkCollection chunks);

        /// <summary>
        /// Deserializes the data in the underlying stream.
        /// </summary>
        /// <returns>
        /// A <see cref="ChunkCollection"/> instance containing the deserialized chunks.
        /// </returns>
        ChunkCollection Deserialize();
    }
}
