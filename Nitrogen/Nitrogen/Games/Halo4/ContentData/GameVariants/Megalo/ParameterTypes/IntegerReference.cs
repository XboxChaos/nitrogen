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

using Nitrogen.ContentData.GameVariants.Megalo;
using Nitrogen.IO;
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo.ParameterTypes
{
    public class IntegerReference
        : IScriptObject
    {
        private byte type;
        private short value;
        private byte index, id;

        public enum IntegerReferenceType
            : byte
        {
            Constant, // readonly
            PlayerMemberVariable, // var index
            ObjectMemberVariable, // var index
            TeamMemberVariable, // var index
            GlobalVariable, // var index
            ScratchVariable, // scratch index
            UserDefinedOption, // option index; readonly
            SpawnSequence, // readonly
            UserData, // readonly
            Unknown9, // readonly; halo 4 only
            TeamScore,
            PlayerScore,
            Unknown12, // requisition related; involves scenario data; readonly
            PlayerRating, // readonly
            PlayerStatistic, // stat index
            TeamStatistic, // stat index
            Unused16, // readonly; always returns zero
            CurrentRound, // readonly
            GetSymmetry, // bool; readonly
            Symmetry, // bool
            GameVariantControlsVictory, // bool
            ScoreLimit,
            TeamLivesRemaining,
            PlayerLivesRemaining,
            PlayerSpawnDelay,

            // all properties below are readonly ---
            Unknown25, // readonly; player
            Unknown26, // readonly; player
            Unknown27, // readonly; player
            Unknown28, // readonly; team
            Unknown29, // readonly; team
            Unknown30, // readonly; team
            Unknown31, // readonly; MP property index; object; used in dominion (dom_res_zone)
            Unknown32, // readonly; MP property index; object
            ScoreToWin,
            Unknown34, // bool
            TeamsEnabled, // bool
            RoundTimeLimit,
            NumberOfRounds,
            RoundsResetMap, // bool
            EarlyVictoryWinCount,
            LivesPerRound,
            TeamLivesPerRound,
            RespawnDuration,
            SuicidePenalty,
            BetrayalPenalty,
            RespawnGrowthTime,
            InitialLoadoutSelectionTime,
            RespawnTraitsDuration,
            FriendlyFire, // bool
            BetrayalBooting, // bool
            EnemyVoice, // bool
            OpenChannelVoice, // bool
            DeadPlayerVoice, // bool
            GrenadesOnMap, // bool
            IndestructibleVehicles,
            DamageBoostAlphaDuration,
            SpeedBoostAlphaDuration,
            OvershieldAlphaDuration,
            CustomPowerupAlphaDuration,
            DamageBoostBetaDuration,
            SpeedBoostBetaDuration,
            OvershieldBetaDuration,
            CustomPowerupBetaDuration,
            MapLoadoutsEnabled,
            InitialOrdnanceEnabled,
            RandomOrdnanceEnabled,
            ObjectiveOrdnanceEnabled,
            PersonalOrdnanceEnabled,
            OrdnanceSystemEnabled,
            KillCamEnabled,
            FinalKillCamEnabled,
            OvertimeDuration,
            DamageType,
        }

        public IntegerReferenceType ReferenceType
        {
            get { return (IntegerReferenceType)this.type; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(IntegerReferenceType), value));
                this.type = (byte)value;
            }
        }

        public short ConstantValue
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public byte Target
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public byte Index
        {
            get { return this.index; }
            set { this.index = value; }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.type, 7);
            IntegerReferenceType type = (IntegerReferenceType)this.type;
            switch (type)
            {
                case IntegerReferenceType.Constant:
                    s.Stream(ref this.value);
                    break;

                case IntegerReferenceType.PlayerMemberVariable:
                    s.Stream(ref this.id, 6);
                    s.Stream(ref this.index, 4);
                    break;

                case IntegerReferenceType.ObjectMemberVariable:
                case IntegerReferenceType.TeamMemberVariable:
                    s.Stream(ref this.id, 5);
                    s.Stream(ref this.index, 4);
                    break;

                case IntegerReferenceType.GlobalVariable:
                    s.Stream(ref this.index, 5);
                    break;

                case IntegerReferenceType.ScratchVariable:
                case IntegerReferenceType.UserDefinedOption:
                    s.Stream(ref this.index, 4);
                    break;

                case IntegerReferenceType.SpawnSequence:
                case IntegerReferenceType.UserData:
                case IntegerReferenceType.Unknown9:
                case IntegerReferenceType.Unknown31:
                case IntegerReferenceType.Unknown32:
                case IntegerReferenceType.TeamScore:
                case IntegerReferenceType.TeamLivesRemaining:
                case IntegerReferenceType.Unknown28:
                case IntegerReferenceType.Unknown29:
                case IntegerReferenceType.Unknown30:
                    s.Stream(ref this.id, 5);
                    break;

                case IntegerReferenceType.PlayerScore:
                case IntegerReferenceType.Unknown12:
                case IntegerReferenceType.PlayerRating:
                case IntegerReferenceType.PlayerLivesRemaining:
                case IntegerReferenceType.PlayerSpawnDelay:
                case IntegerReferenceType.Unknown25:
                case IntegerReferenceType.Unknown26:
                case IntegerReferenceType.Unknown27:
                    s.Stream(ref this.id, 6);
                    break;

                case IntegerReferenceType.PlayerStatistic:
                    s.Stream(ref this.id, 6);
                    s.Stream(ref this.index, 2);
                    break;

                case IntegerReferenceType.TeamStatistic:
                    s.Stream(ref this.id, 5);
                    s.Stream(ref this.index, 2);
                    break;
            }
        }
    }
}
