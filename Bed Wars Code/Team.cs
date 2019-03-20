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
	public class Team
	{
		public Base BaseLoc;
		
		public bool SuppressEliminationMessage = false;

		public bool Eliminated = false;
		
		public List<Player> Players = new List<Player>();

		public string Name;

		public Team(ConsoleColor TeamColor)
		{
			this.DisplayColor = TeamColor;
			this.Name = DisplayColor.ToString() + " Base";
		}

		public void Update()
		{
			if (!SuppressEliminationMessage)
			{
				if (Players.Count == 1)
				{
					if (Players[0].IsDead && BaseLoc.TeamBed.destroyed)
					{
						Eliminated = true;
						CCM.WriteLineMultiColor(new string[] {"------------------\n\n >", DisplayColor.ToString(), " team has been eliminated!\n\n------------------"}, new ConsoleColor[] {ConsoleColor.White, DisplayColor, ConsoleColor.White});
					}
				}
				else
				{
					CCM.WriteLineMultiColor(new string[] {"------------------\n\n >", DisplayColor.ToString(), " team has been eliminated!\n\n------------------"}, new ConsoleColor[] {ConsoleColor.White, DisplayColor, ConsoleColor.White});
					Eliminated = true;
				}
			}
		}
		
		public ConsoleColor DisplayColor;

		Color TeamColor;//for a gui (to be implimented)
	}
}

