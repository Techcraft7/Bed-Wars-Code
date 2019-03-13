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
	public class Bed
	{
		public int level = 0;
		
		public bool destroyed = false;
		
		public Team BedTeam;
		
		public BWAction DefendAct;
		
		public Bed(Team t)
		{
			BedTeam = t;
			DefendAct = new BWAction("defend bed", Defend);
		}
		
		public void Defend(Player p)
		{
			Utils.PrintPlayerNameWithFormattingPlusMoreText(p, "you chose to do bed defense.");
			if (level == 0)
			{
				Console.WriteLine("You don\'t have a bed defense! Would you like to buy one for 4 iron?");
				string ans = "";
				while (ans == "" || ans != "yes" || ans != "no")
				{
					ans = Console.ReadLine().ToLower();
					if (ans == "yes" || ans == "no")
					{
						if (ans == "yes")
						{
							if (p.Items[0] >= 4)
							{
								
							}
						}
					}
				}
			}
		}
	}
}
