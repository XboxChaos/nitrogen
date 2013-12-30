using System;

namespace Nitrogen.Enums
{
	[Flags]
	public enum PlayerKillerTypeFlags
	{
		Guardians,
		Suicide,
		Kill,
		Betrayal,
		Quit
	}
}