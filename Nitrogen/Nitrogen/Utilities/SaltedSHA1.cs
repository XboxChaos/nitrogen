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

using System.Security.Cryptography;

namespace Nitrogen.Utilities
{
    /// <summary>
    /// Computes salted SHA-1 hash digests.
    /// </summary>
    internal class SaltedSHA1
        : SHA1Managed
    {
        private byte[] salt;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaltedSHA1"/> class using the provided salt.
        /// </summary>
        public SaltedSHA1(byte[] salt)
        {
            this.salt = salt;
            Initialize();
        }

        /// <summary>
        /// Initializes an instance of <see cref="SaltedSHA1"/>.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            base.TransformBlock(this.salt, 0, this.salt.Length, this.salt, 0);
        }
    }
}