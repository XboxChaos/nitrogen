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

using Nitrogen.Blob;
using Nitrogen.Blob.Transport;
using Nitrogen.Blob.Transport.BinaryTemplates;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.Content
{
    [Synchronizable]
    public abstract class ContentBase<T> : IDisposable
        where T : class, new()
    {
        private IBlobTransport transport;
        private ChunkCollection chunks;
        private Stream baseStream;

        ~ContentBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets a user data object to be passed to synchronization methods.
        /// </summary>
        protected object SyncUserData { get; set; }

        protected abstract TemplateSet BlfTemplateSet { get; }

        public static T Load(Stream stream)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<IOException>(stream.CanRead);

            var content = new T() as ContentBase<T>;

            // Determine the type of the file by reading the first few characters.
            byte[] peekBytes = new byte[8];
            stream.Read(peekBytes, 0, peekBytes.Length);
            stream.Position -= peekBytes.Length;
            string peekChars = Encoding.UTF8.GetString(peekBytes);
            if (peekChars.Contains("xml"))
            {
                content.transport = new XmlTransport(stream, XmlTransportFlags.LeaveOpen);
            }
            else if (peekChars.Contains("_blf"))
            {
                var blf = new BlfTransport(stream, BlfTransportFlags.LeaveOpen);
                blf.RegisterTemplateSet(content.BlfTemplateSet);
                content.baseStream = stream;
                content.transport = blf;
            }
            else if (peekChars.Contains("CON"))
            {
                // TODO: Add STFS support
                throw new NotSupportedException("CON files are not supported in this version.");
            }
            else
            {
                throw new NotSupportedException("This file type is not supported.");
            }

            // Deserialize stream and bind data to this instance.
            content.chunks = content.transport.Deserialize();
            content.Synchronize(SynchronizationMode.UpdateProperties);

            return content as T;
        }

        public static T Create(Stream outputStream)
        {
            Contract.Requires<ArgumentNullException>(outputStream != null);
            Contract.Requires<IOException>(outputStream.CanWrite);

            var content = new T() as ContentBase<T>;

            var blf = new BlfTransport(outputStream, BlfTransportFlags.LeaveOpen | BlfTransportFlags.ReallocateChunks);
            blf.RegisterTemplateSet(content.BlfTemplateSet);
            content.baseStream = outputStream;
            content.transport = blf;
            content.chunks = new ChunkCollection(new Chunk[0]);

            return content as T;
        }

        public void SaveChanges()
        {
            Synchronize(SynchronizationMode.UpdateDataTable);
            this.transport.Serialize(this.chunks);

            OnSerialize(this.baseStream);
        }

        protected virtual void OnSerialize(Stream stream)
        {
            // Do nothing by default.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.transport.Dispose();
                this.transport = null;
            }
        }

        private void Synchronize(SynchronizationMode mode)
        {
            foreach (var chunk in this.chunks)
                PropertySynchronizer.Synchronize(this, mode, chunk, SyncUserData, null);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
