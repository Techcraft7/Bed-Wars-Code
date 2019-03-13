using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using Bed_Wars_Code;
using Techcraft7_DLL_Pack;
using CCM = Techcraft7_DLL_Pack.ColorConsoleMethods;

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
		
		public static void PrintPlayerNameWithFormattingPlusMoreText(Player p, string text)
		{
			CCM.WriteLineMultiColor(new string[] {"[" + p.Name + "]", "," + text}, new ConsoleColor[] {p.CurrentTeam.DisplayColor, ConsoleColor.White});
		}
	}
}
