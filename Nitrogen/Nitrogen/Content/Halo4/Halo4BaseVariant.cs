using Nitrogen.Blob.Transport.BinaryTemplates;
using Nitrogen.Content.Halo4.BaseVariant;
using Nitrogen.Content.Shared;
using Nitrogen.Utilities;
using System;
using System.IO;

namespace Nitrogen.Content.Halo4
{
    [Synchronizable]
    public class Halo4BaseVariant
        : ContentBase<Halo4BaseVariant>
    {
        /// <summary>
        /// Specifies the game variant template set should be loaded for this content.
        /// </summary>
        protected override TemplateSet BlfTemplateSet { get { return TemplateSets.GameVariant; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo4BaseVariant"/> class with default values.
        /// </summary>
        public Halo4BaseVariant()
        {
            // Pass game information to custom synchronizers.
            SyncUserData = GameRegistry.Halo4;

            // Defaults
            ContentHeader = new ContentHeader();
            GeneralSettings = new Halo4GeneralSettings();
            RespawnSettings = new Halo4RespawnSettings();
            HasWeaponTuningData = false;
        }

        [PropertyBinding]
        public ContentHeader ContentHeader { get; set; }

        [PropertyBinding("mpvr", "BaseVariant/General")]
        public Halo4GeneralSettings GeneralSettings { get; set; }

        [PropertyBinding("mpvr", "BaseVariant/Respawn")]
        public Halo4RespawnSettings RespawnSettings { get; set; }

        [PropertyBinding("mpvr", "BaseVariant/Social")]
        public Halo4SocialSettings SocialSettings { get; set; }

        [PropertyBinding("mpvr", "BaseVariant/MapOverrides")]
        public Halo4MapOverrides MapOverrides { get; set; }

        [PropertyBinding("mpvr", "BaseVariant/Powerups")]
        public Halo4PowerupSettings Powerups { get; set; }

        [PropertyBinding("mpvr", "BaseVariant/Teams")]
        public Halo4TeamSettings Teams { get; set; }

        [PropertyBinding("mpvr", "BaseVariant/Loadouts")]
        public Halo4LoadoutSettings Loadouts { get; set; }

        [PropertyBinding("mpvr", "BaseVariant/Ordnance")]
        public Halo4OrdnanceSettings OrdnanceSettings { get; set; }

        public bool HasWeaponTuningData
        {
            get { return MoshMode == 1; }
            set { MoshMode = value ? 1 : 0; }
        }

        [PropertyBinding("mpvr", "BaseVariant/Prototype/Mode")]
        private int MoshMode { get; set; }

        protected override void OnSerialize(Stream stream)
        {
            // Don't mess with the stream if it isn't binary.
            if (stream == null)
                return;

            // Recalculate variant data length.
            long newPosition = stream.Position;
            int newLength = (int)(newPosition - 0x318);
            byte[] newLengthBytes = BitConverter.GetBytes(newLength);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(newLengthBytes);

            // Write the new data length to the stream.
            stream.Position = 0x314;
            stream.Write(newLengthBytes, 0, newLengthBytes.Length);

            // Get the data to rehash.
            byte[] variantData = new byte[newLength + 4];
            stream.Position = 0x314;
            stream.Read(variantData, 0, variantData.Length);

            // Compute the hash.
            var hasher = new SaltedSHA1(GameRegistry.Halo4.Sha1Salt);
            hasher.TransformFinalBlock(variantData, 0, variantData.Length);

            // Write the new hash to the stream.
            stream.Position = 0x2FC;
            stream.Write(hasher.Hash, 0, hasher.Hash.Length);
        }
    }
}
