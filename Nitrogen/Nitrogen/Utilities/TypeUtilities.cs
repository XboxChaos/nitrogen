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

namespace Nitrogen.Utilities
{
    public static class TypeUtilities
    {
        /// <summary>
        /// Converts an object to a type, taking nullable types into account.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <param name="type">The type to convert it to.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="InvalidCastException">
        /// Thrown if the object cannot be converted to the requested type.
        /// </exception>
        public static object NullableChangeType(object obj, Type type)
        {
            var targetUnderlyingType = Nullable.GetUnderlyingType(type);
            if ((targetUnderlyingType != null || !type.IsValueType) && obj == null)
                return null;

            var srcUnderlyingType = Nullable.GetUnderlyingType(type);
            if (srcUnderlyingType != null)
                obj = Convert.ChangeType(obj, srcUnderlyingType);

            if (targetUnderlyingType != null)
                return Convert.ChangeType(obj, targetUnderlyingType);

            return Convert.ChangeType(obj, type);
        }
    }
}
