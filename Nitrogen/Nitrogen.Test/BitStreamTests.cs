using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nitrogen.Core.IO;
using System.IO;

namespace Nitrogen.Test
{
    [TestClass]
    public class BitStreamTests
    {
        /*
        private static BitStream Stream;
        private static byte[] RandomBytes;

        public TestContext TestContext { get; set; }

        /// <summary>
        /// Initializes a <see cref="BitStream"/> instance to be used by test methods in this class.
        /// </summary>
        [ClassInitialize]
        public static void InitializeBitStreamTest(TestContext context)
        {
            var buffer = new MemoryStream();
            Stream = new BitStream(buffer, SerializationState.Serialize, true);

            RandomBytes = new byte[8];
            new Random().NextBytes(RandomBytes);
        }

        [ClassCleanup]
        public static void CleanUpBitStreamTest()
        {
            Stream.Dispose();
        }

        private static bool VerifyWriteOperation(ulong expectedValue, int bitLength)
        {
            ulong actualValue;
            Stream.Position -= bitLength;
            int bitsRead = Stream.Read(out actualValue, bitLength);
            return bitLength == bitsRead && actualValue == expectedValue;
        }

        [TestMethod]
        public void TestWriteUInt64()
        {
            ulong testValue = BitConverter.ToUInt64(RandomBytes, 0);
            Stream.Write(testValue);
            Assert.IsTrue(VerifyWriteOperation(testValue, 64));
        }

        [TestMethod]
        public void TestWriteUInt32()
        {
            uint testValue = BitConverter.ToUInt32(RandomBytes, 4);
            Stream.Write(testValue);
            Assert.IsTrue(VerifyWriteOperation(testValue, 32));
        }*/
    }
}
