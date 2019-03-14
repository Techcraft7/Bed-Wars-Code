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
			list.Add(TeamBed.DefendAct);
			acts = list;
			this.ques = new Question("You are at your base, What do you do?", this.acts);
		}

		public Team Team;

		public Forge TeamForge;

		public Bed TeamBed;
				
		List<BWAction> acts;
		
		public void CollectResources(Player p)
		{
			Console.WriteLine("You chose to collect resources:\nRolling...");
			int roll = new Random(DateTime.Now.Millisecond).Next(1, 7);
			Console.WriteLine();
		}
	}
}

