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

namespace Nitrogen.Blob.Transport.BinaryTemplates.Halo4
{
    /// <summary>
    /// Defines the structure of Halo 4 Forge settings.
    /// </summary>
    internal class Halo4ForgeSettings
        : DataTemplate
    {
        protected override void Define()
        {
            Register<byte[]>(count: 16); // This shouldn't be necessary... might've missed something....
            // OH MIGHT BE CUZ I ACCIDENTALLY OVERWROTE THE TEST FORGE VARIANT WITHOUT FORGE SETTINGS :P

            Register<bool>();
            Register<bool>();
            Register<byte>("EditMode", n: 2);
            Register<byte>("RespawnTime", n: 6);
            Group("EditorTraits", () => Import<Halo4.Shared.Halo4TraitSet>());
        }
    }
}
