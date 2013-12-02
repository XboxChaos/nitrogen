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
    public class MeterData
        : IScriptObject
    {
        private byte type;
        private IntegerReference numerator, denominator;
        private TimerReference timer;

        public MeterData()
        {
            this.numerator = new IntegerReference();
            this.denominator = new IntegerReference();
            this.timer = new TimerReference();
        }

        public enum MeterType
        {
            None,
            Fraction,
            Timer
        }

        public MeterType Type
        {
            get { return (MeterType)this.type; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(MeterType), value));
                this.type = (byte)value;
            }
        }

        public IntegerReference Numerator
        {
            get { return this.numerator; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.numerator = value;
            }
        }

        public IntegerReference Denominator
        {
            get { return this.denominator; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.denominator = value;
            }
        }

        public TimerReference Timer
        {
            get { return this.timer; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.timer = value;
            }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.type, 2);
            var meterType = (MeterType)this.type;
            switch (meterType)
            {
                case MeterType.Fraction:
                    s.Serialize(this.numerator);
                    s.Serialize(this.denominator);
                    break;

                case MeterType.Timer:
                    s.Serialize(this.timer);
                    break;
            }
        }
    }
}
