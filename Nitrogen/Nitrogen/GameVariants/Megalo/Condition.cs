using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.ResourceData;
using System;
using System.Collections.Generic;

namespace Nitrogen.GameVariants.Megalo
{
	public class Condition
		: ISerializable<BitStream>
	{
		private static ScriptDatabase Database;

		private byte _opcode;
		private bool _inverse;
		private ushort _unionId, _startAction;
		private List<IParameter> _parameters;

		static Condition ()
		{
			Database = new ScriptDatabase(new XmlDefinitionsTransport(Resources.ConditionDatabase));
		}

		public Condition ()
		{
			_parameters = new List<IParameter>();
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _opcode);
			if ( _opcode == 0 )
				return;

			s.Stream(ref _inverse);
			s.Stream(ref _unionId, 10);
			s.Stream(ref _startAction, 11);

			var definition = Database.GetDefinition(_opcode);
			foreach (var parameter in definition.Parameters)
			{

			}
		}

		#endregion
	}
}
