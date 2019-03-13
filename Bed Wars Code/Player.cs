
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
	public class Player
	{
		public string Name = "";

		public int health = 20;
		
		public bool IsAtBase = false;
		
		public Location loc;

		public int[] Items = new int[4];

		public bool IsDead = false;
		
		//iron, gold, diamond, emerald
		public int[] EnderChest = new int[4];

		//iron, gold, diamond, emerald
		public Team CurrentTeam;

		public ConsoleColor GetTeamColor()
		{
			return this.CurrentTeam.DisplayColor;
		}
		
		public string GetItemMessage()
		{
			return string.Format("You have:\n{0} iron\n{1} gold\n{2} diamond\n{3} emerald", Items[0], Items[1], Items[2], Items[3]);
		}
		
		public Player(string name)
		{
			this.Name = name;
		}
	}
}

