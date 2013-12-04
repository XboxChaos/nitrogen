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
    public enum Shape
    {
        None,
        Sphere,
        Cylinder,
        Box
    }

    public class ShapeData
        : IParameter
    {
        private IntegerReference radius, top, bottom, length, width;
        private byte shapeType;

        public ShapeData()
        {
            this.radius = new IntegerReference();
            this.top = new IntegerReference();
            this.bottom = new IntegerReference();
            this.length = new IntegerReference();
            this.width = new IntegerReference();
        }

        public Shape ShapeType
        {
            get { return (Shape)this.shapeType; }
            set
            {
                Contract.Requires<InvalidEnumArgumentException>(Enum.IsDefined(typeof(Shape), value));
                this.shapeType = (byte)value;
            }
        }

        public IntegerReference Radius
        {
            get { return this.radius; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.radius = value;
            }
        }

        public IntegerReference Top
        {
            get { return this.top; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.top = value;
            }
        }

        public IntegerReference Bottom
        {
            get { return this.bottom; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.bottom = value;
            }
        }

        public IntegerReference Width
        {
            get { return this.width; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.width = value;
            }
        }

        public IntegerReference Length
        {
            get { return this.length; }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                this.length = value;
            }
        }

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.shapeType, 2);
            var shape = (Shape)this.shapeType;
            switch (shape)
            {
                case Shape.Sphere:
                    s.Serialize(this.radius);
                    break;

                case Shape.Cylinder:
                    s.Serialize(this.radius);
                    s.Serialize(this.top);
                    s.Serialize(this.bottom);
                    break;

                case Shape.Box:
                    s.Serialize(this.width);
                    s.Serialize(this.length);
                    s.Serialize(this.top);
                    s.Serialize(this.bottom);
                    break;
            }
        }
    }
}