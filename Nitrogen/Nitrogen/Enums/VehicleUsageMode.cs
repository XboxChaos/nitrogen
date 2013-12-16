namespace Nitrogen.Enums
{
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
