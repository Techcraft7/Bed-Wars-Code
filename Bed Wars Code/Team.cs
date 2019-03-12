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

		public bool Eliminated = false;
		
		public List<Player> Players = new List<Player>();

		public string Name;

		public Team(ConsoleColor TeamColor)
		{
			this.DisplayColor = TeamColor;
			this.Name = DisplayColor.ToString() + " Base";
		}

		public ConsoleColor DisplayColor;

		Color TeamColor;//for a gui (to be implimented)
	}
}

