
using System;

namespace Bed_Wars_Code
{
	/// <summary>
	/// Description of Utils.
	/// </summary>
	public static class Utils
	{
		public static Question GetPlayerLocationQuestion(Player p)
		{
			return p.loc.ques;
		}
	}
}
