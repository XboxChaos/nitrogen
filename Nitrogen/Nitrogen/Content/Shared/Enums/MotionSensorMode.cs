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
    /// Specifies what the motion sensor picks up.
    /// </summary>
    public enum MotionSensorMode
    {
        /// <summary>
        /// The motion sensor mode will be inherited.
        /// </summary>
        Unchanged,

        /// <summary>
        /// The motion sensor is disabled.
        /// </summary>
        Off,

        /// <summary>
        /// The motion sensor picks up teammates only.
        /// </summary>
        AlliesOnly,

        /// <summary>
        /// The motion sensor picks up moving enemies.
        /// </summary>
        Normal,

        /// <summary>
        /// The motion sensor picks up all players regardless of whether they are moving.
        /// </summary>
        Enhanced,
    }
}
