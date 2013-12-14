namespace Nitrogen.IO
{
    public interface ISerializable<TStream>
        where TStream : class
    {
        void SerializeObject(TStream s);
    }
}
