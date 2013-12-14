using Nitrogen.IO;
using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Base
{
    /// <summary>
    /// Represents a set of settings in a multiplayer variant for a prototype feature which was cut
    /// from the release version of Halo 4.
    /// </summary>
    internal sealed class PrototypeSettings
        : ISerializable<BitStream>
    {
        private byte
            _mode,
            _prometheanEnergyKill,
            _prometheanEnergyTime,
            _prometheanEnergyMedal,
            _prometheanDuration;

        private bool _unknownFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrototypeSettings"/> class with default values.
        /// </summary>
        public PrototypeSettings()
        {
            _mode = 1;
            _prometheanEnergyKill = 3;
            _prometheanEnergyMedal = 3;
            _prometheanEnergyTime = 3;
            _prometheanDuration = 0;
        }

        /// <summary>
        /// Gets or sets the mode.
        /// 
        /// The value must fall in the range between 0 and 3 or an exception will be thrown.
        /// </summary>
        public byte Mode
        {
            get { return _mode; }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value <= 3);
                _mode = value;
            }
        }

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject(BitStream s)
        {
            s.Stream(ref _mode, 2);
            s.Stream(ref _prometheanEnergyKill, 3);
            s.Stream(ref _prometheanEnergyTime, 3);
            s.Stream(ref _prometheanEnergyMedal, 3);
            s.Stream(ref _prometheanDuration, 4);
            s.Stream(ref _unknownFlag);
		}

		#endregion
	}
}
