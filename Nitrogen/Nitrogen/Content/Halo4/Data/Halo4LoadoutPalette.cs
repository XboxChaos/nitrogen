using System;
using System.Collections.Generic;

namespace Nitrogen.Content.Halo4.Data
{
    [Synchronizable]
    public class Halo4LoadoutPalette
    {
        [PropertyBinding(Count = 5, Prefix = "Loadout")]
        List<Halo4Loadout> Loadouts { get; set; }
    }
}
