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
    public enum TargetReferenceType
        : byte
    {
        TeamReference,
        PlayerReference,
    }

    public class TargetReference
        : IParameter
    {
        private byte type;
        private TeamReference teamRef;
        private PlayerReference playerRef;

        public TargetReference()
        {
            this.teamRef = new TeamReference();
            this.playerRef = new PlayerReference();
        }

        public TargetReferenceType ReferenceType
        {
            get { return (TargetReferenceType)this.type; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(TargetReferenceType), value));
                this.type = (byte)value;
            }
        }

        public TeamReference Team
        {
            get { return this.teamRef; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.teamRef = value;
            }
        }

        public PlayerReference Player
        {
            get { return this.playerRef; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.playerRef = value;
            }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.type, 2);
            TargetReferenceType type = (TargetReferenceType)this.type;
            switch (type)
            {
                case TargetReferenceType.TeamReference:
                    s.Serialize(this.teamRef);
                    break;

                case TargetReferenceType.PlayerReference:
                    s.Serialize(this.playerRef);
                    break;
            }
        }
    }
}