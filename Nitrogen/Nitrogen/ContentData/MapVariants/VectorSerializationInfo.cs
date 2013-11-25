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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrogen.ContentData.MapVariants
{
    /// <summary>
    /// Contains information about the size of each value in a serialized 3-D vector.
    /// </summary>
    public class VectorSerializationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VectorSerializationInfo"/> class.
        /// Sizes will be computed from a bounds array and a suggested size value.
        /// </summary>
        /// <param name="bounds">The bounds array to use. This must be made up of 6 elements (3 pairs representing X, Y, and Z respectively), with each pair of two elements representing a minimum and a maximum.</param>
        /// <param name="suggestedValue">The suggested size value to use for each component.</param>
        public VectorSerializationInfo(float[] bounds, int suggestedValue)
        {
            int[] sizes = CalculateSizes(bounds, suggestedValue);
            XSize = sizes[0];
            YSize = sizes[1];
            ZSize = sizes[2];
            
            XMin = bounds[0];
            XMax = bounds[1];
            YMin = bounds[2];
            YMax = bounds[3];
            ZMin = bounds[4];
            ZMax = bounds[5];
        }

        /// <summary>
        /// Gets the minimum X value allowed.
        /// </summary>
        public float XMin { get; private set; }

        /// <summary>
        /// Gets the maximum X value allowed.
        /// </summary>
        public float XMax { get; private set; }

        /// <summary>
        /// Gets the minimum Y value allowed.
        /// </summary>
        public float YMin { get; private set; }

        /// <summary>
        /// Gets the maximum Y value allowed.
        /// </summary>
        public float YMax { get; private set; }

        /// <summary>
        /// Gets the minimum Z value allowed.
        /// </summary>
        public float ZMin { get; private set; }

        /// <summary>
        /// Gets the maximum Z value allowed.
        /// </summary>
        public float ZMax { get; private set; }

        /// <summary>
        /// Gets the size of the vector's X component in bits when serialized.
        /// </summary>
        public int XSize { get; private set; }

        /// <summary>
        /// Gets the size of the vector's Y component in bits when serialized.
        /// </summary>
        public int YSize { get; private set; }

        /// <summary>
        /// Gets the size of the vector's Z component in bits when serialized.
        /// </summary>
        public int ZSize { get; private set; }

        private static int[] CalculateSizes(float[] bounds, int suggestedValue)
        {
            var ranges = new float[3];
            ranges[0] = bounds[1] - bounds[0];
            ranges[1] = bounds[3] - bounds[2];
            ranges[2] = bounds[5] - bounds[4];

            var result = new int[3];
            result[0] = suggestedValue;
            result[1] = suggestedValue;
            result[2] = suggestedValue;

            float unknown1;
            if (suggestedValue <= 16)
                unknown1 = 1.0f / 120.0f * (1 << (16 - suggestedValue));
            else
                unknown1 = 1.0f / 120.0f / (1 << (suggestedValue - 16));

            if (unknown1 <= 0.0001f)
            {
                result[0] = 26;
                result[1] = 26;
                result[2] = 26;
                return result;
            }

            var unknown2 = unknown1 * 2.0f; // fp13
            var unknown3 = 1.0f / unknown2; // fp31

            for (var i = 0; i < 3; i++)
            {
                var unknown4 = (int)(ranges[i] * unknown3); // There was actually a subroutine call here, but this *should* be sufficient
                unknown4 = Math.Min(unknown4, 0x400000);
                var unknown5 = GetHighestBit((uint)unknown4);
                unknown5 = Math.Min(unknown5, 25);
                result[i] = unknown5;
            }

            return result;
        }

        /// <summary>
        /// Gets the index of the highest 1 bit in an integer.
        /// </summary>
        /// <param name="x">The integer.</param>
        /// <returns>The index of the highest 1 bit (31 = MSB, 0 = LSB), or 0 if none.</returns>
        private static int GetHighestBit(uint x)
        {
            var mask = 1U << 31;
            var bits = 31;
            while ((x & mask) == 0)
            {
                mask >>= 1;
                bits--;
            }
            if (bits < 0)
                return 0;
            return bits;
        }
    }
}
