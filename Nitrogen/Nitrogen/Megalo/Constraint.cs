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

using System;
using System.Collections.Generic;

namespace Nitrogen.Megalo
{
    internal static class Constraints
    {
        private static Dictionary<string, Type> ConstraintTypes = new Dictionary<string, Type>
        {
            { "EnumConstraint", typeof(EnumConstraint) },
            { "IndexOfConstraint", typeof(EnumConstraint) },
            { "ReferenceTypeConstraint", typeof(EnumConstraint) },
        };

        internal static IConstraint CreateConstraint(string name, object value)
        {
            if (ConstraintTypes.ContainsKey(name))
            {
                var constraint = Activator.CreateInstance(ConstraintTypes[name]) as IConstraint;
                constraint.SetValue(value);
                return constraint;
            }

            return null;
        }

        internal class EnumConstraint
            : Constraint<string>
        {
            public override bool IsParameterValid(MegaloDefinition definition, ParameterDefinition parameter)
            {
                throw new NotImplementedException();
            }
        }

        internal class IndexOfConstraint
            : Constraint<string>
        {
            public override bool IsParameterValid(MegaloDefinition definition, ParameterDefinition parameter)
            {
                throw new NotImplementedException();
            }
        }

        internal class ReferenceTypeConstraint
            : Constraint<string>
        {
            public override bool IsParameterValid(MegaloDefinition definition, ParameterDefinition parameter)
            {
                throw new NotImplementedException();
            }
        }

        internal abstract class Constraint<TValueType>
            : IConstraint
        {
            public TValueType Value { get; set; }

            public abstract bool IsParameterValid(MegaloDefinition definition, ParameterDefinition parameter);

            public virtual void SetValue(object value)
            {
                Value = (TValueType)value;
            }
        }

        internal interface IConstraint
        {
            bool IsParameterValid(MegaloDefinition definition, ParameterDefinition parameter);

            void SetValue(object value);
        }
    }
}
