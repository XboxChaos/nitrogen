using System;

namespace Nitrogen.Core.Blf
{
    internal class BlobFooter
        : Chunk
    {
        private int chunkOffset;
        private byte flags;
        private byte[] rsaSignature;

        internal BlobFooter()
            : base("_eof", 1)
        {
            this.rsaSignature = new byte[256];
        }

        internal int ChunkOffset
        {
            get { return this.chunkOffset; }
            set { this.chunkOffset = value; }
        }

        /// <summary>
        /// Appends an empty RSA signature to this chunk. This is needed for signed BLF files. The
        /// modified xex ignores the signature but the chunk size needs to accomodate the signature.
        /// </summary>
        internal void Resign()
        {
            this.flags = 3;
        }

        protected override void SerializeEndianStreamData(IO.EndianStream s)
        {
            s.Stream(ref this.chunkOffset);
            s.Stream(ref this.flags);

            if (this.flags == 3)
            {
                s.Stream(this.rsaSignature, 0, this.rsaSignature.Length);
            }
        }
    }
}
