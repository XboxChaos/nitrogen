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

namespace Nitrogen.Blob.Transport.BinaryTemplates.HaloReach
{
    /// <summary>
    /// Defines the structure of a base variant in Halo: Reach.
    /// </summary>
    internal class HaloReachBaseVariant
        : DataTemplate
    {
        protected override void Define()
        {
            Register<bool>();

            var sections = new Dictionary<string, Action>
            {
                { "General", DefineGeneralSettings },
                { "Respawn", DefineRespawnSettings },
                { "Social", DefineSocialSettings },
                { "MapOverrides", DefineMapOverrides },
                { "Powerups", DefinePowerups },
                { "Teams", DefineTeams },
                { "Loadouts", DefineLoadouts },
            };

            foreach (var section in sections)
                Group(section.Key, section.Value);
        }

        private void DefineGeneralSettings()
        {
            Register<bool>("TeamsEnabled");
            Register<bool>("RoundsResetPlayers");
            Register<bool>("RoundsResetEverything");
            Register<bool>();
            Register<byte>("RoundTimeLimit");
            Register<byte>("NumberOfRounds", n: 5);
            Register<byte>("EarlyVictoryWinCount", n: 4);
            Register<byte>("SuddenDeathDuration", n: 7);
            Register<byte>("GracePeriod", n: 5);
        }

        private void DefineRespawnSettings()
        {
            Register<bool>("SyncWithTeam");
            Register<bool>();
            Register<bool>();
            Register<bool>("RespawnOnKill");
            Register<byte>("Lives", n: 6);
            Register<byte>("SharedTeamLives", n: 7);
            Register<byte>("RespawnTime");
            Register<byte>("SuicidePenalty");
            Register<byte>("BetrayalPenalty");
            Register<byte>("RespawnTimeGrowth", n: 4);
            Register<byte>("InitialLoadoutDuration", n: 4);

            Group("RespawnTraits", () =>
            {
                Register<byte>("Duration", n: 6);
                Import<HaloReach.Shared.HaloReachTraitSet>();
            });
        }

        private void DefineSocialSettings()
        {
            Register<byte>("TeamChangingMode", n: 2);
            Register<bool>();
            Register<bool>();
            Register<bool>();
            Register<bool>();
            Register<bool>();
        }

        private void DefineMapOverrides()
        {
            Register<bool>("IndestructibleVehicles");
            Register<bool>("TurretsOnMap");
            Register<bool>("PowerupsOnMap");
            Register<bool>("ArmorAbilitiesOnMap");
            Register<bool>("ShortcutsOnMap");
            Register<bool>("GrenadesOnMap");
            Group("BasePlayerTraits", () => Import<HaloReach.Shared.HaloReachTraitSet>());
            Register<sbyte>("WeaponSet");
            Register<sbyte>("VehicleSet");
        }

        private void DefinePowerups()
        {
            Group("Red", () => Import<HaloReach.Shared.HaloReachTraitSet>());
            Group("Blue", () => Import<HaloReach.Shared.HaloReachTraitSet>());
            Group("Yellow", () => Import<HaloReach.Shared.HaloReachTraitSet>());

            Group("Red", () => Register<byte>("Duration", n: 7));
            Group("Blue", () => Register<byte>("Duration", n: 7));
            Group("Yellow", () => Register<byte>("Duration", n: 7));
        }

        private void DefineTeams()
        {
            throw new NotImplementedException();
        }

        private void DefineLoadouts()
        {
            throw new NotImplementedException();
        }
    }
}

