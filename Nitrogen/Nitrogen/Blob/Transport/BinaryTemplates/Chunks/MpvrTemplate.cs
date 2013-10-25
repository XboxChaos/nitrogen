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

using Nitrogen.Blob.Transport.BinaryTemplates.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Nitrogen.Blob.Transport.BinaryTemplates.Chunks
{
    /// <summary>
    /// Defines the structure of the 'mpvr' (multiplayer variant) chunk.
    /// </summary>
    public class MpvrTemplate
        : ChunkTemplate
    {
        private Dictionary<int, DataTemplate> supportedMegaloVersions;
        private Engine engine;
        private int megaloEncodingVersion;
        private Context context;

        public override string ChunkSignature { get { return "mpvr"; } }

        public override bool IsWellDefined { get { return true; } }

        protected override void Initialize(Dictionary<int, Action> supportedVersions)
        {
            supportedVersions[0x36] = DefineHaloReach;
            //supportedVersions[0x63] = DefineHalo4Beta;
            supportedVersions[0x84] = DefineHalo4;

            this.supportedMegaloVersions = new Dictionary<int, DataTemplate>
            {
                // TODO: Add all known encoding #'s from H4 and Reach betas
                { 0x6A, new HaloReach.HaloReachMegaloData() },
                { 0x103, new Halo4.Halo4MegaloData() },
            };

            this.context = new Context();
        }

        private void DefineHaloReach()
        {
            DefineHeader();
            Group("BaseVariant", () => Import<HaloReach.HaloReachBaseVariant>(this.context));

            switch (this.engine)
            {
                case Engine.Forge:
                    Group("Megalo", DefineMegaloData);
                    Group("Forge", () => Import<HaloReach.HaloReachForgeSettings>(this.context));
                    break;

                case Engine.Standard:
                    Group("Megalo", DefineMegaloData);
                    break;
            }
        }

        private void DefineHalo4()
        {
            DefineHeader();
            Group("BaseVariant", () => Import<Halo4.Halo4BaseVariant>(this.context));
            switch (this.engine)
            {
                case Engine.Forge:
                    Group("Megalo", DefineMegaloData);
                    Group("Forge", () => Import<Halo4.Halo4ForgeSettings>(this.context));
                    break;

                case Engine.Standard:
                    Group("Megalo", DefineMegaloData);
                    break;
            }
        }

        /// <summary>
        /// Defines the format for the content header found in user-generated data.
        /// </summary>
        private void DefineHeader()
        {
            this.engine = Engine.Unspecified;
            Group("Header", () =>
            {
                Register<byte[]>("Hash", count: 20);
                Register<byte[]>(count: 4); // This is ignored in-game.
                Register<uint>("DataLength");
                this.engine = Register<Engine>("Engine", n: 4);
            });

            switch (this.engine)
            {
                case Engine.Forge:
                case Engine.Standard:
                    var megaloContext = new Context { { "MegaloEncodingVersion", 0 } };
                    Group("Megalo", () => Group("Header", () => Import<Shared.Megalo.MegaloHeader>(megaloContext)));
                    this.megaloEncodingVersion = megaloContext.Get<int>("MegaloEncodingVersion");
                    break;

                case Engine.Campaign:
                case Engine.Firefight:
                case Engine.SpartanOps:
                    // Do nothing
                    break;

                default:
                    throw new InvalidOperationException(string.Format("Unknown engine type: {0}", this.engine));
            }

            Group("ContentHeader", () => Import<Shared.ContentHeaderEncoded>(NamedArgs));
        }

        private void DefineMegaloData()
        {
            if (this.supportedMegaloVersions.ContainsKey(this.megaloEncodingVersion))
                Import(this.supportedMegaloVersions[this.megaloEncodingVersion], this.context);
        }
    }
}