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
using System.IO;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Chunks
{
    /// <summary>
    /// Defines the structure of the '_blf' (blob file? blam file?) header chunk.
    /// </summary>
    public class BlfTemplate
        : ChunkTemplate
    {
        public override string ChunkSignature { get { return "_blf"; } }

        public override bool IsWellDefined { get { return true; } }

        protected override void Initialize(Dictionary<int, Action> supportedVersions)
        {
            supportedVersions[0x01] = Define;
        }

        protected override void Define()
        {
            
        }
    }
}
