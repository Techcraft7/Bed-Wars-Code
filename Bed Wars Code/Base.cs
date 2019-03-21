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
	public class Base : Location
	{
		public Base(string name, Map map, int blocksreq, Team t, int[] coords) : base(name, map, blocksreq, coords)
		{
			Team = t;
			Setup();
		}
		
		public override void Setup()
		{
			this.TeamBed = new Bed(Team);
			var list = new List<BWAction>();
			list.Add(new BWAction("get resources", CollectResources));
			list.Add(new BWAction("bed defense", Defend));
			acts = list;
			TeamForge = new Forge("TeamForge", map, 0, Coords);
			this.ques = new Question("You are at your base, What do you do?", this.acts);
		}

		public Team Team;

		public Forge TeamForge;

		public Bed TeamBed;
				
		List<BWAction> acts;
		
		public void Defend(Player p)
		{
			Utils.PrintPlayerNameWithFormattingPlusMoreText(p, "you chose to do bed defense.");
			if (TeamBed.level == 0)
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
		
		public void CollectResources(Player p)
		{
			Console.WriteLine("You chose to collect resources:\nRolling...");
			int roll = new Random(DateTime.Now.Millisecond).Next(1, 7);
			Console.WriteLine("You rolled a {0}", roll);
			switch (roll)
			{
				case 1:
					Console.WriteLine("The items despawned :(");
					break;
				default:
					#if DEBUG
					Console.WriteLine(p.Items[0] + " iron");
					Console.WriteLine(TeamForge.MultiResItems[0] + " iron in forge");
					Console.WriteLine("step = " + TeamForge.step);
					#endif
					p.Items[0] += TeamForge.MultiResItems[0] * roll;
					p.Items[1] += TeamForge.MultiResItems[1] * roll;
					Console.WriteLine(p.GetItemMessage());
					break;
			}
		}
	}
}

