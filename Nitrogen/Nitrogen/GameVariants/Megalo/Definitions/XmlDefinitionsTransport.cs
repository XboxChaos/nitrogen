using Nitrogen.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Nitrogen.GameVariants.Megalo.Definitions
{
	internal sealed class XmlDefinitionsTransport
		: IDefinitionsTransport
	{
		private string _path;

		internal XmlDefinitionsTransport (string embeddedResourcePath)
		{
			_path = embeddedResourcePath;
		}

		#region IDefinitionsTransport Members

		DefinitionDatabase IDefinitionsTransport.ReadDefinitions ()
		{
			var db = new DefinitionDatabase();
			using ( var stream = this.GetType().Assembly.GetManifestResourceStream(_path) )
			{
				var definitions = XDocument.Load(stream).Element("Definitions");
				db.AddRange(definitions.Elements("Definition").Select(ReadDefinition));
			}
			return db;
		}

		#endregion

		private Definition ReadDefinition (XElement element)
		{
			string name = XmlUtilities.GetStringAttribute(element, "Name", null);
			int opcode = XmlUtilities.GetIntegerAttribute(element, "Opcode");

			var definition = new Definition(opcode, name);

			var parameters = element.Elements("Parameters");
			if (parameters != null)
			{
				definition.Parameters.AddRange(parameters.Elements().Select((XElement paramElement) =>
				{
					string paramType = paramElement.Name.LocalName;
					var param = new ParameterDefinition
					{
						ParameterType = (ParameterType) Enum.Parse(typeof(ParameterType), paramType, false),

						Name = XmlUtilities.GetStringAttribute(paramElement, "Name", null),
						Unsigned = XmlUtilities.GetBoolAttribute(paramElement, "Unsigned", false),
						Nullable = XmlUtilities.GetBoolAttribute(paramElement, "Nullable", false),
						UsePlusOneEncoding = XmlUtilities.GetBoolAttribute(paramElement, "PlusOneEncoding", false),
						BitLength = XmlUtilities.GetNumericAttribute(paramElement, "Size", 0),
						MinFloatValue = XmlUtilities.GetFloatAttribute(paramElement, "MinFloat", 0),
						MaxFloatValue = XmlUtilities.GetFloatAttribute(paramElement, "MaxFloat", 0),
						FloatFlag = XmlUtilities.GetBoolAttribute(paramElement, "FloatFlag", false),
					};

					return param;
				}));
			}

			return definition;
		}
	}
}
