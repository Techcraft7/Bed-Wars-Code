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
	public class EmeraldGen : Genorator
	{
		public EmeraldGen(string name, Map map, int blocksreq, int[] coords) : base(name, map, blocksreq, coords)
		{
			this.SingleResItemID = Resources.Emerald;
		}
	}
}

