using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.ResourceData;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo
{
	public class MegaloAction
		: ISerializable<BitStream>
	{
		private static ScriptDatabase Database;

		private byte _opcode;
		private Parameters _parameters;

		static MegaloAction ()
		{
			Database = new ScriptDatabase(new XmlDefinitionsTransport(Resources.ActionDatabase));
		}

		public MegaloAction () : this(0) { }

		public MegaloAction (byte opcode)
		{
			_opcode = opcode;
			_parameters = new Parameters();
		}

		public virtual string Name { get { return Database.Definitions.Get(_opcode).Name; } }

		public virtual Parameters Parameters
		{
			get { return _parameters; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_parameters = value;
			}
		}

		public byte Opcode { get { return _opcode; } }

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _opcode);
			if ( _opcode == 0 )
				return;

			var definition = Database.GetDefinition(_opcode);
			_parameters.Serialize(s, definition);
		}

		#endregion
	}
}