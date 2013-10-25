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
    /// Defines the structure of weapon properties in the Halo 4 weapon tuning system.
    /// </summary>
    internal class WeaponProperties
        : DataTemplate
    {
        [Flags]
        private enum WeaponFlags : ulong
        {
            None,
            HeatRecoveryThreshold = (1 << 0),
            OverheatThreshold = (1 << 1),
            HeatLossPerSecond = (1 << 2),
            HeatIllumination = (1 << 3),
            OverheatedHeatLossPerSecond = (1 << 4),
            AutoAimAngle = (1 << 5),
            AutoAimLong = (1 << 6),
            AutoAimShort = (1 << 7),
            AutoAimMinimum = (1 << 8),
            MagnetismAngle = (1 << 9),
            MagnetismLong = (1 << 10),
            MagnetismShort = (1 << 11),
            MagnetismMinimum = (1 << 12),
            DeviationAngle = (1 << 13)
        }

        protected override void Define()
        {
            var flags = (WeaponFlags)Register<ulong>("Flags");

            if (flags.HasFlag(WeaponFlags.HeatRecoveryThreshold)) Register<float>("HeatRecoveryThreshold", n: 20, minValue: 0, maxValue: 1, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.OverheatThreshold)) Register<float>("OverheatThreshold", n: 20, minValue: 0, maxValue: 1, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.HeatLossPerSecond)) Register<float>("HeatLossPerSecond", n: 20, minValue: 0, maxValue: 1, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.HeatIllumination)) Register<float>("HeatIllumination", n: 20, minValue: 0, maxValue: 1, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.OverheatedHeatLossPerSecond)) Register<float>("OverheatedHeatLossPerSecond", n: 20, minValue: 0, maxValue: 1, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.AutoAimAngle)) Register<float>("AutoAimAngle", n: 20, minValue: 0, maxValue: 1.5706964f, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.AutoAimShort)) Register<float>("AutoAimShort", n: 20, minValue: 0, maxValue: 1000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.AutoAimMinimum)) Register<float>("AutoAimMinimum", n: 20, minValue: 0, maxValue: 1000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.MagnetismAngle)) Register<float>("MagnetismAngle", n: 20, minValue: 0, maxValue: 1.5706964f, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.MagnetismShort)) Register<float>("MagnetismShort", n: 20, minValue: 0, maxValue: 1000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.MagnetismLong)) Register<float>("MagnetismLong", n: 20, minValue: 0, maxValue: 1000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.MagnetismMinimum)) Register<float>("MagnetismMinimum", n: 20, minValue: 0, maxValue: 1000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(WeaponFlags.DeviationAngle)) Register<float>("DeviationAngle", n: 20, minValue: 0, maxValue: 6.2831855f, isSigned: true, roundFloat: false, flag: true);
        }
    }
}
