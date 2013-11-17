using Nitrogen.Core.IO;

namespace Nitrogen.Core.ContentData.MapVariants
{
    public interface IBoundary
        : ISerializable<BitStream>
    {
        byte BoundaryIndex { get; }
    }
}
