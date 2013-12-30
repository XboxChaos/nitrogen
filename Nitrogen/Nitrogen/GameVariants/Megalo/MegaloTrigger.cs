using Nitrogen.IO;
using System;

namespace Nitrogen.GameVariants.Megalo
{
	public enum TriggerType
	{
		Step,
		Call,
		Init,
		LocalInit,
		HostMigration,
		ObjectDeath,
		Local,
		Pregame,
		Incident
	}

	public enum TriggerIterationType
	{
		None,
		Players,
		PlayersRandomized,
		Teams,
		Objects,
		ObjectsFiltered,
		CandyObjectsFiltered,
	}

	public class MegaloTrigger
		: ISerializable<BitStream>
	{
		private byte _iterationType, _type, _frequency, _offset;
		private byte? _filterIndex;
		private short _conditionStart, _conditionCount, _actionStart, _actionCount;

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _iterationType, 3);
			var iterationType = (TriggerIterationType) _iterationType;
			s.Stream(ref _type, 4);

			switch ( iterationType )
			{
				case TriggerIterationType.ObjectsFiltered:
					s.StreamOptional(ref _filterIndex, 4, inverted: true);
					break;

				case TriggerIterationType.CandyObjectsFiltered:
					s.StreamOptional(ref _filterIndex, 2, inverted: true);
					break;
			}

			s.Stream(ref _conditionStart, 10);
			s.Stream(ref _conditionCount, 10);
			s.Stream(ref _actionStart, 11);
			s.Stream(ref _actionCount, 11);
			s.Stream(ref _frequency);
			s.Stream(ref _offset);
		}

		#endregion
	}
}
