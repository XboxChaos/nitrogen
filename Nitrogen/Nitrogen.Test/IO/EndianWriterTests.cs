using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Nitrogen.Core.IO.Temp;

namespace Nitrogen.Test.IO
{
    [TestClass]
    public class EndianWriterTests
    {
        [TestMethod]
        public void TestEndianWriter()
        {
            using (var buffer = new MemoryStream())
            using (var writer = new EndianWriter(buffer, ByteOrder.LittleEndian, true))
            {
                writer.Write(0xFEFF);
            }
        }
    }
}
