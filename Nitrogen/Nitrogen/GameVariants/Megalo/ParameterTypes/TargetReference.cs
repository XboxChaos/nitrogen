using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum TargetReferenceType
	{
		TeamReference,
		PlayerReference
	}

	public sealed class TargetReference
		: IParameter
	{
		private byte _type;
		private TeamReference _teamRef;
		private PlayerReference _playerRef;

		public TargetReference ()
		{
			_teamRef = new TeamReference();
			_playerRef = new PlayerReference();
		}

		public TargetReferenceType ReferenceType
		{
			get { return (TargetReferenceType) _type; }
			set
			{
				Contract.Requires(value.IsDefined());
				_type = (byte) value;
			}
		}

		public TeamReference Team
		{
			get { return _teamRef; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_teamRef = value;
			}
		}

		public PlayerReference Player
		{
			get { return _playerRef; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_playerRef = value;
			}
		}

		#region IParameter Members

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _type, 2);
			TargetReferenceType type = (TargetReferenceType) _type;
			switch ( type )
			{
				case TargetReferenceType.TeamReference:
					( _teamRef as IParameter ).SerializeObject(s, definition);
					break;

				case TargetReferenceType.PlayerReference:
					( _playerRef as IParameter ).SerializeObject(s, definition);
					break;
			}
		}

		#endregion
	}
}
