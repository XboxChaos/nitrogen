using Nitrogen.Enums;
using Nitrogen.GameVariants.Megalo.Definitions;
using Nitrogen.GameVariants.Megalo.ParameterTypes;
using Nitrogen.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo
{
	public sealed class Parameters
	{
		private List<object> _parameters;

		public Parameters ()
		{
			_parameters = new List<object>();
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
					if ( _parameters[i] is IParameter )
					{
						if ( definition.Parameters[i].ParameterType != ( _parameters[i] as IParameter ).ParameterType )
						{
							throw new Exception(string.Format(
								"Parameter type mismatch; expected {0} but got {1} (index: {2})",
								definition.Parameters[i].ParameterType,
								( _parameters[i] as IParameter ).ParameterType,
								i
							));
						}

						( _parameters[i] as IParameter ).SerializeObject(s, definition.Parameters[i]);
					}
					else if ( _parameters[i] is bool ) // Bit
					{
						bool value = (bool) _parameters[i];
						s.Writer.WriteBit(value);
					}
					else // Enum/Index/Flags
					{
						object value = (int) _parameters[i];
						StreamIntegerValue(s, definition.Parameters[i], ref value);
					}
				}
				else if ( s.State == StreamState.Read )
				{
					var param = ReadParameter(s, definition.Parameters[i]);
					if ( param != null ) { _parameters.Add(param); }
				}
			}
		}

		private object ReadParameter (BitStream s, ParameterDefinition paramDefinition)
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
				case ParameterType.Coordinates3d: { param = new Coordinates3d(); } break;

				case ParameterType.Boolean:
					return s.Reader.ReadBit();

				default:
					object value = 0;
					StreamIntegerValue(s, paramDefinition, ref value);
					return value;
			}

			if ( param != null )
				param.SerializeObject(s, paramDefinition);

			return param;
		}

		private void StreamIntegerValue (BitStream s, ParameterDefinition paramDefinition, ref object value)
		{
			int intValue = (int) ( value ?? 0 );
			switch ( paramDefinition.ParameterType )
			{
				case ParameterType.ComparisonType:
					s.StreamUnsigned(ref intValue, 3);
					value = (ComparisonType) intValue;
					break;

				case ParameterType.PlayerKillerTypeFlags:
					s.StreamUnsigned(ref intValue, 5);
					value = (PlayerKillerTypeFlags) intValue;
					break;

				case ParameterType.TeamDisposition:
					s.StreamUnsigned(ref intValue, 2);
					value = (TeamDisposition) intValue;
					break;

				case ParameterType.MultiplayerObjectType:
					int? nullableValue = (int?) value;
					s.StreamOptional(ref nullableValue, 11);
					value = (MultiplayerObjectType) ( nullableValue ?? -1 );
					break;

				case ParameterType.ObjectFilter:
					byte? filterIndex = null;
					if ( value != null )
						filterIndex = Convert.ToByte(value);
					s.StreamOptional(ref filterIndex, 4);
					value = filterIndex;
					break;

				case ParameterType.Incident:
					if ( paramDefinition.UsePlusOneEncoding )
						s.StreamPlusOne(ref intValue, 10);
					else
						s.StreamUnsigned(ref intValue);
					value = intValue;
					break;

				case ParameterType.Int32:
					s.Stream(ref intValue);
					value = intValue;
					break;

				case ParameterType.UInt5:
					s.StreamUnsigned(ref intValue, 5);
					value = intValue;
					break;

				case ParameterType.Byte:
					s.StreamUnsigned(ref intValue, 8);
					value = (byte) intValue;
					break;

				case ParameterType.OperationType:
					s.StreamUnsigned(ref intValue, 4);
					value = (OperationType) intValue;
					break;

				case ParameterType.Sound:
					s.StreamPlusOne(ref intValue, 8);
					value = intValue;
					break;

				case ParameterType.String:
					s.StreamPlusOne(ref intValue, 32);
					value = intValue;
					break;

				case ParameterType.ObjectSpawnFlags:
					s.StreamUnsigned(ref intValue, 3);
					value = (ObjectSpawnFlags) intValue;
					break;

				case ParameterType.WaypointPriority:
					s.StreamUnsigned(ref intValue, 2);
					value = (WaypointPriority) intValue;
					break;

				case ParameterType.ObjectTimer:
					byte? timerIndex = null;
					if ( value != null )
						timerIndex = Convert.ToByte(value);
					s.StreamOptional(ref timerIndex, 2);
					value = timerIndex;
				break;

				case ParameterType.ObjectPlayer:
					byte? playerIndex = null;
					if ( value != null )
						playerIndex = Convert.ToByte(value);
					s.StreamOptional(ref playerIndex, 2);
					value = playerIndex;
				break;

				case ParameterType.PlayerTraits:
					s.StreamUnsigned(ref intValue, 4);
					value = intValue;
				break;

				case ParameterType.FireteamFilter:
					s.StreamUnsigned(ref intValue, 8);
					value = intValue;
				break;
					
				case ParameterType.TimerRate:
					s.StreamUnsigned(ref intValue, 5);
					value = (TimerRate) intValue;
				break;

				case ParameterType.Trigger:
					s.StreamUnsigned(ref intValue, 7);
					value = intValue;
				break;

				case ParameterType.PlayerModes:
					s.StreamUnsigned(ref intValue, 5);
					value = intValue;
				break;

				case ParameterType.WeaponPickupMode:
					s.StreamUnsigned(ref intValue, 2);
					value = (WeaponPickupMode) intValue;
				break;

				case ParameterType.Widget:
					byte? widgetIndex = null;
					if ( value != null )
						widgetIndex = Convert.ToByte(value);
					s.StreamOptional(ref widgetIndex, 2);
					value = widgetIndex;
				break;

				case ParameterType.WidgetIcon:
					byte? widgetIcon = null;
					if ( value != null )
						widgetIcon = Convert.ToByte(value);
					s.StreamOptional(ref widgetIcon, 6);
					value = widgetIcon;
				break;

				case ParameterType.HudPosition:
					s.StreamUnsigned(ref intValue, 1);
					value = intValue;
				break;

				case ParameterType.VariantIcon:
					byte? variantIcon = null;
					if ( value != null )
						variantIcon = Convert.ToByte(value);
					s.StreamOptional(ref variantIcon, 7);
					value = (VariantIcon) variantIcon;
				break;

				case ParameterType.RequisitionPalette:
					s.StreamUnsigned(ref intValue, 4);
					value = intValue;
				break;

				case ParameterType.GrenadeType:
					s.StreamUnsigned(ref intValue, 3);
					value = (GrenadeType) intValue;
				break;

				case ParameterType.LoadoutPalette:
					s.StreamUnsigned(ref intValue, 3);
					value = intValue;
				break;

				case ParameterType.DeviceAnimation:
					s.StreamUnsigned(ref intValue, 8);
					value = (byte) intValue;
				break;

				case ParameterType.GiveWeaponMode:
					s.StreamUnsigned(ref intValue, 2);
					value = (GiveWeaponMode) intValue;
				break;

				case ParameterType.DropWeaponMode:
					s.StreamUnsigned(ref intValue, 2);
					value = (DropWeaponMode) intValue;
				break;

				case ParameterType.Button:
					s.Stream(ref intValue, 4);
					value = (Button) intValue;
				break;

				case ParameterType.Medal:
					if ( paramDefinition.Nullable )
					{
						byte? medal = null;
						if ( value != null )
							medal = Convert.ToByte(value);
						s.StreamOptional(ref medal, 8);
						value = medal;
					}
					else
					{
						s.StreamUnsigned(ref intValue, 8);
						value = (Medal) intValue;
					}
				break;

				case ParameterType.PowerupType:
					s.StreamUnsigned(ref intValue, 2);
					value = (PowerupType) intValue;
				break;

				default:
					throw new Exception("Unhandled parameter type '" + paramDefinition.ParameterType + "'");
			}
		}
	}
}
