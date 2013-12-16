namespace Nitrogen.Enums
{
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
}
