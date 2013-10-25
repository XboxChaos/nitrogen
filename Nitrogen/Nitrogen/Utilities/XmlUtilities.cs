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
using System.Globalization;
using System.Xml.Linq;

namespace Nitrogen.Utilities
{
    /// <summary>
    /// Provides utility functions for working with XML files.
    /// </summary>
    public static class XmlUtilities
    {
        /// <summary>
        /// Attempts to parse a string representing an integer.
        /// The input string may hold a number represented in either decimal or in hexadecimal.
        /// </summary>
        /// <param name="numberStr">The string to parse. If it begins with "0x", it will be parsed as hexadecimal.</param>
        /// <param name="result">The variable to store the parsed value to if parsing succeeds.</param>
        /// <returns>true if parsing the string succeeded.</returns>
        public static bool ParseNumber(string numberStr, out int result)
        {
            if (numberStr.StartsWith("0x"))
                return int.TryParse(numberStr.Substring(2), NumberStyles.HexNumber, null, out result);
            return int.TryParse(numberStr, out result);
        }

        /// <summary>
        /// Retrieves the value of a integer attribute on an XML element.
        /// The attribute's value may be represented in either decimal or hexadecimal.
        /// An exception will be thrown if the attribute doesn't exist or is invalid.
        /// </summary>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <returns>The integer value that was parsed.</returns>
        /// <exception cref="ArgumentException">Thrown if the attribute is missing.</exception>
        /// <exception cref="FormatException">Thrown if the attribute does not represent an integer.</exception>
        /// <seealso cref="ParseNumber"/>
        public static int GetIntegerAttribute(XElement element, string name)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                throw new ArgumentException("A(n) \"" + element.Name + "\" element is missing the required \"" + name + "\" attribute.");

            int result;
            if (ParseNumber(attribute.Value, out result))
                return result;

            throw new FormatException("A(n) \"" + element.Name + "\" element has an invalid \"" + name + "\" attribute: " + attribute.Value);
        }

        /// <summary>
        /// Retrieves the value of a integer attribute on an XML element, returning a default value if it is not found.
        /// The attribute's value may be represented in either decimal or hexadecimal.
        /// </summary>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <param name="defaultValue">The value to return if the attribute is not found.</param>
        /// <returns>The attribute's value, or the default value if the attribute was not found.</returns>
        /// <exception cref="FormatException">Thrown if the attribute does not represent an integer.</exception>
        /// <seealso cref="ParseNumber"/>
        public static int GetNumericAttribute(XElement element, string name, int defaultValue)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                return defaultValue;

            int result;
            if (ParseNumber(attribute.Value, out result))
                return result;

            throw new FormatException("A(n) \"" + element.Name + "\" element has an invalid \"" + name + "\" attribute: " + attribute.Value);
        }

        /// <summary>
        /// Retrieves the value of a floating-point attribute on an XML element.
        /// </summary>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <returns>The attribute's value, or the default value if the attribute was not found.</returns>
        /// <exception cref="ArgumentException">Thrown if the attribute is missing.</exception>
        /// <exception cref="FormatException">Thrown if the attribute does not represent a floating-point value.</exception>
        public static float GetFloatAttribute(XElement element, string name)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                throw new ArgumentException("A(n) \"" + element.Name + "\" element is missing the required \"" + name + "\" attribute.");

            float result;
            if (float.TryParse(attribute.Value, out result))
                return result;

            throw new FormatException("A(n) \"" + element.Name + "\" element has an invalid \"" + name + "\" attribute: " + attribute.Value);
        }

        /// <summary>
        /// Retrieves the value of a floating-point attribute on an XML element, returning a default value if it is not found.
        /// </summary>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <param name="defaultValue">The value to return if the attribute is not found.</param>
        /// <returns>The attribute's value, or the default value if the attribute was not found.</returns>
        /// <exception cref="FormatException">Thrown if the attribute does not represent a floating-point value.</exception>
        public static float GetFloatAttribute(XElement element, string name, float defaultValue)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                return defaultValue;

            float result;
            if (float.TryParse(attribute.Value, out result))
                return result;

            throw new FormatException("A(n) \"" + element.Name + "\" element has an invalid \"" + name + "\" attribute: " + attribute.Value);
        }

        /// <summary>
        /// Retrieves the value of an attribute on an XML element.
        /// An exception will be thrown if the attribute doesn't exist.
        /// </summary>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <returns>The attribute's value.</returns>
        /// <exception cref="ArgumentException">Thrown if the attribute is missing.</exception>
        public static string GetStringAttribute(XElement element, string name)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                throw new ArgumentException("A(n) \"" + element.Name + "\" element is missing the required \"" + name + "\" attribute.");
            return attribute.Value;
        }

        /// <summary>
        /// Retrieves the value of an attribute on an XML element, returning a default value if it is not found.
        /// </summary>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <param name="defaultValue">The value to return if the attribute is not found.</param>
        /// <returns>The attribute's value, or the default value if the attribute was not found.</returns>
        public static string GetStringAttribute(XElement element, string name, string defaultValue)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute != null)
                return attribute.Value;
            return defaultValue;
        }

        /// <summary>
        /// Retrieves the value of a boolean ("true" or "false") attribute on an XML element.
        /// An exception will be thrown if the attribute doesn't exist or is invalid.
        /// </summary>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <returns>The attribute's corresponding bool value.</returns>
        /// <exception cref="FormatException">Thrown if the attribute does not represent a boolean.</exception>
        public static bool GetBoolAttribute(XElement element, string name)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                throw new ArgumentException("A(n) \"" + element.Name + "\" element is missing the required \"" + name + "\" attribute.");

            if (attribute.Value.ToLower() == "true")
                return true;
            if (attribute.Value.ToLower() == "false")
                return false;

            throw new FormatException("A(n) \"" + element.Name + "\" element has an invalid \"" + name + "\" attribute: " + attribute.Value);
        }

        /// <summary>
        /// Retrieves the value of a boolean ("true" or "false") attribute on an XML element, returning a default value if it is not found.
        /// </summary>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <param name="defaultValue">The value to return if the attribute is not found.</param>
        /// <returns>The attribute's corresponding bool value, or the default value if the attribute was not found.</returns>
        /// <exception cref="FormatException">Thrown if the attribute does not represent a boolean.</exception>
        public static bool GetBoolAttribute(XElement element, string name, bool defaultValue)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                return defaultValue;

            if (attribute.Value.ToLower() == "true")
                return true;
            if (attribute.Value.ToLower() == "false")
                return false;

            throw new FormatException("A(n) \"" + element.Name + "\" element has an invalid \"" + name + "\" attribute: " + attribute.Value);
        }

        /// <summary>
        /// Retrieves the value of an enum-based attribute on an XML element.
        /// An exception will be thrown if the attribute doesn't exist or is invalid.
        /// </summary>
        /// <typeparam name="EnumType">The type of the enum to search through.</typeparam>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <returns>The enum value corresponding to the attribute's value.</returns>
        /// <exception cref="FormatException">Thrown if the attribute value does not correspond to a value in the enum.</exception>
        public static EnumType GetEnumAttribute<EnumType>(XElement element, string name)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                throw new ArgumentException("A(n) \"" + element.Name + "\" element is missing the required \"" + name + "\" attribute.");

            EnumType result;
            if (FindEnumValue<EnumType>(attribute.Value, out result))
                return result;

            throw new FormatException("A(n) \"" + element.Name + "\" element has an invalid \"" + name + "\" attribute: " + attribute.Value);
        }

        /// <summary>
        /// Retrieves the value of an enum-based attribute on an XML element, returning a default value if it is not found.
        /// </summary>
        /// <typeparam name="EnumType">The type of the enum to search through.</typeparam>
        /// <param name="element">The XML element that holds the attribute.</param>
        /// <param name="name">The name of the attribute to get the value of.</param>
        /// <param name="defaultValue">The value to return if the attribute is not found.</param>
        /// <returns>The enum value corresponding to the attribute's value, or the default value if the attribute was not found.</returns>
        /// <exception cref="FormatException">Thrown if the attribute value does not correspond to a value in the enum.</exception>
        public static EnumType GetEnumAttribute<EnumType>(XElement element, string name, EnumType defaultValue)
        {
            XAttribute attribute = element.Attribute(name);
            if (attribute == null)
                return defaultValue;

            EnumType result;
            if (FindEnumValue<EnumType>(attribute.Value, out result))
                return result;

            throw new FormatException("A(n) \"" + element.Name + "\" element has an invalid \"" + name + "\" attribute: " + attribute.Value);
        }

        /// <summary>
        /// Returns whether or not an element has an attribute set.
        /// </summary>
        /// <param name="element">The XML element to search for the attribute on.</param>
        /// <param name="name">The name of the attribute to find.</param>
        /// <returns>true if the element has the attribute set.</returns>
        public static bool HasAttribute(XElement element, string name)
        {
            return (element.Attribute(name) != null);
        }

        /// <summary>
        /// Searches an enum for a value by name and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the enum to search through.</typeparam>
        /// <param name="name">The name of the value to search for.</param>
        /// <param name="result">The variable to store the result to if a match is found.</param>
        /// <returns>true if the value was found and stored in result.</returns>
        private static bool FindEnumValue<T>(string name, out T result)
        {
            // Use reflection to scan the enum and find the first match
            string[] names = typeof(T).GetEnumNames();
            var index = Array.IndexOf(names, name);
            if (index >= 0)
            {
                T[] values = (T[])typeof(T).GetEnumValues();
                result = values[index];
                return true;
            }

            result = default(T);
            return false;
        }
    }
}
