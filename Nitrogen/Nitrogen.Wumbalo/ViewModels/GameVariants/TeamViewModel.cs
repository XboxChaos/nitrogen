using Nitrogen.Enums;
using Nitrogen.GameVariants;
using Nitrogen.GameVariants.Base;
using Nitrogen.GameVariants.Megalo;
using Nitrogen.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Nitrogen.Wumbalo.ViewModels.GameVariants
{
	public class TeamViewModel
		: Inpc
	{
		private static readonly string[] DefaultNames =
		{
			"Red Team",
			"Blue Team",
			"Gold Team",
			"Green Team",
			"Purple Team",
			"Lime Team",
			"Orange Team",
			"Cyan Team"
		};

		private static readonly SolidColorBrush[] DefaultColors =
		{
			new SolidColorBrush(Color.FromArgb(0xFF, 0x8D, 0x2A, 0x1B)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x28, 0x46, 0x7C)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xCA, 0x00)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x6C, 0x00)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x84, 0x3B, 0xD0)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x76, 0xBD, 0x12)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x7F, 0x24)),
			new SolidColorBrush(Color.FromArgb(0xFF, 0x1F, 0xA8, 0xCA))
		};

		private static readonly int[] DefaultEmblems =
		{
		};

		private static readonly EmblemColor[] DefaultEmblemColors =
		{

		};

		private Team _team;
		private int _index;
		private BitmapSource _emblem;

		public TeamViewModel (Team team, int index)
		{
			_team = team;
			_index = index;
		}

		public SolidColorBrush PrimaryColor
		{
			get
			{
				if ( _team.OverridePrimaryColor )
				{
					var teamColor = _team.PrimaryColor;
					return new SolidColorBrush(Color.FromArgb(teamColor.Alpha, teamColor.Red, teamColor.Green, teamColor.Blue));
				}
				return DefaultColors[_index];
			}
		}

		public string DisplayName
		{
			get
			{
				string name = ( _team.Name != null ) ? _team.Name.Get(Language.English) : null;
				if ( string.IsNullOrEmpty(name) )
					name = DefaultNames[_index];

				return name;
			}
		}

		public BitmapSource Emblem
		{
			get
			{
				if ( _emblem == null )
					_emblem = CreateEmblem();

				return _emblem;
			}
		}

		private BitmapSource CreateEmblem ()
		{
			/*int width = 128;
            int height = width;
            int stride = width/8;
            byte[] pixels = new byte[height*stride];

            // Try creating a new image with a custom palette.
            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Colors.Red);
            colors.Add(System.Windows.Media.Colors.Blue);
            colors.Add(System.Windows.Media.Colors.Green);
            BitmapPalette myPalette = new BitmapPalette(colors);

            // Creates a new empty image with the pre-defined palette

            BitmapSource image = BitmapSource.Create(
                width,
                height,
                96,
                96,
                PixelFormats.Indexed1,
                myPalette, 
                pixels, 
                stride);

            FileStream stream = new FileStream("empty.tif", FileMode.Create);
            TiffBitmapEncoder encoder = new TiffBitmapEncoder();
            TextBlock myTextBlock = new TextBlock();
            myTextBlock.Text = "Codec Author is: " + encoder.CodecInfo.Author.ToString();
            encoder.Frames.Add(BitmapFrame.Create(image));
            MessageBox.Show(myPalette.Colors.Count.ToString());
            encoder.Save(stream);
*/
			using ( var stream = GetType().Assembly.GetManifestResourceStream("Nitrogen.Wumbalo.Assets.Images.ForegroundEmblems.11_atomic_2.png") )
			{
				var sourceBitmap = new BitmapImage();
				sourceBitmap.BeginInit();
				sourceBitmap.StreamSource = stream;
				sourceBitmap.EndInit();

				int stride = 512;
				byte[] pixels = new byte[sourceBitmap.PixelHeight * stride];
				sourceBitmap.CopyPixels(pixels, stride, 0);

				for ( int i = 0; i < pixels.Length; i += 32 )
				{
					for (int j = 0; j < 24; j++)
					{
						pixels[i + j] += 255;
					}
				}

				var newBitmap = BitmapSource.Create(
					sourceBitmap.PixelWidth, sourceBitmap.PixelHeight,
					96, 96,
					PixelFormats.Pbgra32,
					BitmapPalettes.WebPalette,
					pixels,
					stride
				);

				return newBitmap;
			}

			
		}
	}
}