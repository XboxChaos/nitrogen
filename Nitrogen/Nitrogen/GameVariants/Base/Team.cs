using Nitrogen.IO;
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
			s.Serialize(_primaryColor);
			s.Serialize(_secondaryColor);
			s.Serialize(_textColor);
			s.Serialize(_uiColor);
			s.Stream(ref _fireteamCount, bits: 5);
			s.Serialize(_emblem);
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
