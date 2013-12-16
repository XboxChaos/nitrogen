namespace Nitrogen.Enums
{
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
}
