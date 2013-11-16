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

using Nitrogen.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

namespace Nitrogen.Core.Blf
{
    public abstract class Blob
        : ISerializable<EndianStream>
    {
        private IList<Chunk> chunks;
        private Chunk header, footer;

        protected Blob()
        {
            this.header = new BlobHeader();
            this.footer = new BlobFooter();
        }

        protected virtual bool IsSigned { get { return false; } }

        public static T Create<T>() // why not?
            where T : Blob, new()
        {
            return new T();
        }

        public static T Load<T>(Stream input, int offset = 0)
            where T : Blob, new()
        {
            Contract.Requires<ArgumentNullException>(input != null);

            input.Position = offset;
            using (var stream = new EndianStream(input, StreamState.Read, ByteOrder.BigEndian, true))
            {
                var blob = new T();
                blob.Serialize(stream);
                return blob;
            }
        }

        public static void Save<T>(Stream output, T data, int offset = 0)
            where T : Blob
        {
            Contract.Requires<ArgumentNullException>(output != null);

            output.Position = offset;
            using (var stream = new EndianStream(output, StreamState.Write, ByteOrder.BigEndian, true))
                data.Serialize(stream);
        }

        public void Serialize(EndianStream s)
        {
            // Define chunk contents if we haven't done so.
            if (this.chunks == null)
            {
                this.chunks = new List<Chunk>();
                Initialize(this.chunks);
            }

            s.Serialize(this.header);

            foreach (Chunk c in this.chunks)
                s.Serialize(c);
            
            var footer = (this.footer as BlobFooter);
            footer.ChunkOffset = (int)s.Position;
            if (IsSigned) { footer.Resign(); }
            s.Serialize(this.footer);
        }

        protected abstract void Initialize(IList<Chunk> contents);
    }
}
