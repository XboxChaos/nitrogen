using Nitrogen.Content.Halo4.BaseVariant;
using System;

namespace Nitrogen.VariantBuilder
{
    public abstract class Halo4GameType
        : Variant
    {
        public abstract void InitGeneralSettings(Halo4GeneralSettings generalSettings);

        public abstract void InitRespawnSettings(Halo4RespawnSettings respawnSettings);

        public abstract void InitSocialSettings(Halo4SocialSettings socialSettings);

        public abstract void InitMapOverrides(Halo4MapOverrides mapOverrides);

        public abstract void InitPowerups(Halo4PowerupSettings powerups);

        public abstract void InitLoadouts(Halo4LoadoutSettings loadoutSettings);

        public abstract void InitOrdnance(Halo4OrdnanceSettings ordnanceSettings);

        public abstract void InitTeams(Halo4TeamSettings teamSettings);
    }
}
