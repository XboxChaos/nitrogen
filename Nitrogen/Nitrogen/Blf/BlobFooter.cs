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

using System;

namespace Nitrogen.Blf
{
    internal class BlobFooter
        : Chunk
    {
        private int chunkOffset;
        private byte flags;
        private byte[] rsaSignature;

        internal BlobFooter()
            : base("_eof", 1)
        {
            this.rsaSignature = new byte[256];
        }

        internal int ChunkOffset
        {
            get { return this.chunkOffset; }
            set { this.chunkOffset = value; }
        }

        /// <summary>
        /// Appends an empty RSA signature to this chunk. This is needed for signed BLF files. The
        /// modified xex ignores the signature but the chunk size needs to accomodate the signature.
        /// </summary>
        internal void Resign()
        {
            this.flags = 3;
        }

        protected override void SerializeEndianStreamData(IO.EndianStream s)
        {
            s.Stream(ref this.chunkOffset);
            s.Stream(ref this.flags);

            if (this.flags == 3)
            {
                s.Stream(this.rsaSignature, 0, this.rsaSignature.Length);
            }
        }
    }
}
