using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum PlayerReferenceType
	{
		GlobalVariable,
		PlayerMemberVariable,
		ObjectMemberVariable,
		TeamMemberVariable
	}

	public sealed class PlayerReference
		: IParameter
	{
		private byte _type, _index, _id;

		public PlayerReferenceType ReferenceType
		{
			get { return (PlayerReferenceType) _type; }
			set
			{
				Contract.Requires(value.IsDefined());
				_type = (byte) value;
			}
		}

		public byte Target
		{
			get { return _id; }
			set { _id = value; }
		}

		public byte Index
		{
			get { return _index; }
			set { _index = value; }
		}

		#region IParameter Members

		ParameterType IParameter.ParameterType { get { return ParameterType.PlayerReference; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _type, 2);
			PlayerReferenceType type = (PlayerReferenceType) _type;
			switch ( type )
			{
				case PlayerReferenceType.GlobalVariable:
					s.Stream(ref _id, 6);
					break;

				case PlayerReferenceType.PlayerMemberVariable:
					s.Stream(ref _id, 6);
					s.Stream(ref _index, 2);
					break;

				case PlayerReferenceType.ObjectMemberVariable:
				case PlayerReferenceType.TeamMemberVariable:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 2);
					break;
			}
		}

		#endregion
	}
}
