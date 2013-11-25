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
using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Nitrogen.ContentData
{
    /// <summary>
    /// Represents a map image (i.e. thumbnail).
    /// </summary>
    /// <remarks>Represents the 'mapi' chunk in a BLF file.</remarks>
    public class MapImage
        : Chunk
    {
        private int unk0;
        private Image image;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapImage"/> class with default values.
        /// </summary>
        public MapImage()
            : base("mapi", 1)
        {
            this.image = new Bitmap(1, 1);
        }

        /// <summary>
        /// Gets or sets the map image.
        /// </summary>
        public Image Image
        {
            get { return this.image; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.image = value;
            }
        }

        #region Chunk Members

        protected override void SerializeEndianStreamData(EndianStream s)
        {
            s.Stream(ref this.unk0);

            if (s.State == StreamState.Read)
            {
                int size;
                s.Reader.Read(out size);
                byte[] data = new byte[size];
                s.Reader.Read(data, size);

                this.image = (Image)new ImageConverter().ConvertFrom(data);
            }
            else if (s.State == StreamState.Write)
            {
                using (var buffer = new MemoryStream())
                {
                    this.image.Save(buffer, this.image.RawFormat);
                    byte[] imageData = buffer.ToArray();

                    s.Writer.Write(imageData.Length);
                    s.Writer.Write(imageData);
                }
            }
        }

        #endregion
    }
}
