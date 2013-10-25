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

using System;
using System.Collections.Generic;
using System.Text;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared
{
    /// <summary>
    /// Defines the structure of the encoded content header in Halo: Reach and Halo 4.
    /// </summary>
    internal class ContentHeaderEncoded
        : DataTemplate
    {
        private bool isReach;

        protected override void Define(Context namedArgs)
        {
            if (namedArgs.ContainsKey(ChunkTemplate.ChunkVersion))
                isReach = ((int)namedArgs[ChunkTemplate.ChunkVersion] <= 0x36);

            Define();
        }

        protected override void Define()
        {
            var contentType = Register<byte>("ContentType", n: 4);
            Register<uint>("FileLength");
            Import<ContentIds>();

            var activity = Register<byte>("Activity", n: isReach ? 3 : 2); // 3 bits in Halo: Reach and 2 bits in Halo 4
            Register<byte>("Mode", n: 3);

            var engine = Register<Enums.Engine>("Engine", n: 3);

            Register<int>("MapId");
            Register<sbyte>("CategoryIndex");

            Action registerIdentity = () =>
            {
                Register<ulong>("UserId");
                Register<string>("Gamertag", Encoding.ASCII, null, padded: false, nullTerminated: true);
                Register<bool>("SignedIntoXboxLive", n: 1);
            };

            Register<DateTime>("DateCreated");
            Group("CreatedBy", registerIdentity);

            Register<DateTime>("DateModified");
            Group("ModifiedBy", registerIdentity);

            Register<string>("ContentName", Encoding.BigEndianUnicode, length: 128, padded: false, nullTerminated: true);
            Register<string>("ContentDescription", Encoding.BigEndianUnicode, length: 128, padded: false, nullTerminated: true);

            switch (contentType)
            {
                case 3:
                case 4:
                    Register<uint>();
                    break;

                case 6:
                    // Do nothing
                    break;

                default:
                    Register<sbyte>("GameVariantIconIndex");
                    break;
            }

            if (activity == 2)
            {
                Register<short>("HopperId");
            }

            switch (engine)
            {
                case Enums.Engine.Campaign:
                    Register<byte>("CampaignId");
                    Register<byte>("DifficultyLevel", n: 2);
                    Register<byte>("ScoringMode", n: 2);
                    Register<byte>("InsertionPoint");
                    Register<int>("Skulls");
                    break;

                case Enums.Engine.Firefight:
                    Group("Firefight", () =>
                    {
                        Register<byte>("DifficultyLevel", n: 2);
                        Register<int>("Skulls");
                    });
                    break;
            }
        }
    }
}
