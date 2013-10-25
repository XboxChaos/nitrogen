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
using System.Diagnostics.Contracts;
using System.Linq;

/// <summary>
/// Represents a dictionary pairing a string key and an object with support for recursion and pathfinding.
/// </summary>
public class DataTable : Dictionary<string, object>
{
    /// <summary>
    /// Searches for a value located at the given <paramref name="path"/>.
    /// </summary>
    /// <typeparam name="T">The value type.</typeparam>
    /// <param name="path">The path to the value.</param>
    /// <returns>The value if found.</returns>
    /// <exception cref="ArgumentNullException">path is null.</exception>
    /// <exception cref="ArgumentException">path is empty.</exception>
    /// <exception cref="KeyNotFoundException">path refers to a nonexistent value.</exception>
    public T GetValueByPath<T>(IEnumerable<string> path)
    {
        Contract.Requires<ArgumentNullException>(path != null);
        Contract.Requires<ArgumentException>(path.Count() > 0);

        string name = path.First();
        if (this.ContainsKey(name))
        {
            if (this[name] is DataTable && path.Count() > 1)
            {
                return (this[name] as DataTable).GetValueByPath<T>(path.Skip(1));
            }

            return (T)Convert.ChangeType(this[name], typeof(T));
        }

        throw new KeyNotFoundException();
    }

    /// <summary>
    /// Searches for a value located at the given <paramref name="path"/>.
    /// </summary>
    /// <param name="path">The path to the value.</param>
    /// <returns>The value if found.</returns>
    public object GetValueByPath(IEnumerable<string> path)
    {
        return GetValueByPath<object>(path);
    }

    /// <summary>
    /// Sets a value at the specified <paramref name="path"/>, creating new instances of the
    /// <see cref="DataTable"/> class for each level as needed.
    /// </summary>
    /// <param name="path">The path to the value.</param>
    /// <param name="value">The value to set.</param>
    /// <exception cref="ArgumentNullException">path is null.</exception>
    /// <exception cref="ArgumentException">path is empty.</exception>
    public void SetValueByPath(IEnumerable<string> path, object value)
    {
        Contract.Requires<ArgumentNullException>(path != null);
        Contract.Requires<ArgumentException>(path.Count() > 0);

        string name = path.First();
        if (path.Count() > 1)
        {
            if (!this.ContainsKey(name))
            {
                this[name] = new DataTable();
            }

            (this[name] as DataTable).SetValueByPath(path.Skip(1), value);
        }
        else
        {
            this[name] = value;
        }
    }
}