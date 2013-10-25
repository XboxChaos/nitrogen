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

using Nitrogen.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Nitrogen.Megalo.Transport
{
    internal class XmlDefinitionsTransport
        : IDefinitionsTransport
    {
        private string path;
        private bool isEmbeddedResource;

        internal XmlDefinitionsTransport(string path, bool isEmbeddedResource)
        {
            this.path = path;
            this.isEmbeddedResource = isEmbeddedResource;
        }

        MegaloDefinitionDatabase IDefinitionsTransport.ReadDefinitions()
        {
            var db = new MegaloDefinitionDatabase();

            // Create stream.
            Stream stream;
            if (this.isEmbeddedResource)
            {
                stream = this.GetType().Assembly.GetManifestResourceStream(this.path);
            }
            else
            {
                stream = File.OpenRead(this.path);
            }

            // Read definitions.
            using (stream)
            {
                var definitions = XDocument.Load(stream).Element("Definitions");
                db.AddRange(definitions.Elements("Definition").Select(ReadDefinition));
            }

            return db;
        }

        private MegaloDefinition ReadDefinition(XElement element)
        {
            string name = XmlUtilities.GetStringAttribute(element, "Name", null);
            int opcode = XmlUtilities.GetIntegerAttribute(element, "Opcode");

            var definition = new MegaloDefinition(opcode, name);

            // Read parameters.
            var parameters = element.Element("Parameters");
            if (parameters != null)
            {
                definition.Parameters.AddRange(parameters.Elements().Select((XElement paramElement) =>
                {
                    string paramType = paramElement.Name.LocalName;
                    var param = new ParameterDefinition();
                    param.Type = (ParameterType)Enum.Parse(typeof(ParameterType), paramType);

                    // Read attributes.
                    param.Name = XmlUtilities.GetStringAttribute(paramElement, "Name", null);
                    param.Unsigned = XmlUtilities.GetBoolAttribute(paramElement, "Unsigned", false);
                    param.Nullable = XmlUtilities.GetBoolAttribute(paramElement, "Nullable", false);
                    param.PlusOneEncoding = XmlUtilities.GetBoolAttribute(paramElement, "PlusOneEncoding", false);
                    param.BitSize = XmlUtilities.GetNumericAttribute(paramElement, "Size", 0);
                    param.MinFloat = XmlUtilities.GetFloatAttribute(paramElement, "MinFloat", 0);
                    param.MaxFloat = XmlUtilities.GetFloatAttribute(paramElement, "MaxFloat", 0);
                    param.FloatFlag2 = XmlUtilities.GetBoolAttribute(paramElement, "FloatFlag", false);

                    // Read constraints.
                    var constraintsElement = paramElement.Element("Constraints");
                    if (constraintsElement != null)
                    {
                        param.Constraints = from c in constraintsElement.Elements()
                                            select Constraints.CreateConstraint(c.Name.LocalName, c.Value);
                    }

                    return param;
                }));
            }

            return definition;
        }
    }
}
