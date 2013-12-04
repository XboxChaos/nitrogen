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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo.ParameterTypes
{
    public class StringReference
        : IParameter
    {
        private sbyte stringIndex;
        private List<StringToken> tokens;

        public StringReference()
        {
            this.tokens = new List<StringToken>();
        }

        public virtual int MaxTokens { get { return 0; } }

        public void Serialize(BitStream s)
        {
            s.StreamPlusOne(ref this.stringIndex);
 
            int bits = MaxTokens == 1 ? 1 : 2; // temp workaround
            int tokenCount = this.tokens.Count;
            s.Stream(ref tokenCount, bits);
            for (int i = 0; i < tokenCount; i++)
            {
                if (s.State == StreamState.Read)
                    this.tokens.Add(new StringToken());

                s.Serialize(this.tokens[i]);
            }
        }
    }
}