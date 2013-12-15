using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.ResourceData;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo
{
	public class Condition
		: ISerializable<BitStream>
	{
		private static ScriptDatabase Database;

		private byte _opcode;
		private bool _inverse;
		private ushort _unionId, _startAction;
		private Parameters _parameters;

		static Condition ()
		{
			Database = new ScriptDatabase(new XmlDefinitionsTransport(Resources.ConditionDatabase));
		}

		public Condition () : this(0) { }

		public Condition (byte opcode)
		{
			_opcode = opcode;
			_parameters = new Parameters();
		}

		public virtual string Name { get { return Database.Definitions.Get(_opcode).Name; } }

		public byte Opcode { get { return _opcode; } }

		public bool IsInverse
		{
			get { return _inverse; }
			set { _inverse = value; }
		}

		public ushort UnionId
		{
			get { return _unionId; }
			set { _unionId = value; }
		}

		internal ushort StartAction
		{
			get { return _startAction; }
			set { _startAction = value; }
		}

		public Parameters Parameters
		{
			get { return _parameters; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_parameters = value;
			}
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
			_parameters.Serialize(s, definition);
		}

		#endregion
	}
}
