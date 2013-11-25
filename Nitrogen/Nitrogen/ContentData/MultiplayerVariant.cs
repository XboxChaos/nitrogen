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

using Nitrogen.Blf;
using Nitrogen.IO;
using System.IO;

namespace Nitrogen.ContentData
{
    /// <summary>
    /// Provides a base implementation of a multiplayer game variant.
    /// </summary>
    /// <remarks>Represents the 'mpvr' chunk in a game variant BLF file.</remarks>
    public abstract class MultiplayerVariant
        : Chunk
    {
        private MemoryStream dataBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplayerVariant"/> class with the
        /// specified version and default values.
        /// </summary>
        /// <param name="version">The version of this instance.</param>
        public MultiplayerVariant(short version)
            : base("mvar", version)
        {
            this.dataBuffer = new MemoryStream();
        }

        protected abstract void SerializeData(BitStream s);

        #region Chunk Members

        protected override void SerializeEndianStreamData(EndianStream s)
        {
            int length;
            s.Position += 24; // Skip hash for now.

            // Read map variant data into the buffer when deserializing.
            if (s.State == StreamState.Read)
            {
                s.Reader.Read(out length);

                var data = new byte[length];
                s.Reader.Read(data, length);

                this.dataBuffer.Write(data, 0, length);
            }
            else if (s.State == StreamState.Write)
            {
                this.dataBuffer.SetLength(0);
            }
            this.dataBuffer.Position = 0;
            s.Position = 0;

            // Stream the encoded multiplayer variant data to/from the buffer.
            using (var bitStream = new BitStream(this.dataBuffer, s.State, true))
            {
                SerializeData(bitStream);
            }
            length = (int)this.dataBuffer.Length;

            // TODO: Generate hash.
            var hash = new byte[20];
            s.Stream(hash, 0, hash.Length);

            s.Stream(ref length);
            s.Position += 4;
            s.Stream(this.dataBuffer.ToArray(), 0, (int)this.dataBuffer.Length);
        }

        #endregion
    }
}
