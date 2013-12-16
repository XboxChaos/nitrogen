using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum TeamReferenceType
	{
		GlobalVariable,
		PlayerMemberVariable,
		ObjectMemberVariable,
		TeamMemberVariable,
		PlayerOwnerTeam,
		ObjectOwnerTeam
	}

	public sealed class TeamReference
		: IParameter
	{
		private byte _type, _index, _id;

		public TeamReferenceType ReferenceType
		{
			get { return (TeamReferenceType) _type; }
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

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _type, 3);
			TeamReferenceType type = (TeamReferenceType) _type;
			switch ( type )
			{
				case TeamReferenceType.GlobalVariable:
				case TeamReferenceType.ObjectOwnerTeam:
					s.Stream(ref _id, 5);
					break;

				case TeamReferenceType.PlayerMemberVariable:
					s.Stream(ref _id, 6);
					s.Stream(ref _index, 2);
					break;

				case TeamReferenceType.ObjectMemberVariable:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 1);
					break;

				case TeamReferenceType.TeamMemberVariable:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 2);
					break;

				case TeamReferenceType.PlayerOwnerTeam:
					s.Stream(ref _id, 6);
					break;
			}
		}

		#endregion
	}
}
