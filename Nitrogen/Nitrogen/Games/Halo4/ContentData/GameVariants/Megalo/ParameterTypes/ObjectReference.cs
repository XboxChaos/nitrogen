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
    public enum ObjectReferenceType
        : byte
    {
        GlobalVariable,
        PlayerMemberVariable,
        ObjectMemberVariable,
        TeamMemberVariable,
        GlobalPlayerBiped,
        PlayerMemberBiped,
        ObjectMemberBiped,
        TeamMemberBiped,
    }

    public class ObjectReference
        : IParameter
    {
        private byte type, index, id;

        public ObjectReferenceType ReferenceType
        {
            get { return (ObjectReferenceType)this.type; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(ObjectReferenceType), value));
                this.type = (byte)value;
            }
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
            s.Stream(ref this.type, 3);
            ObjectReferenceType type = (ObjectReferenceType)this.type;
            switch (type)
            {
                case ObjectReferenceType.GlobalVariable:
                    s.Stream(ref this.id, 5);
                    break;

                case ObjectReferenceType.PlayerMemberVariable:
                case ObjectReferenceType.PlayerMemberBiped:
                    s.Stream(ref this.id, 6);
                    s.Stream(ref this.index, 2);
                    break;

                case ObjectReferenceType.ObjectMemberVariable:
                case ObjectReferenceType.TeamMemberVariable:
                    s.Stream(ref this.id, 5);
                    s.Stream(ref this.index, 3);
                    break;

                case ObjectReferenceType.GlobalPlayerBiped:
                    s.Stream(ref this.id, 6);
                    break;

                case ObjectReferenceType.ObjectMemberBiped:
                case ObjectReferenceType.TeamMemberBiped:
                    s.Stream(ref this.id, 5);
                    s.Stream(ref this.index, 2);
                    break;
            }
        }
    }
}