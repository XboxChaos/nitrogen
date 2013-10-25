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
    /// Specifies the team changing behavior.
    /// </summary>
    public enum TeamChangingMode
    {
        /// <summary>
        /// Sorry buddy, but you're stuck with your bad teammates.
        /// </summary>
        Disabled,

        /// <summary>
        /// Team changing is allowed.
        /// </summary>
        Enabled,

        /// <summary>
        /// Team changing is allowed only if teams are unbalanced.
        /// </summary>
        BalancingOnly,
    }
}
