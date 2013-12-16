using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum ObjectReferenceType
	{
		GlobalVariable,
		PlayerMemberVariable,
		ObjectMemberVariable,
		TeamMemberVariable,
		GlobalPlayerBiped,
		PlayerMemberBiped,
		ObjectMemberBiped,
		TeamMemberBiped
	}

	public sealed class ObjectReference
		: IParameter
	{
		private byte _type, _index, _id;

		public ObjectReferenceType ReferenceType
		{
			get { return (ObjectReferenceType) _type; }
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

		ParameterType IParameter.ParameterType { get { return ParameterType.ObjectReference; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			s.Stream(ref _type, 3);
			ObjectReferenceType type = (ObjectReferenceType) _type;
			switch ( type )
			{
				case ObjectReferenceType.GlobalVariable:
					s.Stream(ref _id, 5);
					break;

				case ObjectReferenceType.PlayerMemberVariable:
				case ObjectReferenceType.PlayerMemberBiped:
					s.Stream(ref _id, 6);
					s.Stream(ref _index, 2);
					break;

				case ObjectReferenceType.ObjectMemberVariable:
				case ObjectReferenceType.TeamMemberVariable:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 3);
					break;

				case ObjectReferenceType.GlobalPlayerBiped:
					s.Stream(ref _id, 6);
					break;

				case ObjectReferenceType.ObjectMemberBiped:
				case ObjectReferenceType.TeamMemberBiped:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 2);
					break;
			}
		}

		#endregion
	}
}
