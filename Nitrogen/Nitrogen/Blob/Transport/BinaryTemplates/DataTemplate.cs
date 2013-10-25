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

using Nitrogen.Utilities;
using Nitrogen.Utilities.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace Nitrogen.Blob.Transport.BinaryTemplates
{
    /// <summary>
    /// Provides a base implementation of a data template.
    /// </summary>
    public abstract class DataTemplate
    {
        /// <summary>
        /// Specifies the maximum null-terminated string length to avoid an infinite loop from occurring.
        /// </summary>
        private const int MaxStringLength = 256;

        private int currentSkipNumber;
        private DataTable currentTable;
        private BitReader reader;
        private BitWriter writer;

        /// <summary>
        /// Indicates whether data is being serialized or deserialized.
        /// </summary>
        protected enum SerializationMode
        {
            /// <summary>
            /// Indicates that no action is being performed.
            /// </summary>
            None,

            /// <summary>
            /// Indicates that the data is being deserialized.
            /// </summary>
            Deserialize,

            /// <summary>
            /// Indicates that the data is being serialized.
            /// </summary>
            Serialize,
        }

        /// <summary>
        /// Gets or sets the current character encoding for strings.
        /// </summary>
        protected Encoding CharacterEncoding
        {
            get
            {
                if (this.characterEncoding == null)
                    return Encoding.BigEndianUnicode;

                return this.characterEncoding;
            }

            set { this.characterEncoding = value; }
        }
        private Encoding characterEncoding;

        /// <summary>
        /// Gets or sets the current endianness of data.
        /// </summary>
        protected ByteOrder Endianness { get; set; }

        /// <summary>
        /// Gets the current serialization mode.
        /// </summary>
        protected SerializationMode Mode { get; private set; }

        /// <summary>
        /// Deserializes the data in the provided stream according to this template.
        /// </summary>
        /// <param name="stream">The input stream.</param>
        /// <param name="namedArgs">Contextual data to be passed to the serialization engine.</param>
        /// <returns>A <see cref="DataTable"/> object containing the deserialized data.</returns>
        internal DataTable Deserialize(Stream stream, Context namedArgs)
        {
            Contract.Requires<ArgumentNullException>(
                stream != null &&
                stream.CanRead &&
                stream.CanSeek
            );

            var rootTable = new DataTable();

            Endianness = ByteOrder.BigEndian;
            CharacterEncoding = Encoding.BigEndianUnicode;
            Mode = SerializationMode.Deserialize;

            this.currentTable = rootTable;
            this.currentSkipNumber = 0;
            this.reader = new BitReader(stream);

            Define(namedArgs);

            return rootTable;
        }

        /// <summary>
        /// Serializes the given <paramref name="data"/> to the <paramref name="stream"/> according
        /// to this template.
        /// </summary>
        /// <param name="stream">The output stream.</param>
        /// <param name="data">The data to serialize.</param>
        /// <param name="namedArgs">Contextual data to be passed to the serialization engine.</param>
        internal void Serialize(Stream stream, DataTable data, Context namedArgs)
        {
            Contract.Requires<ArgumentNullException>(
                stream != null &&
                stream.CanRead &&
                stream.CanSeek
            );

            Endianness = ByteOrder.BigEndian;
            CharacterEncoding = Encoding.BigEndianUnicode;
            Mode = SerializationMode.Serialize;

            this.currentTable = data;
            this.currentSkipNumber = 0;
            this.writer = new BitWriter(stream);

            Define(namedArgs);
        }

        /// <summary>
        /// When overridden in a derived class, defines the structure of the data.
        /// </summary>
        protected abstract void Define();

        /// <summary>
        /// When overridden in a derived class, defines the structure of the data with contextual
        /// information; otherwise, invokes the <see cref="Define"/> method normally.
        /// </summary>
        protected virtual void Define(Context namedArgs)
        {
            Define();
        }

        /// <summary>
        /// Invokes a template under the scope of a new group with a default name.
        /// </summary>
        /// <param name="definition">The template to invoke.</param>
        protected void Group(Action definition)
        {
            Group(GenerateName(), definition);
        }

        /// <summary>
        /// Invokes a template under the scope of a new group with the specified name.
        /// </summary>
        /// <param name="groupName">The name of the group.</param>
        /// <param name="definition">The template to invoke.</param>
        protected void Group(string groupName, Action definition)
        {
            Contract.Requires(groupName != null);

            // Create new group.
            DataTable newTable = null;
            if (this.currentTable.ContainsKey(groupName))
            {
                newTable = this.currentTable[groupName] as DataTable;
            }
            else
            {
                newTable = new DataTable();
                this.currentTable[groupName] = newTable;
            }

            var parentTable = this.currentTable;
            this.currentTable = newTable;

            int previousSkipNumber = this.currentSkipNumber;
            this.currentSkipNumber = 0;

            // Execute definition within the context of the newly created group.
            definition.Invoke();

            // Leave group.
            this.currentTable = parentTable;
            this.currentSkipNumber = previousSkipNumber;
        }

        /// <summary>
        /// Imports a template at the current position.
        /// </summary>
        /// <typeparam name="T">The <see cref="DataTemplate"/> class to import.</typeparam>
        /// <param name="namedArgs">Contextual data to be passed to the serialization engine.</param>
        protected void Import<T>(Context namedArgs = null)
            where T : DataTemplate, new()
        {
            T template = new T();
            Import(template, namedArgs);
        }

        /// <summary>
        /// Imports the given <paramref name="template"/> at the current position.
        /// </summary>
        /// <param name="template">A <see cref="DataTemplate"/> object to import.</param>
        /// <param name="namedArgs">Contextual data to be passed to the serialization engine.</param>
        protected void Import(DataTemplate template, Context namedArgs = null)
        {
            Contract.Requires(template != null);

            template.CharacterEncoding = CharacterEncoding;
            template.Mode = Mode;
            template.Endianness = Endianness;
            template.currentSkipNumber = this.currentSkipNumber;
            template.currentTable = this.currentTable;
            template.reader = this.reader;
            template.writer = this.writer;
            template.Define(namedArgs);
        }

        /// <summary>
        /// Registers a value with a default name.
        /// </summary>
        /// <typeparam name="T">The data type of the value.</typeparam>
        /// <param name="count">
        /// If <typeparamref name="T"/> implements <see cref="IEnumerable"/>, specifies the
        /// number of elements.
        /// </param>
        /// <param name="n">
        /// If <typeparamref name="T"/> or its underlying type is a primitive value, specifies the
        /// bit length of the value in its serialized form.
        /// </param>
        /// <param name="fixedLength">
        /// If <typeparamref name="T"/> or its underlying type is a primitive value, specifies the
        /// total number of bits the value occupies. If not null, <paramref name="fixedLength"/> -
        /// <paramref name="n"/> bits will be read from the stream as padding (which is then ignored).
        /// </param>
        /// <returns>The value that was read from or written to the underlying stream.</returns>
        protected T Register<T>(int? count = null, int? n = null, int? fixedLength = null)
        {
            return Register<T>(GenerateName(), count, n, fixedLength);
        }

        /// <summary>
        /// Registers a value with the specified <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">The data type of the value.</typeparam>
        /// <param name="key">The name of the value.</param>
        /// <param name="count">
        /// If <typeparamref name="T"/> implements <see cref="IEnumerable"/>, specifies the
        /// number of elements.
        /// </param>
        /// <param name="n">
        /// If <typeparamref name="T"/> or its underlying type is a primitive value, specifies the
        /// bit length of the value in its serialized form.
        /// </param>
        /// <param name="fixedLength">
        /// If <typeparamref name="T"/> or its underlying type is a primitive value, specifies the
        /// total number of bits the value occupies. If not null, <paramref name="fixedLength"/> -
        /// <paramref name="n"/> bits will be read from the stream as padding (which is then ignored).
        /// </param>
        /// <returns>The value that was read from or written to the underlying stream.</returns>
        protected T Register<T>(string key, int? count = null, int? n = null, int? fixedLength = null)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<InvalidOperationException>(Mode != SerializationMode.None);

            dynamic value;

            // Determine whether T is an enumerable.
            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
                // Get element type.
                var genericArgs = typeof(T).GetGenericArguments();
                Type elementType = genericArgs.Length > 0 ? genericArgs[0] : typeof(T).GetElementType();

                if (count == null)
                    throw new InvalidOperationException("Length of collection must be specified");

                if (Mode == SerializationMode.Deserialize)
                {
                    var collection = new List<object>();

                    for (int i = 0; i < count; i++)
                        collection.Add(Read(elementType, n, fixedLength));

                    value = Array.CreateInstance(elementType, collection.Count);
                    for (int i = 0; i < collection.Count; i++)
                    {
                        value.SetValue(Convert.ChangeType(collection[i], elementType), i);
                    }

                    this.currentTable[key] = value;
                    return value;
                }
                else
                {
                    value = this.currentTable[key];
                    foreach (var element in value)
                    {
                        Write(element, n, fixedLength);
                    }

                    return value;
                }
            }

            if (Mode == SerializationMode.Deserialize)
            {
                // If deserializing, read data from the underlying stream and add it to the current table.
                value = Read(typeof(T), n, fixedLength);
                this.currentTable[key] = value;
            }
            else
            {
                // If serializing, write stored data to the stream.
                value = this.currentTable[key];
                Write((T)value, n, fixedLength);
            }

            return (T)value;
        }

        /// <summary>
        /// Registers a string with a default name.
        /// </summary>
        /// <typeparam name="T">The data type of the value. This must implement <see cref="IEnumerable&lt;char&gt;"/>.</typeparam>
        /// <param name="length">The maximum length of the string.</param>
        /// <param name="padded">If true, a string with the <paramref name="length"/> will be registered.</param>
        /// <param name="nullTerminated">If true, a null delimiter will be added to the end of the string.</param>
        /// <returns>The string value that was read from or written to the underlying stream.</returns>
        protected T Register<T>(int? length, bool padded, bool nullTerminated)
            where T : class, IEnumerable<char>
        {
            return Register<T>(CharacterEncoding, length, padded, nullTerminated);
        }

        /// <summary>
        /// Registers a string with a default name in the specified <paramref name="encoding"/>.
        /// </summary>
        /// <typeparam name="T">The data type of the value. This must implement <see cref="IEnumerable&lt;char&gt;"/>.</typeparam>
        /// <param name="encoding">The character encoding of the string.</param>
        /// <param name="length">The maximum length of the string.</param>
        /// <param name="padded">If true, a string with the <paramref name="length"/> will be registered.</param>
        /// <param name="nullTerminated">If true, a null delimiter will be added to the end of the string.</param>
        /// <returns>The string value that was read from or written to the underlying stream.</returns>
        protected T Register<T>(Encoding encoding, int? length, bool padded, bool nullTerminated)
            where T : class, IEnumerable<char>
        {
            return Register<T>(GenerateName(), encoding, length, padded, nullTerminated);
        }

        /// <summary>
        /// Registers a string with the specified <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">The data type of the value. This must implement <see cref="IEnumerable&lt;char&gt;"/>.</typeparam>
        /// <param name="key">The name of the value.</param>
        /// <param name="length">The maximum length of the string.</param>
        /// <param name="padded">If true, a string with the <paramref name="length"/> will be registered.</param>
        /// <param name="nullTerminated">If true, a null delimiter will be added to the end of the string.</param>
        /// <returns>The string value that was read from or written to the underlying stream.</returns>
        protected T Register<T>(string key, int? length, bool padded, bool nullTerminated)
            where T : class, IEnumerable<char>
        {
            return Register<T>(key, CharacterEncoding, length, padded, nullTerminated);
        }

        /// <summary>
        /// Registers a string with the specified <paramref name="key"/> in the specified <paramref name="encoding"/>.
        /// </summary>
        /// <typeparam name="T">The data type of the value. This must implement <see cref="IEnumerable&lt;char&gt;"/>.</typeparam>
        /// <param name="key">The name of the value.</param>
        /// <param name="encoding">The character encoding of the string.</param>
        /// <param name="length">The maximum length of the string.</param>
        /// <param name="padded">If true, a string with the <paramref name="length"/> will be registered.</param>
        /// <param name="nullTerminated">If true, a null delimiter will be added to the end of the string.</param>
        /// <returns>The string value that was read from or written to the underlying stream.</returns>
        protected T Register<T>(string key, Encoding encoding, int? length, bool padded, bool nullTerminated)
            where T : class, IEnumerable<char>
        {
            Contract.Requires<ArgumentOutOfRangeException>(length == null || (length != null && length >= 0));

            dynamic value;

            if (Mode == SerializationMode.Deserialize)
            {
                // If deserializing, read data from the underlying stream and add it to the current table.
                value = (T)Read(encoding, length, padded, nullTerminated);
                this.currentTable[key] = value;
            }
            else
            {
                // If serializing, write stored data to the stream.
                value = this.currentTable[key] as IEnumerable<char>;

                var bytes = encoding.GetBytes(value);
                foreach (byte b in bytes)
                {
                    Write(b);
                }

                int byteCount = encoding.GetByteCount(new char[] { '\0' });

                if (nullTerminated)
                {
                    for (int i = 0; i < byteCount; i++)
                    {
                        Write((byte)0x00);
                    }
                }

                if (padded)
                {
                    for (int i = 0; i < length.Value * byteCount - bytes.Length; i++)
                    {
                        Write((byte)0x00);
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Registers a floating-point value with a default name.
        /// </summary>
        /// <typeparam name="T">
        /// The data type of the value. May be either <see cref="Single"/>, <see cref="Double"/>,
        /// or <see cref="Decimal"/>.
        /// </typeparam>
        /// <param name="n">The bit length of the value in its serialized form.</param>
        /// <param name="minValue">The minimum possible value.</param>
        /// <param name="maxValue">The maximum possible value.</param>
        /// <param name="isSigned">The value is signed.</param>
        /// <param name="roundFloat">The value should be rounded to the nearest tenth.</param>
        /// <param name="flag">dunno; just set it to true.</param>
        /// <returns>
        /// The floating-point value that was read from or written to the underlying stream.
        /// </returns>
        protected T Register<T>(int n, float minValue, float maxValue, bool isSigned, bool roundFloat = true, bool flag = true)
            where T : struct
        {
            return Register<T>(GenerateName(), n, minValue, maxValue, isSigned, roundFloat, flag);
        }

        /// <summary>
        /// Registers a floating-point value with the specified <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The data type of the value. May be either <see cref="Single"/>, <see cref="Double"/>,
        /// or <see cref="Decimal"/>.
        /// </typeparam>
        /// <param name="key">The name of the value.</param>
        /// <param name="n">The bit length of the value in its serialized form.</param>
        /// <param name="minValue">The minimum possible value.</param>
        /// <param name="maxValue">The maximum possible value.</param>
        /// <param name="isSigned">The value is signed.</param>
        /// <param name="roundFloat">The value should be rounded to the nearest tenth.</param>
        /// <param name="flag">dunno; just set it to true.</param>
        /// <returns>
        /// The floating-point value that was read from or written to the underlying stream.
        /// </returns>
        protected T Register<T>(string key, int n, float minValue, float maxValue, bool isSigned, bool roundFloat = true, bool flag = true)
        {
            Contract.Requires<InvalidOperationException>(typeof(T) == typeof(float) || typeof(T) == typeof(double));

            dynamic value;

            if (Mode == SerializationMode.Deserialize)
            {
                value = ReadEncodedFloat(n, minValue, maxValue, isSigned, roundFloat, flag);
                this.currentTable[key] = value;
            }
            else
            {
                value = this.currentTable[key];
                WriteEncodedFloat(value, n, minValue, maxValue, isSigned, roundFloat, flag);
            }

            return value;
        }

        /// <summary>
        /// Sets a value within the current scope with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The name of the value.</param>
        /// <param name="value">The value to set.</param>
        protected void SetValue(string key, object value)
        {
            this.currentTable[key] = value;
        }

        /// <summary>
        /// Gets a value from within the current scope with the specified <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">The data type of the value.</typeparam>
        /// <param name="key">The name of the value.</param>
        /// <returns>The value.</returns>
        protected T GetValue<T>(string key)
        {
            return (T)this.currentTable[key];
        }

        /// <summary>
        /// Gets a boolean value specifying whether a value with the specified <paramref name="key"/>
        /// exists within the current scope.
        /// </summary>
        /// <param name="key">The name of the value.</param>
        /// <returns>true if the value was found; otherwise, false.</returns>
        protected bool IsRegistered(string key)
        {
            return this.currentTable.ContainsKey(key);
        }

        /// <summary>
        /// Reads an encoded floating-point number from the underlying stream.
        /// </summary>
        /// <param name="n">The bit length of the value in its serialized form.</param>
        /// <param name="minValue">The minimum possible value.</param>
        /// <param name="maxValue">The maximum possible value.</param>
        /// <param name="isSigned">The value is signed.</param>
        /// <param name="roundFloat">The value should be rounded to the nearest tenth.</param>
        /// <param name="flag">dunno; just set it to true.</param>
        /// <returns>The floating-point value that was read from the underlying stream.</returns>
        protected float ReadEncodedFloat(int n, float minValue, float maxValue, bool isSigned, bool roundFloat = true, bool flag = true)
        {
            var encodedValue = this.reader.ReadUIntN(n);

            uint maxInt = (uint)(1 << n);
            float result;
            if (isSigned)
            {
                maxInt--;
                if ((encodedValue << 1) == maxInt - 1)
                    result = 0.5f * (maxValue + minValue);
            }

            if (flag)
            {
                if (encodedValue == 0)
                    return minValue;
                if (encodedValue == maxValue - 1)
                    return maxValue;

                float y = (maxValue - minValue) / (float)(maxInt - 2);
                result = (float)(encodedValue - 1) * y + y * 0.5f + minValue;
            }
            else
            {
                float y = (maxValue - minValue) / (float)maxInt;
                result = (float)encodedValue * y + y * 0.5f + minValue;
            }

            if (roundFloat)
            {
                int rounded = (int)(result + 0.5f);
                if (Math.Abs((float)rounded - result) <= .002f)
                    result = (float)rounded;
            }

            return result;
        }

        /// <summary>
        /// Writes an encoded floating-point value to the underlying stream stream.
        /// </summary>
        /// <param name="value">The value to encode.</param>
        /// <param name="n">The bit length of the value in its serialized form.</param>
        /// <param name="minValue">The minimum possible value.</param>
        /// <param name="maxValue">The maximum possible value.</param>
        /// <param name="isSigned">The value is signed.</param>
        /// <param name="roundFloat">The value should be rounded to the nearest tenth.</param>
        /// <param name="flag">dunno; just set it to true.</param>
        protected void WriteEncodedFloat(float value, int n, float minValue, float maxValue, bool isSigned, bool roundFloat = true, bool flag = true)
        {
            uint maxInt = (uint)(1 << n);
            if (isSigned)
            {
                maxInt--;
                if (value == 0.5f * (maxValue + minValue))
                {
                    this.writer.WriteUIntN(n, (maxInt - 1) >> 1);
                    return;
                }
            }

            if (flag)
            {
                if (value == minValue)
                {
                    this.writer.WriteUIntN(n, 0);
                    return;
                }

                if (value == maxValue)
                {
                    this.writer.WriteUIntN(n, maxInt - 1);
                    return;
                }

                float y = (maxValue - minValue) / (float)(maxInt - 2);
                this.writer.WriteUIntN(n, (uint)((value - minValue - y * 0.5f) / y + 1));
            }
            else
            {
                float y = (maxValue - minValue) / (float)maxInt;
                this.writer.WriteUIntN(n, (uint)((value - minValue - y * 0.5f) / y));
            }
        }

        /// <summary>
        /// Reads a string from the underlying stream in the specified <paramref name="encoding"/>.
        /// </summary>
        /// <param name="encoding">The character encoding of the string.</param>
        /// <param name="length">The maximum length of the string.</param>
        /// <param name="padded">If true, a string with the <paramref name="length"/> will be registered.</param>
        /// <param name="nullTerminated">If true, a null delimiter will be added to the end of the string.</param>
        /// <returns>The string value that was read from the underlying stream.</returns>
        protected IEnumerable<char> Read(Encoding encoding, int? length, bool padded, bool nullTerminated)
        {
            var result = new List<char>();
            int charByteLength = encoding.GetByteCount(new char[] { '\0' });
            length = length ?? MaxStringLength;

            int i = 0;
            for (i = 0; i < length; i++)
            {
                ulong value = this.reader.ReadUIntN(charByteLength * 8);

                // Break loop if this string is null-terminated and the delimiter is encountered.
                if (nullTerminated && value == 0)
                    return new string(result.ToArray());

                // Get bytes.
                byte[] bytes = BitConverter.GetBytes(value);
                Array.Resize(ref bytes, charByteLength);
                if (BitConverter.IsLittleEndian && encoding == Encoding.BigEndianUnicode)
                    Array.Reverse(bytes);

                // Decode and add character to the result.
                result.AddRange(encoding.GetString(bytes));
            }

            // If this string is padded, skip the padding.
            if (padded)
            {
                while (length.Value - i > 0)
                {
                    this.reader.ReadUIntN(8);
                    i += 8;
                }
            }

            return new string(result.ToArray());
        }

        /// <summary>
        /// Reads a primitive value or a <see cref="DateTime"/> object from the underlying stream.
        /// </summary>
        /// <param name="type">The primitive type (or <see cref="DateTime"/>)</param>
        /// <param name="n">
        /// If <paramref name="type"/> is a primitive value, specifies the bit length of the value
        /// in its serialized form.
        /// </param>
        /// <param name="fixedLength">
        /// If <paramref name="type"/> is a primitive value, specifies the total number of bits the
        /// value occupies. If not null, <paramref name="fixedLength"/> - <paramref name="n"/> bits
        /// will be read from the stream as padding (which is then ignored).
        /// </param>
        /// <returns>The value that was read from the underlying stream.</returns>
        protected object Read(Type type, int? n = null, int? fixedLength = null)
        {
            Contract.Requires<ArgumentException>(
                type == typeof(bool) ||
                type == typeof(byte) ||
                type == typeof(sbyte) ||
                type == typeof(short) ||
                type == typeof(ushort) ||
                type == typeof(int) ||
                type == typeof(uint) ||
                type == typeof(float) ||
                type == typeof(long) ||
                type == typeof(ulong) ||
                type == typeof(double) ||
                type == typeof(DateTime) ||
                type.IsEnum
            );

            // Get underlying type if type is an enum value.
            if (type.IsEnum)
            {
                type = Enum.GetUnderlyingType(type);
            }

            // Assign default bit sizes if one is not provided.
            n = n ?? GetDefaultBitLength(type);

            // Read value from the stream.
            object value;
            if (IsUnsigned(type))
                value = this.reader.ReadUIntN(n.Value);
            else
                value = this.reader.ReadIntN(n.Value);

            // Read the remaining bits if the data has a fixed length.
            if (fixedLength != null)
                this.reader.ReadIntN(fixedLength.Value - n.Value);

            // Return the value under the specified type.
            if (type == typeof(DateTime))
            {
                // Convert to Unix time.
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((long)value);
            }
            else
            {
                return Convert.ChangeType(value, type);
            }
        }

        /// <summary>
        /// Writes a primitive value or a <see cref="DateTime"/> object to the underlying stream.
        /// </summary>
        /// <param name="value">A primitive value or a <see cref="DateTime"/> object.</param>
        /// <param name="n">
        /// If the type of <paramref name="value"/> is primitive, specifies the bit length of the
        /// value in its serialized form.
        /// </param>
        /// <param name="fixedLength">
        /// If the type of <paramref name="value"/> is primitive, specifies the total number of bits
        /// the value occupies. If not null, <paramref name="fixedLength"/> - <paramref name="n"/>
        /// bits will be written to the stream as padding.
        /// </param>
        protected void Write(object value, int? n = null, int? fixedLength = null)
        {
            Contract.Requires<ArgumentException>(
                value is bool ||
                value is byte ||
                value is sbyte ||
                value is short ||
                value is ushort ||
                value is int ||
                value is uint ||
                value is float ||
                value is long ||
                value is ulong ||
                value is double ||
                value is DateTime ||
                value.GetType().IsEnum
            );

            Type type = value.GetType();

            // Get underlying type if type is an enum value.
            if (type.IsEnum)
            {
                type = Enum.GetUnderlyingType(type);
                value = Convert.ChangeType(value, type);
            }

            // Convert to Unix time.
            if (value is DateTime)
            {
                value = ((DateTime)value).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            }

            // Assign default bit sizes if one is not provided.
            n = n ?? GetDefaultBitLength(type);

            // Write value to the stream.
            if (IsUnsigned(type))
                this.writer.WriteUIntN(n.Value, Convert.ToUInt64(value));
            else
                this.writer.WriteIntN(n.Value, Convert.ToInt64(value));

            // Write the padded bits if the data has a fixed length.
            if (fixedLength != null)
                this.writer.WriteUIntN(fixedLength.Value - n.Value, 0);
        }

        /// <summary>
        /// Generates a unique name within the current scope.
        /// </summary>
        /// <returns>A name guaranteed to be unique within the current scope.</returns>
        private string GenerateName()
        {
            return "__unnamed_" + this.currentSkipNumber++;
        }

        /// <summary>
        /// Determines the default bit length of a primitive value in binary.
        /// </summary>
        /// <param name="type">The primitive type to check.</param>
        /// <returns>The bit length of the value.</returns>
        private int GetDefaultBitLength(Type type)
        {
            Contract.Requires<ArgumentException>(type.IsValueType);

            if (type == typeof(bool))
                return 1;

            if (type == typeof(byte) || type == typeof(sbyte))
                return 8;

            if (type == typeof(short) || type == typeof(ushort))
                return 16;

            if (type == typeof(int) || type == typeof(uint) || type == typeof(float))
                return 32;

            if (type == typeof(long) || type == typeof(ulong) || type == typeof(double) || type == typeof(DateTime))
                return 64;

            throw new ArgumentException();
        }

        /// <summary>
        /// Determines whether the given primitive type is signed.
        /// </summary>
        /// <param name="type">The primitive type to check.</param>
        /// <returns>true if the type is signed; otherwise, false.</returns>
        private bool IsUnsigned(Type type)
        {
            return (type == typeof(byte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(bool));
        }
    }
}
