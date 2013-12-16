using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class VirtualTrigger
		: IParameter
	{
		private short _conditionStart, _conditionCount, _actionStart, _actionCount;

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.StreamPlusOne(ref _conditionStart, 10);
			s.Stream(ref _conditionCount, 10);
			s.StreamPlusOne(ref _actionStart, 11);
			s.Stream(ref _actionCount, 11);
		}

		#endregion
	}
}
