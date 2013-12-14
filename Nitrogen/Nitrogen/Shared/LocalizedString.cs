using Nitrogen.Utilities;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Shared
{
	public class LocalizedString
	{
		private Dictionary<Language, string> _table;

		public LocalizedString ()
		{
			_table = new Dictionary<Language, string>();
			foreach ( var value in Enum.GetValues(typeof(Language)) )
			{
				Language language = (Language) value;
				_table.Add(language, "Unlocalized String (" + language + ")");
			}
		}

		public LocalizedString (string value)
			: this()
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Set(value);
		}

		public virtual void Set (string value)
		{
			Contract.Requires<ArgumentNullException>(value != null);

			foreach ( var language in _table.Keys )
				Set(language, value);
		}

		public virtual void Set (Language language, string value)
		{
			Contract.Requires<ArgumentNullException>(value != null);
			Contract.Requires<ArgumentException>(language.IsDefined());

			_table[language] = value;
		}

		public virtual string Get (Language language)
		{
			Contract.Requires<ArgumentException>(language.IsDefined());
			return _table[language];
		}

		public Dictionary<Language, string> GetTable ()
		{
			var table = new Dictionary<Language, string>();
			foreach ( var val in _table )
				table.Add(val.Key, val.Value);

			return table;
		}
	}
}
