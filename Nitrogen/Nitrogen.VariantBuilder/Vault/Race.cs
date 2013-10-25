using Nitrogen.Content.Halo4.BaseVariant;
using Nitrogen.Content.Halo4.Data;
using Nitrogen.Content.Halo4.Enums;
using Nitrogen.Content.Shared;
using Nitrogen.Content.Shared.Enums;
using System;

namespace Nitrogen.VariantBuilder.Vault
{
    /// <summary>
    /// Race v2.0 by Matt (synth92)
    /// </summary>
    public class Race
        : Halo4GameType
    {
        public override string Name { get { return "Race 2.0"; } }

        public override string Author { get { return "Matt"; } } // dem subz n viewz

        // Customize general settings here
        public override void InitGeneralSettings(Halo4GeneralSettings generalSettings)
        {
            generalSettings.KillCamEnabled = false;
            generalSettings.NumberOfRounds = 1;
            generalSettings.PointsSystemEnabled = false;
            generalSettings.RoundTimeLimit = 15;
            generalSettings.TeamsEnabled = false;
        }

        // Customize respawn settings here
        public override void InitRespawnSettings(Halo4RespawnSettings respawnSettings)
        {
            respawnSettings.DualRespawnTiming = false;
            respawnSettings.MinimumRespawnTime = 3;
        }

        // Customize social settings here
        public override void InitSocialSettings(Halo4SocialSettings socialSettings)
        {
            socialSettings.DeadPlayerVoice = true;
            socialSettings.EnemyPlayerVoice = true;
            socialSettings.OpenChannelVoice = true;
        }

        // Customize base player traits, vehicle set, etc here
        public override void InitMapOverrides(Halo4MapOverrides mapOverrides)
        {
            mapOverrides.ArmorAbilitiesOnMap = false;
            mapOverrides.GrenadesOnMap = false;
            mapOverrides.IndestructibleVehicles = true;
            mapOverrides.VehiclesOnMap = Halo4VehicleSet.Disabled;
        }

        // Customize powerup settings here
        public override void InitPowerups(Halo4PowerupSettings powerups)
        {

        }

        // Customize loadouts here
        public override void InitLoadouts(Halo4LoadoutSettings loadoutSettings)
        {
            loadoutSettings.LoadoutUsage = Halo4LoadoutUsage.Game;
            loadoutSettings.MapLoadoutsEnabled = false;
        }

        // Customize the ordnance system here
        public override void InitOrdnance(Halo4OrdnanceSettings ordnanceSettings)
        {
            ordnanceSettings.OrdnanceSystemEnabled = false;
            ordnanceSettings.Personal.PersonalOrdnanceEnabled = false;
        }

        // Customize teams here
        public override void InitTeams(Halo4TeamSettings teamSettings)
        {
            
        }
    }
}
