namespace Nitrogen.IO
{
    /// <summary>
    /// Specifies the byte order (endianness) of integer values.
    /// </summary>
    public enum ByteOrder
    {
        /// <summary>
        /// Indicates that the byte order of the current computer architecture should be used.
        /// </summary>
        Default,

        /// <summary>
        /// Indicates that integers are in LSB to MSB order.
        /// </summary>
        LittleEndian,

        /// <summary>
        /// Indicates that integers are in MSB to LSB order.
        /// </summary>
        BigEndian,
    }
}
