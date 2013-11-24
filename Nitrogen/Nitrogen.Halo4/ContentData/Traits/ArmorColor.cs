using Nitrogen.Core.IO;
using System;
using System.Drawing;

namespace Nitrogen.Halo4.ContentData.Traits
{
    public class ArmorColor
        : ISerializable<BitStream>
    {
        #region Presets

        public static readonly ArmorColor Default = new ArmorColor();
        public static readonly ArmorColor Black = new ArmorColor(Color.Black);
        public static readonly ArmorColor White = new ArmorColor(Color.White);

        #endregion

        // TODO: Add defaults for in-game color.

        private bool useDefault;
        private byte r, g, b;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="ArmorColor"/> class with default values.
        /// </summary>
        public ArmorColor()
        {
            this.useDefault = true;
            this.r = this.g = this.b = 255;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmorColor"/> class with the specified
        /// <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The color of an armor.</param>
        public ArmorColor(Color color)
        {
            Value = color;
        }

        /// <summary>
        /// Gets or sets the armor color.
        /// </summary>
        public Color? Value
        {
            get
            {
                if (this.useDefault)
                    return null;

                return Color.FromArgb(r, g, b);
            }
            set
            {
                if (value == null)
                {
                    this.useDefault = true;
                    this.r = this.g = this.b = 255;
                }
                else
                {
                    this.useDefault = false;
                    this.r = value.Value.R;
                    this.g = value.Value.G;
                    this.b = value.Value.B;
                }
            }
        }

        #region ISerializable<BitStream> Members

        public void Serialize(BitStream s)
        {
            s.Stream(ref this.useDefault);
            s.Stream(ref this.r);
            s.Stream(ref this.g);
            s.Stream(ref this.b);
        }

        #endregion
    }
}
