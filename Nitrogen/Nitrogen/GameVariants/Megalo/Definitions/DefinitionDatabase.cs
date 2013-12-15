using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.Definitions
{
	internal sealed class DefinitionDatabase
		: IEnumerable<Definition>
	{
		private Dictionary<int, Definition> _definitionsByOpcode;
		private Dictionary<string, Definition> _definitionsByName;

		public DefinitionDatabase ()
		{
			_definitionsByName = new Dictionary<string, Definition>();
			_definitionsByOpcode = new Dictionary<int, Definition>();
		}

		public void Add (Definition definition)
		{
			Contract.Requires<ArgumentNullException>(definition != null);

			_definitionsByOpcode[definition.Opcode] = definition;
			if ( !string.IsNullOrEmpty(definition.Name) )
				_definitionsByName[definition.Name] = definition;
		}

		public void AddRange (IEnumerable<Definition> definitions)
		{
			Contract.Requires<ArgumentNullException>(definitions != null);
			foreach ( var definition in definitions )
				Add(definition);
		}

		public Definition Get (int opcode)
		{
			Contract.Requires<ArgumentOutOfRangeException>(opcode >= 0);

			Definition result;
			if ( _definitionsByOpcode.TryGetValue(opcode, out result) )
				return result;

			return null;
		}

		public Definition Get (string name)
		{
			Contract.Requires<ArgumentNullException>(name != null);

			Definition result;
			if ( _definitionsByName.TryGetValue(name, out result) )
				return result;

			return null;
		}

		#region IEnumerable<Definition> Members

		public IEnumerator<Definition> GetEnumerator ()
		{
			return _definitionsByOpcode.Values.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return _definitionsByOpcode.Values.GetEnumerator();
		}

		#endregion
	}
}
