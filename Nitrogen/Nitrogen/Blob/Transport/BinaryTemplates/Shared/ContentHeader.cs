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
using System.Text;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Shared
{
    /// <summary>
    /// Defines the structure of the content header in Halo: Reach and Halo 4.
    /// </summary>
    internal class ContentHeader
        : DataTemplate
    {
        private enum ContentType
            : sbyte
        {
            NotApplicable = -1,
            DLC,
            CampaignSave,
            Screenshot = 2,
            Film = 3,
            FilmClip = 4,
            MapVariant = 5,
            GameVariant = 6,
            Unknown7, // plst
            Unknown8 // Halo 4; data.cache/Unlocks
        }

        protected override void Define()
        {
            var contentType = Register<ContentType>("ContentType", n: 8, fixedLength: 32);
            Register<int>("FileLength");
            Import<ContentIds>();
            var activity = Register<sbyte>("Activity");
            Register<sbyte>("Mode");
            var engine = Register<Enums.Engine>("Engine", n: 8, fixedLength: 16);
            Register<int>("MapId");
            Register<sbyte>("CategoryIndex", n: 8, fixedLength: 64);

            Action registerIdentity = () =>
            {
                Register<ulong>("UserId");
                Register<string>("Gamertag", Encoding.ASCII, length: 16, padded: true, nullTerminated: false);
                Register<bool>("SignedIntoXboxLive", n: 8, fixedLength: 32);
            };

            Register<DateTime>("DateCreated");
            Group("CreatedBy", registerIdentity);

            Register<DateTime>("DateModified");
            Group("ModifiedBy", registerIdentity);

            Register<string>("ContentName", Encoding.BigEndianUnicode, length: 128, padded: true, nullTerminated: false);
            Register<string>("ContentDescription", Encoding.BigEndianUnicode, length: 128, padded: true, nullTerminated: false);

            switch (contentType)
            {
                case ContentType.Screenshot:
                    Register<uint>("ScreenshotNumber");
                    break;

                case ContentType.Film:
                case ContentType.FilmClip:
                    Register<uint>("FilmDuration");
                    break;

                case ContentType.GameVariant:
                    Register<sbyte>("GameVariantIconIndex", n: 8, fixedLength: 32);
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
                    Register<byte>("DifficultyLevel");
                    Register<byte>("ScoringMode");
                    Register<byte>();
                    Register<int>("InsertionPoint");
                    Register<int>("Skulls");
                    break;

                case Enums.Engine.Firefight:
                    Register<byte>("DifficultyLevel", n: 8, fixedLength: 32);
                    Register<int>("Skulls");
                    break;
            }
        }
    }
}
