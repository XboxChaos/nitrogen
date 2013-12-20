using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using Nitrogen.Wumbalo.Converters;
using System;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class OrdnanceSettingsViewModel
		: Inpc
	{
		private OrdnanceSettings _ordnance;
		private GameVariant _variant;

		public OrdnanceSettingsViewModel (GameVariant variant)
		{
			_variant = variant;
			_ordnance = _variant.OrdnanceSettings;
		}

		public bool PersonalOrdnanceEnabled
		{
			get { return _ordnance.PersonalOrdnanceEnabled; }
			set
			{
				_ordnance.PersonalOrdnanceEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool InitialOrdnanceEnabled
		{
			get { return _ordnance.InitialOrdnanceEnabled; }
			set
			{
				_ordnance.InitialOrdnanceEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool ObjectiveOrdnanceEnabled
		{
			get { return _ordnance.ObjectiveOrdnanceEnabled; }
			set
			{
				_ordnance.ObjectiveOrdnanceEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool RandomOrdnanceEnabled
		{
			get { return _ordnance.RandomOrdnanceEnabled; }
			set
			{
				_ordnance.RandomOrdnanceEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool OrdnanceSystemEnabled
		{
			get { return _ordnance.OrdnanceSystemEnabled; }
			set
			{
				_ordnance.OrdnanceSystemEnabled = value;
				OnPropertyChanged();
			}
		}

		public bool CustomizePersonalOrdnance
		{
			get { return _ordnance.CustomizePersonalOrdnance; }
			set
			{
				_ordnance.CustomizePersonalOrdnance = value;
				OnPropertyChanged();
			}
		}

		public float PersonalOrdnancePointRequirement
		{
			get { return _ordnance.PersonalOrdnancePointRequirement; }
			set
			{
				_ordnance.PersonalOrdnancePointRequirement = value;
				OnPropertyChanged();
			}
		}

		public float PersonalOrdnancePointIncrement
		{
			get { return _ordnance.PersonalOrdnancePointIncrement; }
			set
			{
				_ordnance.PersonalOrdnancePointIncrement = value;
				OnPropertyChanged();
			}
		}

		public short InitialDropDelay
		{
			get { return _ordnance.InitialDropDelay; }
			set
			{
				_ordnance.InitialDropDelay = value;
				OnPropertyChanged();
			}
		}

		public short RandomOrdnanceMaxTimer
		{
			get { return _ordnance.RandomOrdnanceMaxTimer; }
			set
			{
				_ordnance.RandomOrdnanceMaxTimer = value;
				OnPropertyChanged();
			}
		}

		public short RandomOrdnanceMinTimer
		{
			get { return _ordnance.RandomOrdnanceMinTimer; }
			set
			{
				_ordnance.RandomOrdnanceMinTimer = value;
				OnPropertyChanged();
			}
		}

		public string InitialDropLabel
		{
			get { return _ordnance.InitialDropLabel; }
			set
			{
				value = value.Trim().Replace(' ', '_');
				_ordnance.InitialDropLabel = value;

				OnPropertyChanged();
			}
		}

		/*

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
		*/
	}
}