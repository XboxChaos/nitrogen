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

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared.Megalo.ParameterTypes
{
    /// <summary>
    /// Defines the structure of a shape in a Megalo action or condition.
    /// </summary>
    internal class ShapeData
        : DataTemplate
    {
        protected override void Define()
        {
            Group("Shape", () =>
            {
                var type = Register<byte>("Type", n: 2);
                switch (type)
                {
                    case 1: // Sphere
                        Group("Radius", () => Import<IntegerReferenceData>());
                        break;

                    case 2: // Cylinder
                        Group("Radius", () => Import<IntegerReferenceData>());
                        Group("Top", () => Import<IntegerReferenceData>());
                        Group("Bottom", () => Import<IntegerReferenceData>());
                        break;

                    case 3: // Box
                        Group("Width", () => Import<IntegerReferenceData>());
                        Group("Length", () => Import<IntegerReferenceData>());
                        Group("Top", () => Import<IntegerReferenceData>());
                        Group("Bottom", () => Import<IntegerReferenceData>());
                        break;
                }
            });
        }
    }
}
