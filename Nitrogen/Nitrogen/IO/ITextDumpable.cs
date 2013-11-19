using System;
using System.Text;

namespace Nitrogen.Core.IO
{
    public interface ITextDumpable
    {
        void Dump(StringBuilder builder);
    }
}
