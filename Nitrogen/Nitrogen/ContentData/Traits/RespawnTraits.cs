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

using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.ContentData.Traits
{
    public class RespawnTraits<TTraits>
        : ISerializable<BitStream>
        where TTraits : ISerializable<BitStream>, new()
    {
        private TTraits traits;
        private byte duration;

        public RespawnTraits()
        {
            this.traits = new TTraits();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RespawnTraits"/> class with the specified
        /// <see cref="duration"/>.
        /// </summary>
        /// <param name="duration">The duration in seconds.</param>
        /// <remarks>The maximum duration is 60 seconds.</remarks>
        public RespawnTraits(byte duration)
        {
            Contract.Requires<ArgumentOutOfRangeException>(duration <= 60);
            this.duration = duration;
        }

        /// <summary>
        /// Gets or sets how long in seconds this trait set is active after respawning.
        /// </summary>
        /// <remarks>The maximum value is 60 seconds.</remarks>
        public byte Duration
        {
            get { return this.duration; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 60);
                this.duration = value;
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.duration, 6);
            s.Serialize(this.traits);
        }

        #endregion
    }
}