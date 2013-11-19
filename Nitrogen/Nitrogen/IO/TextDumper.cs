using System;
using System.IO;
using System.Text;

namespace Nitrogen.Core.IO
{
    public static class TextDumper
    {
        public static void DumpObject(ITextDumpable obj, Stream output)
        {
            var builder = new StringBuilder();
            obj.Dump(builder);
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            output.Write(data, 0, data.Length);
        }
    }
}
