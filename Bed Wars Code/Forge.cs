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
	public class Forge : Genorator
	{
		public int[] Times = {
			1,
			4,
			-1,//No diamonds in forge
			40
		};
		
		//iron, gold, diamond, emerald
		
		public Forge(string name, Map map, int blocksreq, int[] coords) : base(name, map, blocksreq, coords)
		{
			MultiRes = true;
		}
	}
}

