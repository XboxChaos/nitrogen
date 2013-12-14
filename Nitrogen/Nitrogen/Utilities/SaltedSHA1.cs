// Simplified Salted SHA-1 Hash Generator for .NET Framework Portable
// SHA-1 related code adapted from Mono

#region Original Copyright Header
//
// System.Security.Cryptography.HashAlgorithm.cs
//
// Authors:
//        Matthew S. Ford (Matthew.S.Ford@Rose-Hulman.Edu)
//        Sebastien Pouliot (sebastien@ximian.com)
//
// Copyright 2001 by Matthew S. Ford.
// Portions (C) 2002 Motus Technologies Inc. (http://www.motus.com)
// Copyright (C) 2004-2006 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
#endregion

using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace Nitrogen.Utilities
{
    public sealed class SaltedSHA1
    {
        /// <summary>
        /// Specifies the byte length of a SHA-1 hash.
        /// </summary>
        public const int Sha1HashLength = 20;

        public static byte[] GenerateHash(byte[] salt, byte[] data)
        {
            Contract.Requires<ArgumentNullException>(salt != null && data != null);

            SHA1Managed sha1 = new SHA1Managed();
            sha1.Initialize();
            sha1.TransformBlock(salt, 0, salt.Length, salt, 0);
            sha1.TransformFinalBlock(data, 0, data.Length);
            return sha1.Hash;
        }

        #region Internals

        [ContractVerification(false)]
        private class SHA1Managed : SHA1
        {
            private SHA1Internal sha;

            public SHA1Managed()
            {
                sha = new SHA1Internal();
            }

            protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
            {
                State = 1;
                sha.HashCore(rgb, ibStart, cbSize);
            }

            protected override byte[] HashFinal()
            {
                State = 0;
                return sha.HashFinal();
            }

            public override void Initialize()
            {
                sha.Initialize();
            }
        }

        [ContractVerification(false)]
        private abstract class HashAlgorithm : IDisposable
        {
            protected internal byte[] HashValue;
            protected int HashSizeValue;
            protected int State;
            private bool disposed;

            protected HashAlgorithm()
            {
                disposed = false;
            }

            public virtual bool CanTransformMultipleBlocks
            {
                get { return true; }
            }

            public virtual bool CanReuseTransform
            {
                get { return true; }
            }

            public void Clear()
            {
                // same as System.IDisposable.Dispose() which is documented
                Dispose(true);
            }

            public byte[] ComputeHash(byte[] buffer)
            {
                if (buffer == null)
                    throw new ArgumentNullException("buffer");

                return ComputeHash(buffer, 0, buffer.Length);
            }

            public byte[] ComputeHash(byte[] buffer, int offset, int count)
            {
                if (disposed)
                    throw new ObjectDisposedException("HashAlgorithm");
                if (buffer == null)
                    throw new ArgumentNullException("buffer");
                if (offset < 0)
                    throw new ArgumentOutOfRangeException("offset", "< 0");
                if (count < 0)
                    throw new ArgumentException("count", "< 0");
                // ordered to avoid possible integer overflow
                if (offset > buffer.Length - count)
                {
                    throw new ArgumentException("offset + count");
                }


                HashCore(buffer, offset, count);
                HashValue = HashFinal();
                Initialize();

                return HashValue;
            }


            public byte[] ComputeHash(Stream inputStream)
            {
                // don't read stream unless object is ready to use
                if (disposed)
                    throw new ObjectDisposedException("HashAlgorithm");


                byte[] buffer = new byte[4096];
                int len = inputStream.Read(buffer, 0, 4096);
                while (len > 0)
                {
                    HashCore(buffer, 0, len);
                    len = inputStream.Read(buffer, 0, 4096);
                }
                HashValue = HashFinal();
                Initialize();
                return HashValue;
            }

            public virtual byte[] Hash
            {
                get
                {
                    if (HashValue == null)
                    {
                        throw new Exception("No hash value computed.");
                    }
                    return HashValue;
                }
            }

            protected abstract void HashCore(byte[] array, int ibStart, int cbSize);


            protected abstract byte[] HashFinal();


            public virtual int HashSize
            {
                get { return HashSizeValue; }
            }

            public abstract void Initialize();


            protected virtual void Dispose(bool disposing)
            {
                disposed = true;
            }

            public virtual int InputBlockSize
            {
                get { return 1; }
            }

            public virtual int OutputBlockSize
            {
                get { return 1; }
            }


#if NET_4_0
                public void Dispose ()
#else
            void IDisposable.Dispose()
#endif
            {
                Dispose(true);
                GC.SuppressFinalize(this);  // Finalization is now unnecessary
            }


            // LAMESPEC: outputBuffer is optional in 2.0 (i.e. can be null).
            // However a null outputBuffer would throw a ExecutionEngineException under 1.x
            public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
            {
                if (inputBuffer == null)
                    throw new ArgumentNullException("inputBuffer");


                if (inputOffset < 0)
                    throw new ArgumentOutOfRangeException("inputOffset", "< 0");
                if (inputCount < 0)
                    throw new ArgumentException("inputCount");


                // ordered to avoid possible integer overflow
                if ((inputOffset < 0) || (inputOffset > inputBuffer.Length - inputCount))
                    throw new ArgumentException("inputBuffer");


                if (outputBuffer != null)
                {
                    if (outputOffset < 0)
                    {
                        throw new ArgumentOutOfRangeException("outputOffset", "< 0");
                    }
                    // ordered to avoid possible integer overflow
                    if (outputOffset > outputBuffer.Length - inputCount)
                    {
                        throw new ArgumentException("outputOffset + inputCount");
                    }
                }


                HashCore(inputBuffer, inputOffset, inputCount);


                if (outputBuffer != null)
                    Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);


                return inputCount;
            }

            public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
            {
                if (inputBuffer == null)
                    throw new ArgumentNullException("inputBuffer");
                if (inputCount < 0)
                    throw new ArgumentException("inputCount");
                // ordered to avoid possible integer overflow
                if (inputOffset > inputBuffer.Length - inputCount)
                {
                    throw new ArgumentException("inputOffset + inputCount");
                }


                byte[] outputBuffer = new byte[inputCount];

                // note: other exceptions are handled by Buffer.BlockCopy
                Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, 0, inputCount);

                HashCore(inputBuffer, inputOffset, inputCount);
                HashValue = HashFinal();
                Initialize();

                return outputBuffer;
            }

            public sealed class SHA1CryptoServiceProvider : SHA1
            {


                private SHA1Internal sha;


                public SHA1CryptoServiceProvider()
                {
                    sha = new SHA1Internal();
                }


                ~SHA1CryptoServiceProvider()
                {
                    Dispose(false);
                }


                protected override void Dispose(bool disposing)
                {
                    // nothing new to do (managed implementation)
                    base.Dispose(disposing);
                }


                protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
                {
                    State = 1;
                    sha.HashCore(rgb, ibStart, cbSize);
                }


                protected override byte[] HashFinal()
                {
                    State = 0;
                    return sha.HashFinal();
                }


                public override void Initialize()
                {
                    sha.Initialize();
                }
            }

        }

        [ContractVerification(false)]
        private class SHA1Internal
        {

            private const int BLOCK_SIZE_BYTES = 64;
            private uint[] _H;  // these are my chaining variables
            private ulong count;
            private byte[] _ProcessingBuffer;   // Used to start data when passed less than a block worth.
            private int _ProcessingBufferCount; // Counts how much data we have stored that still needs processed.
            private uint[] buff;


            public SHA1Internal()
            {
                _H = new uint[5];
                _ProcessingBuffer = new byte[BLOCK_SIZE_BYTES];
                buff = new uint[80];

                Initialize();
            }


            public void HashCore(byte[] rgb, int ibStart, int cbSize)
            {
                int i;


                if (_ProcessingBufferCount != 0)
                {
                    if (cbSize < (BLOCK_SIZE_BYTES - _ProcessingBufferCount))
                    {
                        System.Buffer.BlockCopy(rgb, ibStart, _ProcessingBuffer, _ProcessingBufferCount, cbSize);
                        _ProcessingBufferCount += cbSize;
                        return;
                    }
                    else
                    {
                        i = (BLOCK_SIZE_BYTES - _ProcessingBufferCount);
                        System.Buffer.BlockCopy(rgb, ibStart, _ProcessingBuffer, _ProcessingBufferCount, i);
                        ProcessBlock(_ProcessingBuffer, 0);
                        _ProcessingBufferCount = 0;
                        ibStart += i;
                        cbSize -= i;
                    }
                }


                for (i = 0; i < cbSize - cbSize % BLOCK_SIZE_BYTES; i += BLOCK_SIZE_BYTES)
                {
                    ProcessBlock(rgb, (uint)(ibStart + i));
                }


                if (cbSize % BLOCK_SIZE_BYTES != 0)
                {
                    System.Buffer.BlockCopy(rgb, cbSize - cbSize % BLOCK_SIZE_BYTES + ibStart, _ProcessingBuffer, 0, cbSize % BLOCK_SIZE_BYTES);
                    _ProcessingBufferCount = cbSize % BLOCK_SIZE_BYTES;
                }
            }

            public byte[] HashFinal()
            {
                byte[] hash = new byte[20];


                ProcessFinalBlock(_ProcessingBuffer, 0, _ProcessingBufferCount);


                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        hash[i * 4 + j] = (byte)(_H[i] >> (8 * (3 - j)));
                    }
                }


                return hash;
            }


            public void Initialize()
            {
                count = 0;
                _ProcessingBufferCount = 0;


                _H[0] = 0x67452301;
                _H[1] = 0xefcdab89;
                _H[2] = 0x98badcfe;
                _H[3] = 0x10325476;
                _H[4] = 0xC3D2E1F0;
            }


            private void ProcessBlock(byte[] inputBuffer, uint inputOffset)
            {
                uint a, b, c, d, e;


                count += BLOCK_SIZE_BYTES;


                // abc removal would not work on the fields
                uint[] _H = this._H;
                uint[] buff = this.buff;
                InitialiseBuff(buff, inputBuffer, inputOffset);
                FillBuff(buff);


                a = _H[0];
                b = _H[1];
                c = _H[2];
                d = _H[3];
                e = _H[4];


                // This function was unrolled because it seems to be doubling our performance with current compiler/VM.
                // Possibly roll up if this changes.


                // ---- Round 1 --------
                int i = 0;
                while (i < 20)
                {
                    e += ((a << 5) | (a >> 27)) + (((c ^ d) & b) ^ d) + 0x5A827999 + buff[i];
                    b = (b << 30) | (b >> 2);


                    d += ((e << 5) | (e >> 27)) + (((b ^ c) & a) ^ c) + 0x5A827999 + buff[i + 1];
                    a = (a << 30) | (a >> 2);


                    c += ((d << 5) | (d >> 27)) + (((a ^ b) & e) ^ b) + 0x5A827999 + buff[i + 2];
                    e = (e << 30) | (e >> 2);


                    b += ((c << 5) | (c >> 27)) + (((e ^ a) & d) ^ a) + 0x5A827999 + buff[i + 3];
                    d = (d << 30) | (d >> 2);


                    a += ((b << 5) | (b >> 27)) + (((d ^ e) & c) ^ e) + 0x5A827999 + buff[i + 4];
                    c = (c << 30) | (c >> 2);
                    i += 5;
                }


                // ---- Round 2 --------
                while (i < 40)
                {
                    e += ((a << 5) | (a >> 27)) + (b ^ c ^ d) + 0x6ED9EBA1 + buff[i];
                    b = (b << 30) | (b >> 2);


                    d += ((e << 5) | (e >> 27)) + (a ^ b ^ c) + 0x6ED9EBA1 + buff[i + 1];
                    a = (a << 30) | (a >> 2);


                    c += ((d << 5) | (d >> 27)) + (e ^ a ^ b) + 0x6ED9EBA1 + buff[i + 2];
                    e = (e << 30) | (e >> 2);


                    b += ((c << 5) | (c >> 27)) + (d ^ e ^ a) + 0x6ED9EBA1 + buff[i + 3];
                    d = (d << 30) | (d >> 2);


                    a += ((b << 5) | (b >> 27)) + (c ^ d ^ e) + 0x6ED9EBA1 + buff[i + 4];
                    c = (c << 30) | (c >> 2);
                    i += 5;
                }

                // ---- Round 3 --------
                while (i < 60)
                {
                    e += ((a << 5) | (a >> 27)) + ((b & c) | (b & d) | (c & d)) + 0x8F1BBCDC + buff[i];
                    b = (b << 30) | (b >> 2);


                    d += ((e << 5) | (e >> 27)) + ((a & b) | (a & c) | (b & c)) + 0x8F1BBCDC + buff[i + 1];
                    a = (a << 30) | (a >> 2);


                    c += ((d << 5) | (d >> 27)) + ((e & a) | (e & b) | (a & b)) + 0x8F1BBCDC + buff[i + 2];
                    e = (e << 30) | (e >> 2);


                    b += ((c << 5) | (c >> 27)) + ((d & e) | (d & a) | (e & a)) + 0x8F1BBCDC + buff[i + 3];
                    d = (d << 30) | (d >> 2);


                    a += ((b << 5) | (b >> 27)) + ((c & d) | (c & e) | (d & e)) + 0x8F1BBCDC + buff[i + 4];
                    c = (c << 30) | (c >> 2);
                    i += 5;
                }


                // ---- Round 4 --------
                while (i < 80)
                {
                    e += ((a << 5) | (a >> 27)) + (b ^ c ^ d) + 0xCA62C1D6 + buff[i];
                    b = (b << 30) | (b >> 2);


                    d += ((e << 5) | (e >> 27)) + (a ^ b ^ c) + 0xCA62C1D6 + buff[i + 1];
                    a = (a << 30) | (a >> 2);


                    c += ((d << 5) | (d >> 27)) + (e ^ a ^ b) + 0xCA62C1D6 + buff[i + 2];
                    e = (e << 30) | (e >> 2);


                    b += ((c << 5) | (c >> 27)) + (d ^ e ^ a) + 0xCA62C1D6 + buff[i + 3];
                    d = (d << 30) | (d >> 2);


                    a += ((b << 5) | (b >> 27)) + (c ^ d ^ e) + 0xCA62C1D6 + buff[i + 4];
                    c = (c << 30) | (c >> 2);
                    i += 5;
                }


                _H[0] += a;
                _H[1] += b;
                _H[2] += c;
                _H[3] += d;
                _H[4] += e;
            }


            private static void InitialiseBuff(uint[] buff, byte[] input, uint inputOffset)
            {
                buff[0] = (uint)((input[inputOffset + 0] << 24) | (input[inputOffset + 1] << 16) | (input[inputOffset + 2] << 8) | (input[inputOffset + 3]));
                buff[1] = (uint)((input[inputOffset + 4] << 24) | (input[inputOffset + 5] << 16) | (input[inputOffset + 6] << 8) | (input[inputOffset + 7]));
                buff[2] = (uint)((input[inputOffset + 8] << 24) | (input[inputOffset + 9] << 16) | (input[inputOffset + 10] << 8) | (input[inputOffset + 11]));
                buff[3] = (uint)((input[inputOffset + 12] << 24) | (input[inputOffset + 13] << 16) | (input[inputOffset + 14] << 8) | (input[inputOffset + 15]));
                buff[4] = (uint)((input[inputOffset + 16] << 24) | (input[inputOffset + 17] << 16) | (input[inputOffset + 18] << 8) | (input[inputOffset + 19]));
                buff[5] = (uint)((input[inputOffset + 20] << 24) | (input[inputOffset + 21] << 16) | (input[inputOffset + 22] << 8) | (input[inputOffset + 23]));
                buff[6] = (uint)((input[inputOffset + 24] << 24) | (input[inputOffset + 25] << 16) | (input[inputOffset + 26] << 8) | (input[inputOffset + 27]));
                buff[7] = (uint)((input[inputOffset + 28] << 24) | (input[inputOffset + 29] << 16) | (input[inputOffset + 30] << 8) | (input[inputOffset + 31]));
                buff[8] = (uint)((input[inputOffset + 32] << 24) | (input[inputOffset + 33] << 16) | (input[inputOffset + 34] << 8) | (input[inputOffset + 35]));
                buff[9] = (uint)((input[inputOffset + 36] << 24) | (input[inputOffset + 37] << 16) | (input[inputOffset + 38] << 8) | (input[inputOffset + 39]));
                buff[10] = (uint)((input[inputOffset + 40] << 24) | (input[inputOffset + 41] << 16) | (input[inputOffset + 42] << 8) | (input[inputOffset + 43]));
                buff[11] = (uint)((input[inputOffset + 44] << 24) | (input[inputOffset + 45] << 16) | (input[inputOffset + 46] << 8) | (input[inputOffset + 47]));
                buff[12] = (uint)((input[inputOffset + 48] << 24) | (input[inputOffset + 49] << 16) | (input[inputOffset + 50] << 8) | (input[inputOffset + 51]));
                buff[13] = (uint)((input[inputOffset + 52] << 24) | (input[inputOffset + 53] << 16) | (input[inputOffset + 54] << 8) | (input[inputOffset + 55]));
                buff[14] = (uint)((input[inputOffset + 56] << 24) | (input[inputOffset + 57] << 16) | (input[inputOffset + 58] << 8) | (input[inputOffset + 59]));
                buff[15] = (uint)((input[inputOffset + 60] << 24) | (input[inputOffset + 61] << 16) | (input[inputOffset + 62] << 8) | (input[inputOffset + 63]));
            }


            private static void FillBuff(uint[] buff)
            {
                uint val;
                for (int i = 16; i < 80; i += 8)
                {
                    val = buff[i - 3] ^ buff[i - 8] ^ buff[i - 14] ^ buff[i - 16];
                    buff[i] = (val << 1) | (val >> 31);


                    val = buff[i - 2] ^ buff[i - 7] ^ buff[i - 13] ^ buff[i - 15];
                    buff[i + 1] = (val << 1) | (val >> 31);


                    val = buff[i - 1] ^ buff[i - 6] ^ buff[i - 12] ^ buff[i - 14];
                    buff[i + 2] = (val << 1) | (val >> 31);


                    val = buff[i + 0] ^ buff[i - 5] ^ buff[i - 11] ^ buff[i - 13];
                    buff[i + 3] = (val << 1) | (val >> 31);


                    val = buff[i + 1] ^ buff[i - 4] ^ buff[i - 10] ^ buff[i - 12];
                    buff[i + 4] = (val << 1) | (val >> 31);


                    val = buff[i + 2] ^ buff[i - 3] ^ buff[i - 9] ^ buff[i - 11];
                    buff[i + 5] = (val << 1) | (val >> 31);


                    val = buff[i + 3] ^ buff[i - 2] ^ buff[i - 8] ^ buff[i - 10];
                    buff[i + 6] = (val << 1) | (val >> 31);


                    val = buff[i + 4] ^ buff[i - 1] ^ buff[i - 7] ^ buff[i - 9];
                    buff[i + 7] = (val << 1) | (val >> 31);
                }
            }

            private void ProcessFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
            {
                ulong total = count + (ulong)inputCount;
                int paddingSize = (56 - (int)(total % BLOCK_SIZE_BYTES));


                if (paddingSize < 1)
                    paddingSize += BLOCK_SIZE_BYTES;


                int length = inputCount + paddingSize + 8;
                byte[] fooBuffer = (length == 64) ? _ProcessingBuffer : new byte[length];


                for (int i = 0; i < inputCount; i++)
                {
                    fooBuffer[i] = inputBuffer[i + inputOffset];
                }


                fooBuffer[inputCount] = 0x80;
                for (int i = inputCount + 1; i < inputCount + paddingSize; i++)
                {
                    fooBuffer[i] = 0x00;
                }


                // I deal in bytes. The algorithm deals in bits.
                ulong size = total << 3;
                AddLength(size, fooBuffer, inputCount + paddingSize);
                ProcessBlock(fooBuffer, 0);


                if (length == 128)
                    ProcessBlock(fooBuffer, 64);
            }


            internal void AddLength(ulong length, byte[] buffer, int position)
            {
                buffer[position++] = (byte)(length >> 56);
                buffer[position++] = (byte)(length >> 48);
                buffer[position++] = (byte)(length >> 40);
                buffer[position++] = (byte)(length >> 32);
                buffer[position++] = (byte)(length >> 24);
                buffer[position++] = (byte)(length >> 16);
                buffer[position++] = (byte)(length >> 8);
                buffer[position] = (byte)(length);
            }
        }

        [ContractVerification(false)]
        private abstract class SHA1 : HashAlgorithm
        {
            protected SHA1()
            {
                HashSizeValue = 160;
            }

            public static SHA1 Create()
            {
                return new SHA1CryptoServiceProvider();
            }
        }

        #endregion
    }
}