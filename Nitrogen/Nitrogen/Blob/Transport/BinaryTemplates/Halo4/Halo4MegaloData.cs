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
using Nitrogen.Blob.Transport.BinaryTemplates.Shared.Megalo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Halo4
{
    /// <summary>
    /// Defines the Halo 4 Megalo variant data structure.
    /// </summary>
    internal class Halo4MegaloData
        : DataTemplate
    {
        protected override void Define()
        {
            var sections = new Dictionary<string, Action>
            {
                { "MegaloTraits", DefineMegaloTraits },
                { "MegaloSettings", DefineMegaloSettings },
                { "Strings", DefineStrings },
                { "Metadata", DefineMetadata },
                { "MapPermissions", DefineMapPermissions },
                { "PlayerRatingParams", DefinePlayerRatingParameters },
                { "GlobalGameOptions", DefineGlobalGameOptions },
                { "MapLoadouts", DefineMapLoadouts },
                { "MenuFlags", DefineMenuFlags },
                { "Conditions", DefineConditions },
                { "Actions", DefineActions },
                { "Triggers", DefineTriggers },
                { "CustomStats", DefineCustomStats },
                { "Variables", DefineVariables },
                { "Widgets", DefineHudWidgets },
                { "Events", DefineEvents },
                { "RequiredObjectTypes", DefineRequiredObjectTypes },
                { "ObjectFilters", DefineObjectFilters },
                { "CandySpawnerFilters", DefineCandySpawnersFilters },
            };

            foreach (var section in sections)
                Group(section.Key, section.Value);
        }

        protected override void Define(Context namedArgs)
        {
            base.Define(namedArgs);

            // Weapon Tuning
            if (namedArgs.ContainsKey("HasWeaponTuning"))
                Group("WeaponTuning", DefineWeaponTuning);
        }

        private void DefineMegaloTraits()
        {
            byte traitCount = Register<byte>("Count", n: 5);
            for (int i = 0; i < traitCount; i++)
            {
                Group("Trait[" + i + "]", () =>
                {
                    Register<byte>("NameStringIndex");
                    Register<byte>("DescriptionStringIndex");
                    Import<Halo4.Shared.Halo4TraitSet>();
                    Register<bool>("IsHidden");
                    Register<bool>("IsRuntime");
                });
            }
        }

        private void DefineMegaloSettings()
        {
            byte settingCount = Register<byte>("Count", n: 5);
            for (var i = 0; i < settingCount; i++)
            {
                Group("Setting[" + i + "]", () =>
                {
                    Register<byte>("NameStringIndex");
                    Register<byte>("DescriptionStringIndex");

                    bool isRangeValue = Register<bool>("IsRangeValue");
                    if (isRangeValue)
                        Register<ushort>("DefaultRangeValue", n: 10);
                    else
                        Register<byte>("DefaultValueIndex", n: 4);

                    Group("Values", () =>
                    {
                        byte valueCount = 2;
                        if (isRangeValue)
                        {
                            SetValue("ValueCount", valueCount);
                        }
                        else
                        {
                            valueCount = Register<byte>("Count", n: 5);
                        }

                        for (var j = 0; j < valueCount; j++)
                        {
                            Group("Value[" + j + "]", () =>
                            {
                                Register<short>("Value", n: 10);
                                if (!isRangeValue)
                                {
                                    Register<byte>("NameStringIndex");
                                    Register<byte>("DescriptionStringIndex");
                                }
                            });
                        }
                    });

                    if (isRangeValue)
                        Register<ushort>("SelectedRangeValue", n: 10);
                    else
                        Register<byte>("SelectedValueIndex", n: 4);
                });
            }
        }

        private void DefineStrings()
        {
            Group("MegaloStrings", () => Import<StringTable>(new Context
            {
                { "OffsetSize", 16 },
                { "LengthSize", 16 },
                { "CountSize", 8 },
            }));

            Register<byte>("BaseNameStringIndex");

            Group("NameString", () => Import<StringTable>(new Context
            {
                { "OffsetSize", 11 },
                { "LengthSize", 11 },
                { "CountSize", 1 },
            }));

            Group("Description", () => Import<StringTable>(new Context
            {
                { "OffsetSize", 13 },
                { "LengthSize", 13 },
                { "CountSize", 1 },
            }));

            Group("IntroDescription", () => Import<StringTable>(new Context
            {
                { "OffsetSize", 13 },
                { "LengthSize", 13 },
                { "CountSize", 1 },
            }));

            Group("CategoryName", () => Import<StringTable>(new Context
            {
                { "OffsetSize", 10 },
                { "LengthSize", 10 },
                { "CountSize", 1 },
            }));
        }

        private void DefineMetadata()
        {
            Register<byte>("GameVariantIconIndex", n: 5);
            Register<byte>("CategoryIndex", n: 5);
        }

        private void DefineMapPermissions()
        {
            int permissionCount = 0;
            Group("Maps", () =>
            {
                permissionCount = Register<byte>("Count", n: 6);
                if (permissionCount >= 0 && permissionCount < 32)
                {
                    for (var i = 0; i < permissionCount; i++)
                    {
                        Group("Map[" + i + "]", () =>
                        {
                            Register<ushort>("MapId");
                        });
                    }
                }
            });

            if (permissionCount >= 0 && permissionCount < 32)
                Register<bool>("IsExclusive");
        }

        private void DefinePlayerRatingParameters()
        {
            for (int i = 0; i < 15; i++)
            {
                Register<int>("Parameter[" + i + "]");
            }
            Register<bool>();
        }

        private void DefineGlobalGameOptions()
        {
            Register<short>("ScoreLimit");
            Register<bool>();
            Register<bool>();
            Register<bool>();
        }

        private void DefineMapLoadouts()
        {
            int mapLoadoutsCount = Register<int>("Count");
            for (var i = 0; i < mapLoadoutsCount; i++)
            {
                Group("Loadout[" + i + "]", () =>
                {
                    Register<byte>("GroupId", n: 2);
                    Import<Halo4.Shared.Halo4Loadout>();
                });
            }
        }

        private void DefineMenuFlags()
        {
            Group("DisabledFlags", () => Register<bool[]>(count: 26 * 32));
            Group("HiddenFlags", () => Register<bool[]>(count: 26 * 32));

            Group("MegaloSettings", () =>
            {
                Group("DisabledFlags", () => Register<bool[]>(count: 32));
                Group("HiddenFlags", () => Register<bool[]>(count: 32));
            });
        }

        private void DefineConditions()
        {
            ushort count = Register<ushort>("Count", n: 10);
            for (var i = 0; i < count; i++)
            {
                Group("Condition[" + i + "]", () =>
                {
                    byte opcode = Register<byte>("Opcode", n: 5);
                    if (opcode <= 0)
                        return;

                    Register<bool>("IsInverse");
                    Register<ushort>("UnionId", n: 10);
                    Register<ushort>("StartAction", n: 11);
                    Group("Parameters", () =>
                    {
                        var condition = GameRegistry.Halo4.MegaloConditionsDatabase.GetDefinition(opcode);
                        Import<DefinitionParameters>(new Context { { "Definition", condition } });
                    });
                });
            }
        }

        private void DefineActions()
        {
            ushort count = Register<ushort>("Count", n: 11);
            for (var i = 0; i < count; i++)
            {
                Group("Action[" + i + "]", () =>
                {
                    byte opcode = Register<byte>("Opcode");
                    if (opcode > 0)
                    {
                        Group("Parameters", () =>
                        {
                            var action = GameRegistry.Halo4.MegaloActionsDatabase.GetDefinition(opcode);
                            Import<DefinitionParameters>(new Context { { "Definition", action } });
                        });
                    }
                });
            }
        }

        private void DefineTriggers()
        {
            byte count = Register<byte>("Count");
            for (var i = 0; i < count; i++)
            {
                Group("Trigger[" + i + "]", () =>
                {
                    var enumType = Register<byte>("EnumType", n: 3);
                    Register<byte>("Type", n: 4);

                    switch (enumType)
                    {
                        case 5:
                            bool isFilterIndexNull = Register<bool>("IsFilterIndexNull");
                            if (!isFilterIndexNull)
                                Register<byte>("FilterIndex", n: 4);
                            break;

                        case 6:
                            Register<bool>("GameObjectType");

                            bool isGameObjectFilterIndexNull = Register<bool>("IsGameObjectFilterIndexNull");
                            if (!isGameObjectFilterIndexNull)
                                Register<byte>("GameObjectFilterIndex", n: 2);
                            break;
                    }

                    Register<uint>("ConditionIndex", n: 10);
                    Register<uint>("ConditionCount", n: 10);
                    Register<uint>("ActionIndex", n: 11);
                    Register<uint>("ActionCount", n: 11);
                    Register<byte>("FrameUpdateFrequency");
                    Register<byte>("FrameUpdateOffset");
                });
            }
        }

        private void DefineCustomStats()
        {
            byte count = Register<byte>("Count", n: 3);
            for (var i = 0; i < count; i++)
            {
                Group("Stat[" + i + "]", () =>
                {
                    Register<byte>("NameStringIndex");
                    Register<byte>("Format", n: 2);
                    Register<byte>("SortOrder", n: 2);
                    Register<bool>("Grouping");
                    Register<bool>(); // could be whether it shows up in the scoreboard at the bottom-right of the screen???
                    Register<bool>("DeterminesPlace");
                });
            }
        }

        private void DefineVariables()
        {
            Group("Global", () => Import<VariableTableData>(new Context
            {
                { "IntegerCountSize", 5 },
                { "TimerCountSize", 4 },
                { "TeamCountSize", 4 },
                { "PlayerCountSize", 4 },
                { "ObjectCountSize", 5 },
            }));

            Group("Player", () => Import<VariableTableData>(new Context
            {
                { "IntegerCountSize", 4 },
                { "TimerCountSize", 3 },
                { "TeamCountSize", 3 },
                { "PlayerCountSize", 3 },
                { "ObjectCountSize", 3 },
            }));

            Group("Object", () => Import<VariableTableData>(new Context
            {
                { "IntegerCountSize", 4 },
                { "TimerCountSize", 3 },
                { "TeamCountSize", 2 },
                { "PlayerCountSize", 3 },
                { "ObjectCountSize", 3 },
            }));

            Group("Team", () => Import<VariableTableData>(new Context
            {
                { "IntegerCountSize", 4 },
                { "TimerCountSize", 3 },
                { "TeamCountSize", 3 },
                { "PlayerCountSize", 3 },
                { "ObjectCountSize", 3 },
            }));
        }

        private void DefineHudWidgets()
        {
            byte count = Register<byte>("Count", n: 3);
            for (var i = 0; i < count; i++)
            {
                Group("Widget[" + i + "]", () => Register<byte>("Position", n: 4));
            }
        }

        private void DefineEvents()
        {
            var events = new[] {
                "OnInit",
                "OnLocalInit",
                "OnHostMigration",
                "OnDoubleHostMigration",
                "OnObjectDeath",
                "OnLocalTick",
                "OnPregame",
                "OnIncident",
            };

            foreach (var name in events)
            {
                if (Mode == SerializationMode.Deserialize)
                {
                    bool isNull = Convert.ToBoolean(Read(typeof(bool)));
                    if (!isNull)
                        SetValue(name, Read(typeof(byte), n: 7));
                    else
                        SetValue(name, null);
                }
                else
                {
                    var value = GetValue<object>(name);
                    Write(value == null);
                    if (value != null)
                        Write(value, n: 7);
                }
            }
        }

        private void DefineRequiredObjectTypes()
        {
            Register<bool[]>("Flags", count: 64 * 32);
        }

        private void DefineObjectFilters()
        {
            byte count = Register<byte>("Count", n: 5);
            for (var i = 0; i < count; i++)
            {
                Group("Filter[" + i + "]", () =>
                {
                    Register<byte>("NameStringIndex");

                    var flags = Register<byte>("Flags", n: 3);
                    if ((flags & 1) != 0)
                    {
                        bool isObjectTypeNull = Register<bool>("IsObjectTypeNull");
                        if (!isObjectTypeNull)
                            Register<ushort>("ObjectType", n: 11);
                    }
                    if ((flags & 2) != 0)
                        Register<byte>("TeamIndex", n: 4);
                    if ((flags & 4) != 0)
                        Register<short>("Numeric");

                    Register<byte>("MinimumCount", n: 7);
                });
            }
        }

        private void DefineCandySpawnersFilters()
        {
            Register<int>(n: 3);
            byte count = Register<byte>("Count", n: 3);
            for (var i = 0; i < count; i++)
            {
                Group("Filter[" + i + "]", () =>
                {
                    Register<byte>();
                    Register<byte>();
                    Register<byte>();
                    Register<byte>();
                    Register<bool>();
                });
            }
        }

        private void DefineWeaponTuning()
        {
            var twoBarrelsSet = new[] {
                "LightRifle",
                "Boltshot",
                "SpartanLaser",
                "Railgun",
                "PlasmaPistol",
                "AutoTurret",
            };

            var singleBarrelSet = new[] {
                "Suppressor",
                "Scattershot",
                "BinaryRifle",
                "IncinerationCannon",
                "Magnum",
                "AssaultRifle",
                "DMR",
                "Shotgun",
                "BattleRifle",
                "SniperRifle",
                "RocketLauncher",
                "StickyDetonator",
                "SAW",
                "Needler",
                "CovenantCarbine",
                "BeamRifle",
                "StormRifle",
                "ConcussionRifle",
                "FuelRodCannon",
                "GhostCannons",
                "WraithMortar",
                "WraithTurret",
                "BansheeBomb",
                "WarthogChaingun",
                "WarthogGauss",
                "WarthogRocket",
                "ScorpionCannon",
                "ScorpionChaingun",
                "MantisChaingun",
                "MantisRocket",
                "Flagnum",
                "FloodTalons",
            };

            // Barrel properties for dual barrel weapons
            for (int i = 0; i < twoBarrelsSet.Length; i++)
            {
                Group(twoBarrelsSet[i], () =>
                {
                    Group("PrimaryBarrel", () => Import<WeaponBarrelProperties>());
                    Group("SecondaryBarrel", () => Import<WeaponBarrelProperties>());
                });
            }

            // Barrel properties for single barrel weapons
            for (int i = 0; i < singleBarrelSet.Length; i++)
            {
                Group(singleBarrelSet[i], () => Group("PrimaryBarrel", () => Import<WeaponBarrelProperties>()));
            }

            // Weapon properties for single barrel weapons
            for (int i = 0; i < singleBarrelSet.Length; i++)
            {
                Group(singleBarrelSet[i], () => Import<WeaponProperties>());
            }

            // Weapon properties for dual barrel weapons
            for (int i = 0; i < twoBarrelsSet.Length; i++)
            {
                Group(twoBarrelsSet[i], () => Import<WeaponProperties>());
            }
        }
    }
}
