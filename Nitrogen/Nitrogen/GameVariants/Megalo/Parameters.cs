using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.GameVariants.Megalo.ParameterTypes;
using Nitrogen.IO;
using System;
using System.Collections.Generic;

namespace Nitrogen.GameVariants.Megalo
{
	public sealed class Parameters
	{
		private List<IParameter> _parameters;

		public Parameters ()
		{
			_parameters = new List<IParameter>();
		}

		internal void Serialize (BitStream s, Definition definition)
		{
			if ( s.State == StreamState.Read )
				_parameters.Clear();

			if ( s.State == StreamState.Write && definition.Parameters.Count != _parameters.Count )
				throw new Exception(string.Format("Parameter count mismatch for {0}", definition.Name ?? definition.Opcode.ToString()));

			for ( int i = 0; i < definition.Parameters.Count; i++ )
			{
				if ( s.State == StreamState.Write )
				{
					if ( definition.Parameters[i].ParameterType != _parameters[i].ParameterType )
					{
						throw new Exception(string.Format(
							"Parameter type mismatch; expected {0} but got {1} (index: {2})",
							definition.Parameters[i].ParameterType,
							_parameters[i].ParameterType,
							i
						));
					}

					_parameters[i].SerializeObject(s, definition.Parameters[i]);
				}
				else if ( s.State == StreamState.Read )
				{
					var param = ReadParameter(s, definition.Parameters[i]);
					if ( param != null ) { _parameters.Add(param); }
				}
			}
		}

		private IParameter ReadParameter (BitStream s, ParameterDefinition paramDefinition)
		{
			IParameter param = null;

			switch ( paramDefinition.ParameterType )
			{
				case ParameterType.Float: { param = new FloatValue(); } break;
				case ParameterType.EntityFilter: { param = new EntityFilter(); } break;
				case ParameterType.GenericReference: { param = new GenericReference(); } break;
				case ParameterType.IntegerReference: { param = new IntegerReference(); } break;
				case ParameterType.Meter: { param = new MeterData(); } break;
				case ParameterType.ObjectReference: { param = new ObjectReference(); } break;
				case ParameterType.PlayerReference: { param = new PlayerReference(); } break;
				case ParameterType.Shape: { param = new BoundaryData(); } break;
				case ParameterType.StringReference: { param = new StringReference(); } break;
				case ParameterType.StringReferenceOneToken: { param = new StringReferenceOneToken(); } break;
				case ParameterType.StringReferenceTwoTokens: { param = new StringReferenceTwoTokens(); } break;
				case ParameterType.StringReferenceThreeTokens: { param = new StringReferenceThreeTokens(); } break;
				case ParameterType.TargetReference: { param = new TargetReference(); } break;
				case ParameterType.TeamReference: { param = new TeamReference(); } break;
				case ParameterType.TimerReference: { param = new TimerReference(); } break;
				case ParameterType.VirtualTrigger: { param = new VirtualTrigger(); } break;
				case ParameterType.WaypointIcon: { param = new WaypointIconData(); } break;

				case ParameterType.Integer:
					{
						if ( paramDefinition.Unsigned )
						{
							if ( paramDefinition.Nullable )
								param = new OptionalUInt16Value();
							else
								param = new UInt16Value();
						}
						else
						{
							if ( paramDefinition.Nullable )
								param = new OptionalInt16Value();
							else
								param = new Int16Value();
						}
					}
					break;
			}

			if ( param != null )
				param.SerializeObject(s, paramDefinition);

			return param;
		}
	}
}
