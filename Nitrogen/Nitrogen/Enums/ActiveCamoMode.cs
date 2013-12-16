namespace Nitrogen.Enums
{
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
}
