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

using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.BaseVariant
{
    /// <summary>
    /// Represents a set of ordnance settings in a Halo 4 multiplayer variant.
    /// </summary>
    public class OrdnanceSettings
        : ISerializable<BitStream>
    {
        private bool initial, random, objective, personal, ordnanceSystemEnabled;
        private sbyte unk0; // enum; -1 disables initial ordnace
        private short infinityMinTimer, infinityMaxTimer;
        private short unk1, unk2; // timer
        private string initialDropFilter, randomDropSet, personalDropSet, substitutionSet;
        private short initialDropDelay;
        private bool overridePersonalOrdnance;
        private float pointReq, pointIncrement;
        private OrdnanceSlot[] personalOrdnance;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdnanceSettings"/> class with default values.
        /// </summary>
        public OrdnanceSettings()
        {
            this.initial = true;
            this.random = true;
            this.personal = true;
            this.ordnanceSystemEnabled = true;
            this.infinityMaxTimer = 100;
            this.infinityMinTimer = 90;
            this.initialDropFilter = "initial_drop";
            this.pointIncrement = 0.3f;
            this.pointReq = 70.0f;
            this.randomDropSet = "?";
            this.substitutionSet = "";
            this.personalDropSet = "?";
            this.unk0 = 50;
            this.unk2 = 5;

            this.personalOrdnance = new OrdnanceSlot[4];
            for (int i = 0; i < 4; i++)
                this.personalOrdnance[i] = new OrdnanceSlot();
        }

        /// <summary>
        /// Gets or sets the personal ordnance slots which will override the default slots.
        /// </summary>
        public OrdnanceSlot[] PersonalOrdnanceSlots
        {
            get { return this.personalOrdnance; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(value.Length != 4);

                this.personalOrdnance = value;
                for (int i = 0; i < 4; i++)
                {
                    if (this.personalOrdnance[i] == null)
                        this.personalOrdnance[i] = new OrdnanceSlot();
                }
            }
        }

        /// <summary>
        /// Gets or sets the top personal ordnance slot override.
        /// </summary>
        public OrdnanceSlot PersonalOrdnanceTopSlot
        {
            get { return PersonalOrdnanceSlots[0]; }
            set { PersonalOrdnanceSlots[0] = value; }
        }

        /// <summary>
        /// Gets or sets the left personal ordnance slot override.
        /// </summary>
        public OrdnanceSlot PersonalOrdnanceLeftSlot
        {
            get { return PersonalOrdnanceSlots[1]; }
            set { PersonalOrdnanceSlots[1] = value; }
        }

        /// <summary>
        /// Gets or sets the bottom (middle) personal ordnance slot override.
        /// </summary>
        public OrdnanceSlot PersonalOrdnanceBottomSlot
        {
            get { return PersonalOrdnanceSlots[2]; }
            set { PersonalOrdnanceSlots[2] = value; }
        }

        /// <summary>
        /// Gets or sets the right personal ordnance slot override.
        /// </summary>
        public OrdnanceSlot PersonalOrdnanceRightSlot
        {
            get { return PersonalOrdnanceSlots[3]; }
            set { PersonalOrdnanceSlots[3] = value; }
        }

        /// <summary>
        /// Gets or sets whether personal ordnance slots will be overridden.
        /// </summary>
        public bool CustomizePersonalOrdnance
        {
            get { return this.overridePersonalOrdnance; }
            set { this.overridePersonalOrdnance = value; }
        }

        /// <summary>
        /// Gets or sets the points requirement for calling upon a personal ordnance.
        /// </summary>
        public float PersonalOrdnancePointRequirement
        {
            get { return this.pointReq; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0 && value <= 10000);
                this.pointReq = value;
            }
        }

        /// <summary>
        /// Gets or sets the percentage that will be added to the point requirement for the next
        /// ordnance after calling upon a personal ordnance.
        /// </summary>
        public float PersonalOrdnancePointIncrement
        {
            get { return this.pointIncrement; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value >= 0 && value <= 10000);
                this.pointIncrement = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration in seconds before initial ordnance drops at the beginning
        /// of each round.
        /// </summary>
        public short InitialDropDelay
        {
            get { return this.initialDropDelay; }
            set { this.initialDropDelay = value; }
        }

        /// <summary>
        /// Gets or sets the object filter label of initial drop points.
        /// </summary>
        public string InitialDropLabel
        {
            get { return this.initialDropFilter; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(Encoding.ASCII.GetByteCount(value) <= 32);

                this.initialDropFilter = value;
            }
        }

        /// <summary>
        /// Gets or sets the random ordnance drop set.
        /// </summary>
        public string RandomDropSet
        {
            get { return this.randomDropSet; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(Encoding.ASCII.GetByteCount(value) <= 32);

                this.randomDropSet = value;
            }
        }

        /// <summary>
        /// Gets or sets the personal ordnance drop set.
        /// </summary>
        public string PersonalDropSet
        {
            get { return this.personalDropSet; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(Encoding.ASCII.GetByteCount(value) <= 32);

                this.personalDropSet = value;
            }
        }

        /// <summary>
        /// Gets or sets the global ordnance substitution set.
        /// </summary>
        public string GlobalSubstitutionSet
        {
            get { return this.substitutionSet; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                Contract.Requires<ArgumentException>(Encoding.ASCII.GetByteCount(value) <= 32);

                this.substitutionSet = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum time in seconds before the next random ordnance drops.
        /// </summary>
        public short RandomOrdnanceMinTimer
        {
            get { return this.infinityMinTimer; }
            set { this.infinityMinTimer = value; }
        }

        /// <summary>
        /// Gets or sets the maximum time in seconds before the next random ordnance drops.
        /// </summary>
        public short RandomOrdnanceMaxTimer
        {
            get { return this.infinityMaxTimer; }
            set { this.infinityMaxTimer = value; }
        }

        /// <summary>
        /// Gets or sets whether initial ordnance is enabled.
        /// </summary>
        public bool InitialOrdnanceEnabled
        {
            get { return this.initial; }
            set { this.initial = value; }
        }

        /// <summary>
        /// Gets or sets whether random ordnance is enabled.
        /// </summary>
        public bool RandomOrdnanceEnabled
        {
            get { return this.random; }
            set { this.random = value; }
        }

        /// <summary>
        /// Gets or sets whether objective ordnance is enabled.
        /// </summary>
        public bool ObjectiveOrdnanceEnabled
        {
            get { return this.objective; }
            set { this.objective = value; }
        }

        /// <summary>
        /// Gets or sets whether personal ordnance is enabled.
        /// </summary>
        public bool PersonalOrdnanceEnabled
        {
            get { return this.personal; }
            set { this.personal = value; }
        }

        /// <summary>
        /// Gets or sets whether the ordnance system is enabled.
        /// </summary>
        public bool OrdnanceSystemEnabled
        {
            get { return this.ordnanceSystemEnabled; }
            set { this.ordnanceSystemEnabled = value; }
        }

        #region ISerializable<BitStream>

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.initial);
            s.Stream(ref this.random);
            s.Stream(ref this.objective);
            s.Stream(ref this.personal);
            s.Stream(ref this.ordnanceSystemEnabled);
            s.Stream(ref this.unk0); 
            s.Stream(ref this.infinityMinTimer);
            s.Stream(ref this.infinityMaxTimer);
            s.Stream(ref this.unk1);
            s.StreamNullTerminatedString(ref this.initialDropFilter, Encoding.ASCII, 32);
            s.Stream(ref this.unk2);
            s.Stream(ref this.initialDropDelay);
            s.StreamNullTerminatedString(ref this.randomDropSet, Encoding.ASCII, 32);
            s.StreamNullTerminatedString(ref this.personalDropSet, Encoding.ASCII, 32);
            s.StreamNullTerminatedString(ref this.substitutionSet, Encoding.ASCII, 32);
            s.Stream(ref this.overridePersonalOrdnance);
            s.Serialize(this.personalOrdnance, 0, 4);
            s.Stream(ref this.pointReq, bits: 30, min: 0, max: 10000, signed: false, rounded: true, flag: false);
            s.Stream(ref this.pointIncrement, bits: 30, min: 0, max: 10000, signed: false, rounded: true, flag: false);
        }

        #endregion
    }
}