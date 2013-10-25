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
using System.Diagnostics;

namespace Nitrogen.Blob.Transport.BinaryTemplates
{
    /// <summary>
    /// Extends the <see cref="DataTemplate"/> class with additional properties for chunk templates.
    /// </summary>
    public abstract class ChunkTemplate
        : DataTemplate
    {
        internal const string ChunkVersion = "ChunkVersion";

        private Dictionary<int, Action> versionHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkTemplate"/> class.
        /// </summary>
        protected ChunkTemplate()
        {
            this.versionHandlers = new Dictionary<int, Action>();
        }

        /// <summary>
        /// When overridden in a derived class, gets the signature of the applicable chunk.
        /// </summary>
        public abstract string ChunkSignature { get; }

        /// <summary>
        /// When overridden in a derived class, gets a boolean value indicating whether this chunk
        /// has a well-defined template.
        /// </summary>
        public abstract bool IsWellDefined { get; }

        /// <summary>
        /// Gets the local context.
        /// </summary>
        protected Context NamedArgs { get; private set; }

        /// <summary>
        /// When overridden by a derived class, specifies which version of the chunk is supported.
        /// </summary>
        /// <param name="supportedVersions"></param>
        protected abstract void Initialize(Dictionary<int, Action> supportedVersions);

        protected override void Define()
        {
            Debug.Fail("This method should never be called.");
        }

        protected override void Define(Context namedArgs)
        {
            // Chunk version info must be provided.
            NamedArgs = namedArgs;
            if (!namedArgs.ContainsKey(ChunkTemplate.ChunkVersion))
                throw new InvalidOperationException();

            // Initializes the chunk template (typically to specify methods for certain versions).
            Initialize(this.versionHandlers);

            // Invokes the method associated with the chunk version.
            int chunkVersion = Convert.ToInt32(namedArgs[ChunkTemplate.ChunkVersion]);
            if (this.versionHandlers.ContainsKey(chunkVersion))
                this.versionHandlers[chunkVersion]();
        }
    }
}