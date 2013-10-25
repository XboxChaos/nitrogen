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

namespace Nitrogen.Content.Shared.Enums
{
    /// <summary>
    /// Specifies the type of an operation.
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// +
        /// </summary>
        Add,

        /// <summary>
        /// -
        /// </summary>
        Subtract,

        /// <summary>
        /// ×
        /// </summary>
        Multiply,

        /// <summary>
        /// ÷
        /// </summary>
        Divide,

        /// <summary>
        /// Assignment
        /// </summary>
        Set,

        /// <summary>
        /// %
        /// </summary>
        Modulus,

        /// <summary>
        /// Logical AND.
        /// </summary>
        And,

        /// <summary>
        /// Logical OR.
        /// </summary>
        Or,

        /// <summary>
        /// Logical XOR
        /// </summary>
        Xor,

        /// <summary>
        /// Logical NOT
        /// </summary>
        Not,

        /// <summary>
        /// Bit shift left
        /// </summary>
        Shl,

        /// <summary>
        /// Bit shift right
        /// </summary>
        Shr,

        /// <summary>
        /// Same as <see cref="Shl"/>; I don't know why this exists.
        /// </summary>
        Shl2
    }
}
