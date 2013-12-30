using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.IO;
namespace Nitrogen.GameVariants.Megalo.ParameterTypes
{
	public class Coordinates3d
		: IParameter
	{
		private float _x, _y, _z;

		public Coordinates3d () { }

		public Coordinates3d (float x, float y, float z)
		{
			_x = x;
			_y = y;
			_z = z;
		}

		public float X { get { return _x; } set { _x = value; } }
		public float Y { get { return _y; } set { _y = value; } }
		public float Z { get { return _z; } set { _z = value; } }

		#region IParameter Members

		ParameterType IParameter.ParameterType { get { return ParameterType.Coordinates3d; } }

		void IParameter.SerializeObject (BitStream s, ParameterDefinition definition)
		{
			byte x = 0, y = 0, z = 0;

			if ( s.State == StreamState.Write )
			{
				x = (byte) ( _x / .10f );
				y = (byte) ( _y / .10f );
				z = (byte) ( _z / .10f );
			}

			s.Stream(ref x);
			s.Stream(ref y);
			s.Stream(ref z);

			if (s.State == StreamState.Read)
			{
				_x = x * .10f;
				_y = y * .10f;
				_z = z * .10f;
			}
		}

		#endregion
	}
}