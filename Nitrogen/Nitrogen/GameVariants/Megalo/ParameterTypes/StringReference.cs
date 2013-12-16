using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public class StringReference
		: IParameter
	{
		private sbyte _stringIndex;
		private List<StringToken> _tokens;

		public StringReference ()
		{
			_tokens = new List<StringToken>();
		}

		public virtual int MaxTokens { get { return 0; } }

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.StreamPlusOne(ref _stringIndex);

			int bits = MaxTokens == 1 ? 1 : 2; // temp workaround
			int tokenCount = _tokens.Count;
			s.Stream(ref tokenCount, bits);
			for ( int i = 0; i < tokenCount; i++ )
			{
				if ( s.State == StreamState.Read )
					_tokens.Add(new StringToken());

				( _tokens[i] as IParameter ).SerializeObject(s, definition);
			}
		}

		#endregion
	}
}
