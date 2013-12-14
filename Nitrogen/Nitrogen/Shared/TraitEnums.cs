namespace Nitrogen.Shared
{
	public partial class PlayerTraits
	{
		/// <summary>
		/// Specifies a flag which can be inherited.
		/// </summary>
		public enum TraitFlag
			: byte
		{
			/// <summary>
			/// This trait will be inherited.
			/// </summary>
			Inherit,

			/// <summary>
			/// This trait is disabled.
			/// </summary>
			Disabled,

			/// <summary>
			/// This trait is enabled.
			/// </summary>
			Enabled,
		}

		/// <summary>
		/// Specifies the behavior of Active Camo for a player.
		/// </summary>
		public enum ActiveCamoMode
		{
			/// <summary>
			/// The value will be inherited.
			/// </summary>
			Inherit,

			/// <summary>
			/// Active Camo is disabled.
			/// </summary>
			Disabled,

			/// <summary>
			/// Active Camo is enabled.
			/// </summary>
			Enabled,

			/// <summary>
			/// Only Grunts will be fooled.
			/// </summary>
			Poor,

			/// <summary>
			/// This might fool an Elite.
			/// </summary>
			Good,

			/// <summary>
			/// This might fool other players.
			/// </summary>
			/// <remarks>
			/// In Halo 4, this isn't as good as it used to be since one of the Title Updates nerfed
			/// the Active Camo.
			/// </remarks>
			Best,
		}

		/// <summary>
		/// Specifies the visibility of a HUD element.
		/// </summary>
		public enum HudVisibility
		{
			/// <summary>
			/// Visibility will be inherited.
			/// </summary>
			Inherit,

			/// <summary>
			/// This HUD element is not visible to anyone.
			/// </summary>
			None,

			/// <summary>
			/// This HUD element is visible to teammates.
			/// </summary>
			VisibleToAllies,

			/// <summary>
			/// This HUD element is visible to all players.
			/// </summary>
			VisibleToEveryone,
		}

		public enum InfiniteAmmoMode
		{
			/// <summary>
			/// This setting will be inherited.
			/// </summary>
			Inherit,

			/// <summary>
			/// Infinite ammo is disabled.
			/// </summary>
			Disabled,

			/// <summary>
			/// Infinite ammo is enabled.
			/// </summary>
			Enabled,

			/// <summary>
			/// Ra-ta-ta-ta-ta-ta-ta-ta-ta-ta-ta-ta-ta-ta-tat.
			/// </summary>
			BottomlessClip,
		}

		/// <summary>
		/// Specifies what the motion sensor picks up.
		/// </summary>
		public enum MotionSensorMode
		{
			/// <summary>
			/// The motion sensor mode will be inherited.
			/// </summary>
			Inherit,

			/// <summary>
			/// The motion sensor is disabled.
			/// </summary>
			Off,

			/// <summary>
			/// The motion sensor picks up teammates only.
			/// </summary>
			AlliesOnly,

			/// <summary>
			/// The motion sensor picks up moving enemies.
			/// </summary>
			Normal,

			/// <summary>
			/// The motion sensor picks up all players regardless of whether they are moving.
			/// </summary>
			Enhanced,
		}

		/// <summary>
		/// Specifies the vehicle access permission.
		/// </summary>
		public enum VehicleUsageMode
		{
			/// <summary>
			/// Vehicle usage will be inherited.
			/// </summary>
			Inherit,

			/// <summary>
			/// Vehicles cannot be accessed.
			/// </summary>
			None,

			/// <summary>
			/// Only the driver seat is accessible.
			/// </summary>
			DriverOnly,

			/// <summary>
			/// Only the gunner is accessible.
			/// </summary>
			GunnerOnly,

			/// <summary>
			/// Only the passenger seat is accessible.
			/// </summary>
			PassengerOnly,

			/// <summary>
			/// Every seat of a vehicle is accessible.
			/// </summary>
			FullUse,
		}
	}
}