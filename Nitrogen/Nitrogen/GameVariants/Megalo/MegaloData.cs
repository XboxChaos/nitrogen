using Nitrogen.IO;
using Nitrogen.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Nitrogen.GameVariants.Megalo
{
	public sealed class MegaloData
		: ISerializable<BitStream>
	{
		private List<UserDefinedTraits> _userDefinedTraits;
		private List<UserDefinedOption> _userDefinedOptions;
		private StringTable _userDefinedStrings;

		/*private List<UserDefinedOption> userDefinedOptions;
        private StringTable userDefinedStrings, name, description, introDescription, categoryName;
        private byte baseNameStringIndex;
        private sbyte iconIndex, categoryIndex;
        private List<ushort> mapPermissions;
        private bool mapPermissionIsExclusive;
        private int[] playerRatingParams;
        private bool unk0;
        private GlobalGameOptions globalOptions;
        private List<MapLoadout> mapLoadouts;
        private int[] disabledMenuItems, hiddenMenuItems;
        private int disabledUserDefinedItems, hiddenUserDefinedItems;*/

		public MegaloData()
		{
			_userDefinedTraits = new List<UserDefinedTraits>();
			_userDefinedOptions = new List<UserDefinedOption>();
			_userDefinedStrings = new StringTable();
		}

		#region Properties

		/// <summary>
		/// Gets or sets the user-defined trait sets.
		/// 
		/// A maximum of 16 traits are available.
		/// </summary>
		public List<UserDefinedTraits> UserDefinedTraits
		{
			get { return _userDefinedTraits; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires(value.Count <= 16);

				_userDefinedTraits = value;
			}
		}

		/// <summary>
		/// Gets or sets the user-defined game variant options.
		/// </summary>
		public List<UserDefinedOption> UserDefinedOptions
		{
			get { return _userDefinedOptions; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_userDefinedOptions = value;
			}
		}

		/// <summary>
		/// Gets or sets the string table containing user-defined strings.
		/// </summary>
		public StringTable UserDefinedStrings
		{
			get { return _userDefinedStrings; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);
				_userDefinedStrings = value;
			}
		}

		#endregion

		#region ISerializable<BitStream> Members

		void ISerializable<BitStream>.SerializeObject (BitStream s)
		{
			s.SerializeObjects(_userDefinedTraits, countBitLength: 5);
			s.SerializeObjects(_userDefinedOptions, countBitLength: 5);
			_userDefinedStrings.Serialize(s, offsetBitLength: 16, lengthBitLength: 16, countBitLength: 8);




			/*
			s.Stream(ref _baseNameStringIndex);
			_name.Serialize(s, 11, 11, 1);
			_description.Serialize(s, 13, 13, 1);
			_introDescription.Serialize(s, 13, 13, 1);
			_categoryName.Serialize(s, 10, 10, 1);
			s.StreamPlusOne(ref _iconIndex, 5);
			s.StreamPlusOne(ref _categoryIndex, 5);

			int permissionCount = _mapPermissions.Count;
			s.Stream(ref permissionCount, 6);
			if ( permissionCount >= 0 && permissionCount < 32 )
			{
				if ( s.State == StreamState.Read )
					_mapPermissions = new List<ushort>(new ushort[permissionCount]);

				s.Stream(_mapPermissions, 0, permissionCount);
				s.Stream(ref _mapPermissionIsExclusive);
			}

			s.Stream(_playerRatingParams, 0, _playerRatingParams.Length);
			s.Stream(ref _unk0);
			s.Serialize(_globalOptions);

			int mapLoadoutsCount = _mapLoadouts.Count;
			s.Stream(ref mapLoadoutsCount);
			if ( s.State == StreamState.Read )
			{
				_mapLoadouts = new List<MapLoadout>();
				for ( int i = 0; i < mapLoadoutsCount; i++ )
					_mapLoadouts.Add(new MapLoadout());
			}
			s.Serialize(_mapLoadouts, 0, mapLoadoutsCount);

			s.Stream(_disabledMenuItems, 0, _disabledMenuItems.Length);
			s.Stream(_hiddenMenuItems, 0, _hiddenMenuItems.Length);
			s.Stream(ref _disabledUserDefinedItems);
			s.Stream(ref _hiddenUserDefinedItems);

			int conditionsCount = _conditions.Count;
			s.Stream(ref conditionsCount, 10);
			for (int i = 0; i < conditionsCount; i++)
			{
				if (s.State == StreamState.Read)
				{
					//_conditions.Add(Halo4Properties.ConditionsDatabase.ReadScriptObject(s));
				}
				else if (s.State == StreamState.Write)
				{
					//_conditions[i].Serialize(s);
				}
			}*/
			/* ushort count = Register<ushort>("Count", n: 10);
			for (var i = 0; i < count; i++)
			{
				Group("Condition[" + i + "]", () =>
				{
					byte opcode = Register<byte>("Opcode", n: 5);
					if (opcode <= 0)
						return;

					Register<bool>("IsInverse");
					Register<ushort>("UnionId", n: 10);
					Register<ushort>("StartAction", n: 11);
					Group("Parameters", () =>
					{
						var condition = GameRegistry.Halo4.MegaloConditionsDatabase.GetDefinition(opcode);
						Import<DefinitionParameters>(new Context { { "Definition", condition } });
					});
				});
			}*/

			/*ushort count = Register<ushort>("Count", n: 11);
			for (var i = 0; i < count; i++)
			{
				Group("Action[" + i + "]", () =>
				{
					byte opcode = Register<byte>("Opcode");
					if (opcode > 0)
					{
						Group("Parameters", () =>
						{
							var action = GameRegistry.Halo4.MegaloActionsDatabase.GetDefinition(opcode);
							Import<DefinitionParameters>(new Context { { "Definition", action } });
						});
					}
				});
			}*/

			/*byte count = Register<byte>("Count");
			for (var i = 0; i < count; i++)
			{
				Group("Trigger[" + i + "]", () =>
				{
					var enumType = Register<byte>("EnumType", n: 3);
					Register<byte>("Type", n: 4);

					switch (enumType)
					{
						case 5:
							bool isFilterIndexNull = Register<bool>("IsFilterIndexNull");
							if (!isFilterIndexNull)
								Register<byte>("FilterIndex", n: 4);
							break;

						case 6:
							Register<bool>("GameObjectType");

							bool isGameObjectFilterIndexNull = Register<bool>("IsGameObjectFilterIndexNull");
							if (!isGameObjectFilterIndexNull)
								Register<byte>("GameObjectFilterIndex", n: 2);
							break;
					}

					Register<uint>("ConditionIndex", n: 10);
					Register<uint>("ConditionCount", n: 10);
					Register<uint>("ActionIndex", n: 11);
					Register<uint>("ActionCount", n: 11);
					Register<byte>("FrameUpdateFrequency");
					Register<byte>("FrameUpdateOffset");
				});
			}*/
			/* 
				{ "Conditions", DefineConditions },
				{ "Actions", DefineActions },
				{ "Triggers", DefineTriggers },
				{ "CustomStats", DefineCustomStats },
				{ "Variables", DefineVariables },
				{ "Widgets", DefineHudWidgets },
				{ "Events", DefineEvents },
				{ "RequiredObjectTypes", DefineRequiredObjectTypes },
				{ "ObjectFilters", DefineObjectFilters },
				{ "CandySpawnerFilters", DefineCandySpawnersFilters },*/
		}

		#endregion
	}
}
