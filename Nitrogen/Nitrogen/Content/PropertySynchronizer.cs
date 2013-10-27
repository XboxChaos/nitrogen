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

using Nitrogen.Blob;
using Nitrogen.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nitrogen.Content
{
    internal static class PropertySynchronizer
    {
        /// <summary>
        /// Converts a value to a requested type, making it suitable for synchronization.
        /// </summary>
        /// <param name="val">The value to convert.</param>
        /// <param name="requestedType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        internal static object ConvertValue(object val, Type requestedType)
        {
            if (val == null)
                return null;

            if (val.GetType() == requestedType)
                return val;

            if (requestedType.IsEnum)
            {
                return Enum.ToObject(requestedType, val);
            }
            else
            {
                var converter = requestedType.GetMethod("Convert");
                if (converter != null)
                {
                    return converter.Invoke(null, new object[] { val });
                }

                return TypeUtilities.NullableChangeType(val, requestedType);
            }
        }

        internal static bool Synchronize(
            object instance, SynchronizationMode mode, Chunk chunk, object userData, IEnumerable<string> path)
        {
            bool foundBinding = false;

            var customSync = instance as ICustomSynchronizable;
            if (customSync != null && mode == SynchronizationMode.UpdateDataTable)
            {
                customSync.UpdateDataTable(path, chunk, userData);
            }

            // Iterate through each property if the instance is synchronizable.
            if (instance.GetType().GetCustomAttribute<SynchronizableAttribute>() != null)
            {
                Type instanceType = instance.GetType();
                foreach (var property in instanceType.GetProperties(
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy))
                {
                    // Iterate through each PropertyBindingAttribute.
                    Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    var bindings = property.GetCustomAttributes<PropertyBindingAttribute>();
                    foreach (var binding in bindings)
                    {
                        // Skip this binding if the chunk is not relevant.
                        if (binding.Chunk != null && binding.Chunk != chunk.Signature)
                            continue;

                        // If parent exists, concat parent path with current path.
                        var newPath = (path == null) ? binding.Path : path.Concat(binding.Path);

                        // If this property is a container, go through this property's type's members instead.
                        if (propertyType.GetCustomAttribute<SynchronizableAttribute>() != null)
                        {
                            object newValue;
                            if (SynchronizeValue(property.GetValue(instance), propertyType, mode, chunk, userData, newPath, binding.UsePlusOneEncoding, out newValue))
                            {
                                if (mode == SynchronizationMode.UpdateProperties)
                                    property.SetValue(instance, newValue);
                            }

                            foundBinding = true;
                            continue;
                        }

                        foundBinding = true;
                        if (binding.IsListBinding)
                        {
                            // If the property is a list, then synchronize as a list.
                            SynchronizeList(property, binding, instance, property.PropertyType, mode, chunk, userData, newPath);
                        }
                        else
                        {
                            // Otherwise, it's just a value -- synchronize that.
                            try
                            {
                                var value = (mode == SynchronizationMode.UpdateProperties) ? chunk.Data.GetValueByPath(newPath) : property.GetValue(instance);
                                object newValue;
                                if (SynchronizeValue(value, propertyType, mode, chunk, userData, newPath, binding.UsePlusOneEncoding, out newValue))
                                {
                                    if (mode == SynchronizationMode.UpdateProperties)
                                    {
                                        // Trim string.
                                        if (newValue is string)
                                        {
                                            newValue = (newValue as string).Trim('\0');
                                        }

                                        property.SetValue(instance, newValue);
                                    }
                                    else
                                    {
                                        chunk.Data.SetValueByPath(newPath, newValue);
                                    }
                                }
                            }
                            catch (KeyNotFoundException)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            if (customSync != null && mode == SynchronizationMode.UpdateProperties)
            {
                customSync.UpdateProperties(path, chunk, userData);
                foundBinding = true;
            }

            return foundBinding;
        }

        internal static bool SynchronizeValue(
            object value, Type requestedType, SynchronizationMode mode, Chunk chunk, object userData, IEnumerable<string> path, bool usePlusOneEncoding, out object result)
        {
            result = null;

            // Create a new instance of the value if necessary.
            var childInstance = value;
            if (mode == SynchronizationMode.UpdateProperties && value == null && requestedType != typeof(string))
                childInstance = Activator.CreateInstance(requestedType, true);

            // If the value can be synchronized, then do so, otherwise just convert it.
            if (childInstance == null || !PropertySynchronizer.Synchronize(childInstance, mode, chunk, userData, path))
            {
                // Don't overwrite a DataTable
                if (mode == SynchronizationMode.UpdateDataTable && (chunk.Data.GetValueByPath(path) is DataTable))
                    return false;

                if (value != null && requestedType != null)
                {
                    if (usePlusOneEncoding)
                    {
                        if (mode == SynchronizationMode.UpdateProperties)
                            value = (dynamic)value - 1;
                        else
                            value = (dynamic)value + 1;
                    }

                    value = PropertySynchronizer.ConvertValue(value, requestedType);
                }
                childInstance = value;

                if (mode == SynchronizationMode.UpdateDataTable)
                {
                    result = value;
                    return true;
                }
            }

            if (mode == SynchronizationMode.UpdateProperties)
            {
                result = childInstance;
                return true;
            }

            return false;
        }

        private static void SynchronizeList(
            PropertyInfo property, PropertyBindingAttribute binding, object instance, Type listType, SynchronizationMode mode, Chunk chunk, object userData, IEnumerable<string> path)
        {
            // Don't do anything if the current chunk is not referenced.
            if (!IsChunkReferenced(chunk, path))
                return;

            Type objectType = listType.GetGenericArguments().Single();
            var countPath = path.Concat(new[] { binding.CountPropertyName });

            if (mode == SynchronizationMode.UpdateProperties)
            {
                // Obtain list size.
                uint count;
                if (binding.Count > 0)
                    count = binding.Count;
                else
                    count = chunk.Data.GetValueByPath<uint>(countPath);

                // Obtain all objects.
                var list = Activator.CreateInstance(listType) as IList;
                for (int i = 0; i < count; i++)
                {
                    string elementName = binding.Prefix ?? "";
                    elementName += i;
                    if (binding.ValueName == null)
                    {
                        var element = Activator.CreateInstance(objectType, true);
                        Synchronize(element, mode, chunk, userData, path.Concat(new[] { elementName }));
                        list.Add(element);
                    }
                    else
                    {
                        var elementPath = path.Concat(new[] { elementName, binding.ValueName });
                        var data = chunk.Data.GetValueByPath(elementPath);
                        var element = ConvertValue(data, objectType);
                        list.Add(element);
                    }
                }

                // Update property with new value.
                property.SetValue(instance, list);
            }
            else
            {
                // Update count property.
                var list = property.GetValue(instance) as IList;
                if (binding.Count == null)
                    chunk.Data.SetValueByPath(countPath, list.Count);

                // Update each object.
                for (int i = 0; i < list.Count; i++)
                {
                    string elementName = binding.Prefix ?? "";
                    elementName += i;

                    if (binding.ValueName == null)
                    {
                        Synchronize(list[i], mode, chunk, userData, path.Concat(new[] { elementName }));
                    }
                    else
                    {
                        var elementPath = path.Concat(new[] { elementName, binding.ValueName });
                        chunk.Data.SetValueByPath(elementPath, list[i]);
                    }
                }
            }
        }

        private static bool IsChunkReferenced(Chunk chunk, IEnumerable<string> path)
        {
            return (chunk.Signature == path.FirstOrDefault());
        }
    }
}
