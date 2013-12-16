using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum EntityFilterType
	{
		None,
		Everyone,
		AllyTeams,
		EnemyTeams,
		PlayerMask,
		NoOne,
	}

	public sealed class EntityFilter
		: IParameter
	{
		private byte _type;
		private PlayerReference _playerRef;
		private IntegerReference _enabled;

		public EntityFilterType FilterType
		{
			get { return (EntityFilterType) _type; }
			set
			{
				Contract.Requires(value.IsDefined());
				_type = (byte) value;
			}
		}

		#region IParameter Members

		ParameterType IParameter.ParameterType { get { return ParameterType.EntityFilter; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _type, 3);
			var type = (EntityFilterType) _type;
			if ( type == EntityFilterType.PlayerMask )
			{
				( _playerRef as IParameter ).SerializeObject(s, definition);
				( _enabled as IParameter ).SerializeObject(s, definition);
			}
		}

		#endregion
	}
}
