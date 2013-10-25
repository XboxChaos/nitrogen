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

using Nitrogen.Blob.Transport.BinaryTemplates.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Halo4
{
    /// <summary>
    /// Defines the Halo 4 base variant structure.
    /// </summary>
    internal class Halo4BaseVariant
        : DataTemplate
    {
        private Context context;

        protected override void Define(Context namedArgs)
        {
            this.context = namedArgs;
            Define();
        }

        protected override void Define()
        {
            Register<bool>();
            Register<bool>();

            var sections = new Dictionary<string, Action>
            {
                { "General", DefineGeneralSettings },
                { "Prototype", DefinePrototypeOptions },
                { "Respawn", DefineRespawnSettings },
                { "Social", DefineSocialSettings },
                { "MapOverrides", DefineMapOverrides },
                { "Powerups", DefinePowerups },
                { "Requisition", DefineRequisitionSettings },
                { "Teams", DefineTeams },
                { "Loadouts", DefineLoadouts },
                { "Ordnance", DefineOrdnanceSettings },
            };

            foreach (var section in sections)
                Group(section.Key, section.Value);
        }

        private void DefineGeneralSettings()
        {
            Register<bool>("TeamsEnabled");
            Register<bool>("RoundsResetPlayers");
            Register<bool>("RoundsResetMap");
            Register<bool>("RoundsResetEverythingElse");
            Register<byte>("RoundTimeLimit");
            Register<uint>("NumberOfRounds", n: 5);
            Register<uint>("EarlyVictoryWinCount", n: 4);
            Register<bool>("DeathCamEnabled");
            Register<bool>("PointsSystemEnabled");
            Register<bool>("FinalKillCamEnabled");
            Register<byte>("SuddenDeathDuration");
            Register<bool>();
            Register<bool>();
            Register<byte>("MoshDifficulty", n: 2);
        }

        private void DefinePrototypeOptions()
        {
            this.context["HasWeaponTuning"] = (Register<byte>("Mode", n: 2) == 1);
            Register<byte>("PrometheanEnergyKill", n: 3);
            Register<byte>("PrometheanEnergyTime", n: 3);
            Register<byte>("PrometheanEnergyMedal", n: 3);
            Register<byte>("PrometheanDuration", n: 4);
            Register<bool>();
        }

        private void DefineRespawnSettings()
        {
            Register<bool>("SyncWithTeam");
            Register<bool>("RespawnAtTeammate"); // doesn't seem to work in H4
            Register<bool>("RespawnInPlace"); // doesn't seem to work in H4
            Register<bool>("RespawnOnKill");
            Register<bool>("DualRespawnTiming");
            Register<byte>("Lives", n: 6);
            Register<byte>("SharedTeamLives", n: 7);
            Register<byte>("RespawnTime");
            Register<byte>("SecondaryRespawnTime");
            Register<byte>("SuicidePenalty");
            Register<byte>("BetrayalPenalty");
            Register<byte>("RespawnTimeGrowth", n: 4);
            Register<byte>("InitialLoadoutDuration", n: 4); // broken in H4; the lowest you can go is 7.

            Group("RespawnTraits", () =>
            {
                Register<byte>("Duration", n: 6);
                Import<Halo4.Shared.Halo4TraitSet>();
            });
        }

        private void DefineSocialSettings()
        {
            Register<bool>("ObserverTeamEnabled");
            Register<byte>("TeamChangingMode", n: 2);
            Register<bool>("FriendlyFireEnabled");
            Register<bool>("BetrayalBootingEnabled");
            Register<bool>("EnemyVoice");
            Register<bool>("OpenChannelVoice");
            Register<bool>("DeadPlayerVoice");
        }

        private void DefineMapOverrides()
        {
            Register<bool>("IndestructibleVehicles");
            Register<bool>("TurretsOnMap");
            Register<bool>("PowerupsOnMap");
            Register<bool>("ArmorAbilitiesOnMap");
            Register<bool>("ShortcutsOnMap");
            Register<bool>("GrenadesOnMap");
            Group("BasePlayerTraits", () => Import<Halo4.Shared.Halo4TraitSet>());
            Register<sbyte>("WeaponSet");
            Register<sbyte>("VehicleSet");
            Register<sbyte>("ArmorAbilitySet");
        }

        private void DefinePowerups()
        {
            // Extracted from Map Overrides for simplicity.

            Action<string> powerup = (string name) =>
            {
                Group(name, () =>
                {
                    Group("AlphaPhaseTraits", () => {
                        Import<Halo4.Shared.Halo4TraitSet>();
                        Register<byte>("Duration", n: 7);
                    });
                    Group("BetaPhaseTraits", () =>
                    {
                        Import<Halo4.Shared.Halo4TraitSet>();
                        Register<byte>("Duration", n: 7);
                    });
                });
            };

            powerup("DamageBoost");
            powerup("SpeedBoost");
            powerup("Overshield");
            powerup("Custom");
        }

        private void DefineRequisitionSettings()
        {
            Register<float>();
            Register<int>();
            var count = Register<byte>("Count", n: 7);
            Group("Settings", () =>
            {
                for (int i = 0; i < count; i++)
                {
                    Group("Settings[" + i + "]", () =>
                    {
                        Register<byte>("PaletteIndex");
                        Register<bool>();
                        Register<int>();
                        Register<int>("MaxInstances", n: 30);
                        Register<float>();
                        Register<int>("ModelVariantNameStringIndex", n: 30);
                        Register<int>("InitialAmmo");
                        Register<float>();
                        Register<float>();
                        Register<float>();
                        Register<float>();
                        Register<byte>("MaxBuyPlayer");
                        Register<byte>("MaxBuyTeam");
                    });
                }
            });
            Register<int>();
        }

        private void DefineTeams()
        {
            Register<byte>("TeamModelOverride", n: 3);
            Register<byte>("DesignatorSwitchType", n: 2);

            for (int i = 0; i < 8; i++)
            {
                Group("Team[" + i + "]", () =>
                {
                    Register<bool>("OverrideEmblem");
                    Register<bool>("OverrideUIColor");
                    Register<bool>("OverrideTextColor");
                    Register<bool>("OverridePrimaryColor");
                    Register<bool>("OverrideSecondaryColor");
                    Register<bool>("IsEnabled");

                    Group("Name", () => Import<StringTable>(new Context
                    {
                        { "OffsetSize", 10 },
                        { "LengthSize", 10 },
                        { "CountSize", 1 },
                    }));

                    Register<uint>("Index", n: 4);
                    Register<bool>("OverrideTeamModel");

                    Group("PrimaryColor", () =>
                    {
                        Register<byte>("Alpha");
                        Register<byte>("Red");
                        Register<byte>("Green");
                        Register<byte>("Blue");
                    });

                    Group("SecondaryColor", () =>
                    {
                        Register<byte>("Alpha");
                        Register<byte>("Red");
                        Register<byte>("Green");
                        Register<byte>("Blue");
                    });

                    Group("TextColor", () =>
                    {
                        Register<byte>("Alpha");
                        Register<byte>("Red");
                        Register<byte>("Green");
                        Register<byte>("Blue");
                    });

                    Group("UIColor", () =>
                    {
                        Register<byte>("Alpha");
                        Register<byte>("Red");
                        Register<byte>("Green");
                        Register<byte>("Blue");
                    });

                    Register<uint>("FireTeamCount", n: 5);

                    Group("Emblem", () =>
                    {
                        Register<byte>("ForegroundIndex");
                        Register<byte>("BackgroundIndex");
                        Register<bool>();
                        Register<bool>("BackgroundToggle");
                        Register<bool>("ForegroundToggle");
                        Register<uint>("PrimaryColorIndex", n: 6);
                        Register<uint>("SecondaryColorIndex", n: 6);
                        Register<uint>("BackgroundColorIndex", n: 6);
                    });
                });
            }
        }

        private void DefineLoadouts()
        {
            Register<bool>("PersonalLoadoutsEnabled");
            Register<bool>();
            Register<bool>();
            Register<bool>("MapLoadoutsEnabled");

            for (int i = 0; i < 6; i++)
            {
                Group("Palette[" + i + "]", () =>
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Group("Loadout" + j, () => Import<Halo4.Shared.Halo4Loadout>());
                    }
                });
            }
        }

        private void DefineOrdnanceSettings()
        {
            Register<bool>("InitialOrdnanceEnabled");
            Register<bool>("RandomOrdnanceEnabled");
            Register<bool>("ObjectiveOrdnanceEnabled");
            Group("Personal", () => Register<bool>("IsEnabled"));
            Register<bool>("OrdnanceSystemEnabled");
            Register<sbyte>(); // timer or distance? -2 in forge like the other unknown int16's. -1 disables initial ordnance
            Register<short>("RandomOrdnanceMinTimer");
            Register<short>("RandomOrdnanceMaxTimer");
            Register<short>(); // timer
            Register<string>("InitialDropObjectFilterName", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);
            Register<short>(); // timer
            Register<short>("InitialDropDelay");
            Register<string>("RandomDropSet", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);
            Group("Personal", () => Register<string>("DropSet", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true));
            Register<string>("SubstitutionSet", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);

            Group("Personal", () =>
            {
                Register<bool>("IsCustomized");

                for (int i = 0; i < 4; i++)
                {
                    string slotName = i.ToString();
                    switch (i)
                    {
                        case 0: slotName = "Top"; break;
                        case 1: slotName = "Left"; break;
                        case 2: slotName = "Middle"; break;
                        case 3: slotName = "Right"; break;
                    }

                    Group(slotName, () =>
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Group("Item[" + j + "]", () =>
                            {
                                Register<string>("Item", encoding: Encoding.ASCII, length: 32, padded: false, nullTerminated: true);
                                Register<float>("Weight", n: 30, minValue: 0, maxValue: 10000, isSigned: false, flag: true);
                            });
                        }
                    });
                }

                Register<float>("PointRequirement", n: 30, minValue: 0, maxValue: 10000, isSigned: false, flag: true);
                Register<float>("PointIncreaseMultiplier", n: 30, minValue: 0, maxValue: 10000, isSigned: false, flag: true);
            });
        }
    }
}
