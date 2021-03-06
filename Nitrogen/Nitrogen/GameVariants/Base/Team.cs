﻿using Nitrogen.IO;
using Nitrogen.Shared;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
	public sealed class Team
		: ISerializable<BitStream>
	{
		private bool
            _overrideEmblem,
            _overrideUI,
            _overrideText,
            _overridePrimary,
            _overrideSecondary,
            _isEnabled,
            _overrideTeamModel;

		private Color
			_primaryColor,
			_secondaryColor,
			_textColor,
			_uiColor;

		private byte
			_index,
			_fireteamCount;

		private StringTable _teamName;
		private TeamEmblem _emblem;

        public Team()
        {
            _teamName = new StringTable();
            _emblem = new TeamEmblem();
			_primaryColor = new Color();
			_secondaryColor = new Color();
			_textColor = new Color();
			_uiColor = new Color();

			_teamName.Add(new LocalizedString(""));
        }

		public LocalizedString Name
		{
			get { return _teamName.Get(0); }

			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_teamName.Set(0, value);
			}
		}

		public TeamEmblem Emblem
		{
			get { return _emblem; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_emblem = value;
			}
		}

		public bool OverridePrimaryColor
		{
			get { return _overridePrimary; }
			set { _overridePrimary = value; }
		}

		public bool OverrideSecondaryColor
		{
			get { return _overrideSecondary; }
			set { _overrideSecondary = value; }
		}

		public bool OverrideUIColor
		{
			get { return _overrideUI; }
			set { _overrideUI = value; }
		}

		public bool OverrideTextColor
		{
			get { return _overrideText; }
			set { _overrideText = value; }
		}

		public Color PrimaryColor
		{
			get { return _primaryColor; }
			set { _primaryColor = value; }
		}

		public Color SecondaryColor
		{
			get { return _secondaryColor; }
			set { _secondaryColor = value; }
		}

		public Color UIColor
		{
			get { return _uiColor; }
			set { _uiColor = value; }
		}

		public Color TextColor
		{
			get { return _textColor; }
			set { _textColor = value; }
		}

		public bool IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value; }
		}

		public byte DesginatorId
		{
			get { return _index; }
			set
			{
				Contract.Requires<ArgumentOutOfRangeException>(value <= 15);
				_index = value;
			}
		}

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.Stream(ref _overrideEmblem);
			s.Stream(ref _overrideUI);
			s.Stream(ref _overrideText);
			s.Stream(ref _overridePrimary);
			s.Stream(ref _overrideSecondary);
			s.Stream(ref _isEnabled);
			_teamName.Serialize(s, offsetBitLength: 10, lengthBitLength: 10, countBitLength: 1);
			s.Stream(ref _index, bits: 4);
			s.Stream(ref _overrideTeamModel);
			s.SerializeObject(_primaryColor);
			s.SerializeObject(_secondaryColor);
			s.SerializeObject(_textColor);
			s.SerializeObject(_uiColor);
			s.Stream(ref _fireteamCount, bits: 5);
			s.SerializeObject(_emblem);
		}

		#endregion

		public class Color
			: ISerializable<BitStream>
		{
			private byte _red, _green, _blue, _alpha;

			public Color () : this(255, 255, 255, 255) { }

			public Color (byte red, byte green, byte blue) : this(255, red, green, blue) { }

			public Color (byte alpha, byte red, byte green, byte blue)
			{
				_alpha = alpha;
				_red = red;
				_green = green;
				_blue = blue;
			}

			public byte Alpha { get { return _alpha; } }
			public byte Red { get { return _red; } }
			public byte Green { get { return _green; } }
			public byte Blue { get { return _blue; } }

			#region ISerializable<BitStream> Members

			void ISerializable<BitStream>.SerializeObject (BitStream s)
			{
				s.Stream(ref _alpha);
				s.Stream(ref _red);
				s.Stream(ref _green);
				s.Stream(ref _blue);
			}

			#endregion
		}
	}
}
