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
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo
{
    public class UserDefinedOption
        : ISerializable<BitStream>
    {
        private byte nameStringIndex, descStringIndex;
        private bool isRange;
        private ushort defaultRangeValue, selectedRangeValue;
        private byte defaultValueIndex, selectedValueIndex;
        private List<IUserDefinedOptionValue> values;

        public UserDefinedOption()
        {
            this.values = new List<IUserDefinedOptionValue>();
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.nameStringIndex);
            s.Stream(ref this.descStringIndex);
            s.Stream(ref this.isRange);

            int count;
            if (this.isRange)
            {
                s.Stream(ref this.defaultRangeValue, 10);
                count = 2;
            }
            else
            {
                s.Stream(ref this.defaultValueIndex, 4);
                count = this.values.Count;
                s.Stream(ref count, 5);
            }

            for (int i = 0; i < count; i++)
            {
                if (s.State == StreamState.Read)
                {
                    if (this.isRange)
                        this.values.Add(new UserDefinedOptionRangeEndpoint());
                    else
                        this.values.Add(new UserDefinedOptionValue());
                }

                s.Serialize(this.values[i]);
            }

            if (this.isRange)
                s.Stream(ref this.selectedRangeValue, 10);
            else
                s.Stream(ref this.selectedValueIndex, 4);
        }

        #endregion
    }
}
