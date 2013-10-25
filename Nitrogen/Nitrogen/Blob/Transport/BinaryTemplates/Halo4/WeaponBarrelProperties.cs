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
    /// Defines the structure of barrel properties in the Halo 4 weapon tuning system.
    /// </summary>
    internal class WeaponBarrelProperties
        : DataTemplate
    {
        [Flags]
        private enum BarrelFlags : ulong
        {
            None,
            RoundsPerSecondLower = (1 << 0),
            RoundsPerSecondUpper = (1 << 1),
            SpinAccelerationTime = (1 << 2),
            SpinDecelerationTime = (1 << 3),
            ShotsPerFireLower = (1 << 4),
            ShotsPerFireUpper = (1 << 5),
            FireRecoveryTime = (1 << 6),
            SoftRecoveryTime = (1 << 7),
            BloomResetSpeed = (1 << 8),
            DamageErrorLower = (1 << 9),
            DamageErrorUpper = (1 << 10),
            BaseTurningSpeed = (1 << 11),
            DynamicTurningSpeedMin = (1 << 12),
            DynamicTurningSpeedMax = (1 << 13),
            ErrorAngle = (1 << 14),
            BloomBase = (1 << 15),
            BloomIncreaseRate = (1 << 16),
            HeatGeneratedPerRound = (1 << 17),
            ChargePercentageSubtraction = (1 << 18),
            AreaOfEffectRadius = (1 << 19),
            DamageLower = (1 << 20),
            DamageUpperMin = (1 << 21),
            DamageUpperMax = (1 << 22),
            ProjectileDamageRangeLower = (1 << 23),
            ProjectileDamageRangeUpper = (1 << 24),
            BuckshotAccuracy = (1 << 25),
            BuckshotSpread = (1 << 26)
        }

        protected override void Define()
        {
            var flags = (BarrelFlags)Register<ulong>("Flags");

            if (flags.HasFlag(BarrelFlags.RoundsPerSecondLower)) Register<short>("RoundsPerSecondLower");
            if (flags.HasFlag(BarrelFlags.RoundsPerSecondUpper)) Register<short>("RoundsPerSecondUpper");
            if (flags.HasFlag(BarrelFlags.SpinAccelerationTime)) Register<short>("SpinAccelerationTime");
            if (flags.HasFlag(BarrelFlags.SpinDecelerationTime)) Register<short>("SpinDecelerationTime");
            if (flags.HasFlag(BarrelFlags.ShotsPerFireLower)) Register<byte>("ShotsPerFireLower", n: 7);
            if (flags.HasFlag(BarrelFlags.ShotsPerFireUpper)) Register<byte>("ShotsPerFireUpper", n: 7);
            if (flags.HasFlag(BarrelFlags.FireRecoveryTime)) Register<short>("FireRecoveryTime");
            if (flags.HasFlag(BarrelFlags.SoftRecoveryTime)) Register<short>("SoftRecoveryTime");
            if (flags.HasFlag(BarrelFlags.BloomResetSpeed)) Register<short>("BloomResetSpeed");
            if (flags.HasFlag(BarrelFlags.DamageErrorLower)) Register<short>("DamageErrorLower");
            if (flags.HasFlag(BarrelFlags.DamageErrorUpper)) Register<short>("DamageErrorUpper");
            if (flags.HasFlag(BarrelFlags.BaseTurningSpeed)) Register<float>("BaseTurningSpeed", n: 20, minValue: 0, maxValue: 130, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.DynamicTurningSpeedMin)) Register<float>("DynamicTurningSpeedMin", n: 20, minValue: 0, maxValue: 130, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.DynamicTurningSpeedMax)) Register<float>("DynamicTurningSpeedMax", n: 20, minValue: 0, maxValue: 10, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.ErrorAngle)) Register<float>("ErrorAngle", n: 20, minValue: 0, maxValue: 6.2831855f, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.BloomBase)) Register<float>("BloomBase", n: 20, minValue: 0, maxValue: 6.2831855f, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.BloomIncreaseRate)) Register<float>("BloomIncreaseRate", n: 20, minValue: 0, maxValue: 6.2831855f, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.HeatGeneratedPerRound)) Register<short>("HeatGeneratedPerRound");
            if (flags.HasFlag(BarrelFlags.ChargePercentageSubtraction)) Register<short>("ChargePercentageSubtraction");
            if (flags.HasFlag(BarrelFlags.AreaOfEffectRadius)) Register<float>("AreaOfEffectRadius", n: 20, minValue: 0, maxValue: 6.2831855f, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.DamageLower)) Register<float>("DamageLower", n: 20, minValue: 0, maxValue: 3000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.DamageUpperMin)) Register<float>("DamageUpperMin", n: 20, minValue: 0, maxValue: 3000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.DamageUpperMax)) Register<float>("DamageUpperMax", n: 20, minValue: 0, maxValue: 3000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.ProjectileDamageRangeLower)) Register<float>("ProjectileDamageRangeLower", n: 20, minValue: 0, maxValue: 1000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.ProjectileDamageRangeUpper)) Register<float>("ProjectileDamageRangeUpper", n: 20, minValue: 0, maxValue: 1000, isSigned: true, roundFloat: false, flag: true);
            if (flags.HasFlag(BarrelFlags.BuckshotAccuracy)) Register<short>("BuckshotAccuracy");
            if (flags.HasFlag(BarrelFlags.BuckshotSpread)) Register<short>("BuckshotSpread");
        }
    }
}
