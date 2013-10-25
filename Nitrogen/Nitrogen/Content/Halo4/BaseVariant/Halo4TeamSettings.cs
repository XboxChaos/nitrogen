/*
 *   Nitrogen - Halo Content API
 *   Copyright (c) 2013 Matt Saville and Aaron Dierking
 * 
 *   This file is part of Nitrogen.
 *
 *   Nitrogen is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   Nitrogen is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with Nitrogen.  If not, see <http://www.gnu.org/licenses/>.
 */

using Nitrogen.Content.Halo4.Data;
using System;

namespace Nitrogen.Content.Halo4.BaseVariant
{
    [Synchronizable]
    public class Halo4TeamSettings
    {
        [PropertyBinding("Team[0]")]
        public Halo4Team RedTeam { get; set; }

        [PropertyBinding("Team[1]")]
        public Halo4Team BlueTeam { get; set; }

        [PropertyBinding("Team[2]")]
        public Halo4Team GoldTeam { get; set; }

        [PropertyBinding("Team[3]")]
        public Halo4Team GreenTeam { get; set; }

        [PropertyBinding("Team[4]")]
        public Halo4Team PurpleTeam { get; set; }

        [PropertyBinding("Team[5]")]
        public Halo4Team LimeTeam { get; set; }

        [PropertyBinding("Team[6]")]
        public Halo4Team OrangeTeam { get; set; }

        [PropertyBinding("Team[7]")]
        public Halo4Team CyanTeam { get; set; }
    }
}
