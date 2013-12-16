using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
using Nitrogen.Utilities;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public enum TimerReferenceType
	{
		GlobalVariable,
		PlayerMemberVariable,
		TeamMemberVariable,
		ObjectMemberVariable
	}

	public sealed class TimerReference
		: IParameter
	{
		private byte _type, _index, _id;

		public TimerReferenceType ReferenceType
		{
			get { return (TimerReferenceType) _type; }
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
			TimerReferenceType type = (TimerReferenceType) _type;
			switch ( type )
			{
				case TimerReferenceType.GlobalVariable:
					s.Stream(ref _index, 3);
					break;

				case TimerReferenceType.PlayerMemberVariable:
					s.Stream(ref _id, 6);
					s.Stream(ref _index, 2);
					break;

				case TimerReferenceType.TeamMemberVariable:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 2);
					break;

				case TimerReferenceType.ObjectMemberVariable:
					s.Stream(ref _id, 5);
					s.Stream(ref _index, 2);
					break;
			}
		}

		#endregion
	}
}
