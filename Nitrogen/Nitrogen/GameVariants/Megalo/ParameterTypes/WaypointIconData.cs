using Nitrogen.Enums;
using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public sealed class WaypointIconData
		: IParameter
	{
		private sbyte _icon;
		private IntegerReference _letterIndex;

		public WaypointIconData()
		{
			_letterIndex = new IntegerReference();
		}

		public WaypointIconData (WaypointIcon icon)
		{
			_icon = (sbyte) icon;
		}

		public WaypointIcon Icon
		{
			get { return (WaypointIcon) _icon; }
			set
			{
				Contract.Requires(value.IsDefined());
				_icon = (sbyte) value;
			}
		}

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.StreamPlusOne(ref _icon, 5);

			if ( (WaypointIcon) _icon == WaypointIcon.Letter )
				( _letterIndex as IParameter ).SerializeObject(s, definition);
		}

		#endregion
	}
}
