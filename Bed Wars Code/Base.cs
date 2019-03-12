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
		public Base(string name, Map map, int blocksreq, Team t) : base(name, map, blocksreq)
		{
			Team = t;
			this.ques = new Question("You are at your base, What do you do?", this.acts);
			this.TeamBed = new Bed(Team);
			acts = new List<BWAction>() {new BWAction("get resources", CollectResources), new BWAction("build defense", TeamBed.Defend)};
		}

		public Team Team;

		public Forge TeamForge;

		public Bed TeamBed;
				
		List<BWAction> acts;
		
		public void CollectResources(Player p)
		{
			
		}
	}
}

