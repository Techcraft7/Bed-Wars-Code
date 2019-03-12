
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

		public Location loc;

		public int[] Items = new int[4];

		//iron, gold, diamond, emerald
		public int[] EnderChest = new int[4];

		//iron, gold, diamond, emerald
		public Team CurrentTeam;

		public ConsoleColor GetTeamColor()
		{
			return this.CurrentTeam.DisplayColor;
		}
		
		public Player(string name)
		{
			this.Name = name;
		}
	}
}

