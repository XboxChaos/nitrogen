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

namespace Nitrogen.Content
{
    /// <summary>
    /// Binds a property with data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class PropertyBindingAttribute
        : Attribute
    {
        internal PropertyBindingAttribute() { }

        internal PropertyBindingAttribute(string path) : this(null, path) { }

        internal PropertyBindingAttribute(string chunk, string path) : this(chunk, path, false) { }

        internal PropertyBindingAttribute(string chunk, string path, bool plusOneEncoding)
        {
            Chunk = chunk;
            Path = path.Split('/');
            UsePlusOneEncoding = plusOneEncoding;
        }

        public string Chunk { get; internal set; }

        /// <summary>
        /// Gets an enumerable collection of strings representing a path to the desired property.
        /// </summary>
        public IEnumerable<string> Path { get; private set; }

        /// <summary>
        /// Gets a boolean value indicating whether +1 encoding should be used.
        /// </summary>
        public bool UsePlusOneEncoding { get; private set; }

        public string CountPropertyName { get; set; }

        public uint Count { get; set; }

        /// <summary>
        /// When bound to a list, gets or sets the prefix for each element.
        /// </summary>
        public string Prefix { get; set; }

        public string ValueName { get; set; }

        /// <summary>
        /// Gets a boolean value indicating whether the property is a list.
        /// </summary>
        public bool IsListBinding
        {
            get { return CountPropertyName != null || Count > 0; }
        }
    }
}
