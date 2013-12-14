using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.GameVariants.Base
{
	/// <summary>
	/// Represents a set of ordnance settings in a Halo 4 game variant.
	/// </summary>
	public sealed class OrdnanceSettings
		: ISerializable<BitStream>
	{
		private bool
			_initialEnabled,
			_randomEnabled,
			_objectiveEnabled,
			_personalEnabled,
			_ordnanceSystemEnabled,
			_overridePersonalOrdnance;

		private sbyte _unknown1; // -1 disables initial ordnace

		private short
			_infinityMinTimer,
			_infinityMaxTimer,
			_unknown2,
			_unknown3, // Matt: This seems to be the index of the initial_drop label in user-defined string table
			_initialDropDelay;

		private string
			_initialDropFilter,
			_randomDropSet,
			_personalDropSet,
			_substitutionSet;

		private float
			_pointReq,
			_pointIncrement;

		private OrdnanceSlot[] _personalOrdnance;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrdnanceSettings"/> class with default values.
		/// </summary>
		public OrdnanceSettings ()
		{
			_initialEnabled = true;
			_randomEnabled = true;
			_personalEnabled = true;
			_ordnanceSystemEnabled = true;
			_infinityMaxTimer = 100;
			_infinityMinTimer = 90;
			_initialDropFilter = "initial_drop";
			_pointIncrement = 0.3f;
			_pointReq = 70.0f;
			_randomDropSet = "?";
			_substitutionSet = "";
			_personalDropSet = "?";
			_unknown1 = 50;
			_unknown3 = 5;

			_personalOrdnance = new OrdnanceSlot[4];
			for ( int i = 0; i < 4; i++ )
				_personalOrdnance[i] = new OrdnanceSlot();
		}

		/// <summary>
		/// Gets or sets the personal ordnance slots which will override the default slots.
		/// </summary>
		public OrdnanceSlot[] PersonalOrdnanceSlots
		{
			get { return _personalOrdnance; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(value.Length != 4);

				_personalOrdnance = value;
				for ( int i = 0; i < 4; i++ )
				{
					if ( _personalOrdnance[i] == null )
						_personalOrdnance[i] = new OrdnanceSlot();
				}
			}
		}

		/// <summary>
		/// Gets or sets the top personal ordnance slot override.
		/// </summary>
		public OrdnanceSlot PersonalOrdnanceTopSlot
		{
			get { return PersonalOrdnanceSlots[0]; }
			set { PersonalOrdnanceSlots[0] = value; }
		}

		/// <summary>
		/// Gets or sets the left personal ordnance slot override.
		/// </summary>
		public OrdnanceSlot PersonalOrdnanceLeftSlot
		{
			get { return PersonalOrdnanceSlots[1]; }
			set { PersonalOrdnanceSlots[1] = value; }
		}

		/// <summary>
		/// Gets or sets the bottom (middle) personal ordnance slot override.
		/// </summary>
		public OrdnanceSlot PersonalOrdnanceBottomSlot
		{
			get { return PersonalOrdnanceSlots[2]; }
			set { PersonalOrdnanceSlots[2] = value; }
		}

		/// <summary>
		/// Gets or sets the right personal ordnance slot override.
		/// </summary>
		public OrdnanceSlot PersonalOrdnanceRightSlot
		{
			get { return PersonalOrdnanceSlots[3]; }
			set { PersonalOrdnanceSlots[3] = value; }
		}

		/// <summary>
		/// Gets or sets whether personal ordnance slots will be overridden.
		/// </summary>
		public bool CustomizePersonalOrdnance
		{
			get { return _overridePersonalOrdnance; }
			set { _overridePersonalOrdnance = value; }
		}

		/// <summary>
		/// Gets or sets the points requirement for calling upon a personal ordnance.
		/// </summary>
		public float PersonalOrdnancePointRequirement
		{
			get { return _pointReq; }
			set
			{
				Contract.Requires<ArgumentOutOfRangeException>(value >= 0 && value <= 10000);
				_pointReq = value;
			}
		}

		/// <summary>
		/// Gets or sets the percentage that will be added to the point requirement for the next
		/// ordnance after calling upon a personal ordnance.
		/// </summary>
		public float PersonalOrdnancePointIncrement
		{
			get { return _pointIncrement; }
			set
			{
				Contract.Requires<ArgumentOutOfRangeException>(value >= 0 && value <= 10000);
				_pointIncrement = value;
			}
		}

		/// <summary>
		/// Gets or sets the duration in seconds before initial ordnance drops at the beginning
		/// of each round.
		/// </summary>
		public short InitialDropDelay
		{
			get { return _initialDropDelay; }
			set { _initialDropDelay = value; }
		}

		/// <summary>
		/// Gets or sets the object filter label of initial drop points.
		/// </summary>
		public string InitialDropLabel
		{
			get { return _initialDropFilter; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(Encoding.UTF8.GetByteCount(value) <= 32);

				_initialDropFilter = value;
			}
		}

		/// <summary>
		/// Gets or sets the random ordnance drop set.
		/// </summary>
		public string RandomDropSet
		{
			get { return _randomDropSet; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(Encoding.UTF8.GetByteCount(value) <= 32);

				_randomDropSet = value;
			}
		}

		/// <summary>
		/// Gets or sets the personal ordnance drop set.
		/// </summary>
		public string PersonalDropSet
		{
			get { return _personalDropSet; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(Encoding.UTF8.GetByteCount(value) <= 32);

				_personalDropSet = value;
			}
		}

		/// <summary>
		/// Gets or sets the global ordnance substitution set.
		/// </summary>
		public string GlobalSubstitutionSet
		{
			get { return _substitutionSet; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentException>(Encoding.UTF8.GetByteCount(value) <= 32);

				_substitutionSet = value;
			}
		}

		/// <summary>
		/// Gets or sets the minimum time in seconds before the next random ordnance drops.
		/// </summary>
		public short RandomOrdnanceMinTimer
		{
			get { return _infinityMinTimer; }
			set { _infinityMinTimer = value; }
		}

		/// <summary>
		/// Gets or sets the maximum time in seconds before the next random ordnance drops.
		/// </summary>
		public short RandomOrdnanceMaxTimer
		{
			get { return _infinityMaxTimer; }
			set { _infinityMaxTimer = value; }
		}

		/// <summary>
		/// Gets or sets whether initial ordnance is enabled.
		/// </summary>
		public bool InitialOrdnanceEnabled
		{
			get { return _initialEnabled; }
			set { _initialEnabled = value; }
		}

		/// <summary>
		/// Gets or sets whether random ordnance is enabled.
		/// </summary>
		public bool RandomOrdnanceEnabled
		{
			get { return _randomEnabled; }
			set { _randomEnabled = value; }
		}

		/// <summary>
		/// Gets or sets whether objective ordnance is enabled.
		/// </summary>
		public bool ObjectiveOrdnanceEnabled
		{
			get { return _objectiveEnabled; }
			set { _objectiveEnabled = value; }
		}

		/// <summary>
		/// Gets or sets whether personal ordnance is enabled.
		/// </summary>
		public bool PersonalOrdnanceEnabled
		{
			get { return _personalEnabled; }
			set { _personalEnabled = value; }
		}

		/// <summary>
		/// Gets or sets whether the ordnance system is enabled.
		/// </summary>
		public bool OrdnanceSystemEnabled
		{
			get { return _ordnanceSystemEnabled; }
			set { _ordnanceSystemEnabled = value; }
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _initialEnabled);
			s.Stream(ref _randomEnabled);
			s.Stream(ref _objectiveEnabled);
			s.Stream(ref _personalEnabled);
			s.Stream(ref _ordnanceSystemEnabled);
			s.Stream(ref _unknown1);
			s.Stream(ref _infinityMinTimer);
			s.Stream(ref _infinityMaxTimer);
			s.Stream(ref _unknown2);
			s.StreamNullTerminatedString(ref _initialDropFilter, Encoding.UTF8, maxLength: 32);
			s.Stream(ref _unknown3);
			s.Stream(ref _initialDropDelay);
			s.StreamNullTerminatedString(ref _randomDropSet, Encoding.UTF8, maxLength: 32);
			s.StreamNullTerminatedString(ref _personalDropSet, Encoding.UTF8, maxLength: 32);
			s.StreamNullTerminatedString(ref _substitutionSet, Encoding.UTF8, maxLength: 32);
			s.Stream(ref _overridePersonalOrdnance);
			s.Serialize(_personalOrdnance, 0, 4);
			s.StreamEncodedFloat(ref _pointReq, bits: 30, min: 0, max: 10000, signed: false, isRounded: true, flag: false);
			s.StreamEncodedFloat(ref _pointIncrement, bits: 30, min: 0, max: 10000, signed: false, isRounded: true, flag: false);
		}

		#endregion
	}
}
