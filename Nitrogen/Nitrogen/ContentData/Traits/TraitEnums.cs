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

namespace Nitrogen.ContentData.Traits
{
    /// <summary>
    /// Specifies a toggleable value which can be inherited.
    /// </summary>
    public enum InheritableToggle
        : byte
    {
        /// <summary>
        /// This trait will be inherited.
        /// </summary>
        Inherit,

        /// <summary>
        /// This trait is disabled.
        /// </summary>
        Disabled,
        
        /// <summary>
        /// This trait is enabled.
        /// </summary>
        Enabled,
    }

    /// <summary>
    /// Specifies the behavior of Active Camo for a player.
    /// </summary>
    public enum ActiveCamoMode
    {
        /// <summary>
        /// The value will be inherited.
        /// </summary>
        Unchanged,

        /// <summary>
        /// Active Camo is disabled.
        /// </summary>
        Disabled,

        /// <summary>
        /// Active Camo is enabled.
        /// </summary>
        Enabled,

        /// <summary>
        /// Only Grunts will be fooled.
        /// </summary>
        Poor,

        /// <summary>
        /// This might fool an Elite.
        /// </summary>
        Good,

        /// <summary>
        /// This might fool other players.
        /// </summary>
        /// <remarks>
        /// In Halo 4, this isn't as good as it used to be since one of the Title Updates nerfed
        /// the Active Camo.
        /// </remarks>
        Best,
    }

    /// <summary>
    /// Specifies the visibility of a HUD element.
    /// </summary>
    public enum HudVisibility
    {
        /// <summary>
        /// Visibility will be inherited.
        /// </summary>
        Unchanged,

        /// <summary>
        /// This HUD element is not visible to anyone.
        /// </summary>
        None,

        /// <summary>
        /// This HUD element is visible to teammates.
        /// </summary>
        VisibleToAllies,

        /// <summary>
        /// This HUD element is visible to all players.
        /// </summary>
        VisibleToEveryone,
    }

    public enum InfiniteAmmoMode
    {
        /// <summary>
        /// This setting will be inherited.
        /// </summary>
        Unchanged,

        /// <summary>
        /// Infinite ammo is disabled.
        /// </summary>
        Disabled,

        /// <summary>
        /// Infinite ammo is enabled.
        /// </summary>
        Enabled,

        /// <summary>
        /// Shoot to your heart's content!
        /// </summary>
        BottomlessClip,
    }

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

    /// <summary>
    /// Specifies the vehicle access permission.
    /// </summary>
    public enum VehicleUsageMode
    {
        /// <summary>
        /// Vehicle usage will be inherited.
        /// </summary>
        Unchanged,

        /// <summary>
        /// Vehicles cannot be accessed.
        /// </summary>
        None,

        /// <summary>
        /// Only the driver seat is accessible.
        /// </summary>
        DriverOnly,

        /// <summary>
        /// Only the gunner is accessible.
        /// </summary>
        GunnerOnly,

        /// <summary>
        /// Only the passenger seat is accessible.
        /// </summary>
        PassengerOnly,

        /// <summary>
        /// Every seat of a vehicle is accessible.
        /// </summary>
        FullUse,
    }
}
