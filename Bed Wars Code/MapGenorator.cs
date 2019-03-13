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
	public class MapGenorator
	{
		private Map GeneratedMap;
		
		public Map MakeMap(List<Team> Teams)
		{
			Map x = new Map(4,4);
			/* Example Map
			 * D-B-B-D
			 * | | | |
			 * B-E-E-B
			 * | | | |
			 * B-E-E-B
			 * | | | |
			 * D-B-B-D
			 */
			x.LocCoords = new Location[][] 
			{
				new Location[]
				{
					new DiamondGen("Diamond Gen 1", x, 32), new Base(Teams[0].Name, x, 32, Teams[0]), new Base(Teams[1].Name, x, 32, Teams[1]), new DiamondGen("Diamond Gen 2", x, 32)
				},
				new Location[]
				{
					new Base(Teams[2].Name, x, 32, Teams[2]), new EmeraldGen("Emerald Gen 1", x, 32),new EmeraldGen("Emerald Gen 2", x, 32), new Base(Teams[3].Name, x, 32, Teams[3])
				},
				new Location[]
				{
					new Base(Teams[4].Name, x, 32, Teams[4]), new EmeraldGen("Emerald Gen 3", x, 32),new EmeraldGen("Emerald Gen 4", x, 32), new Base(Teams[5].Name, x, 32, Teams[5])
				},
				new Location[]
				{
					new DiamondGen("Diamond Gen 3", x, 32), new Base(Teams[6].Name, x, 32, Teams[6]), new Base(Teams[7].Name, x, 32, Teams[7]), new DiamondGen("Diamond Gen 4", x, 32)
				}
			};
			GeneratedMap = x;
			return x;
		}
		
		public MapGenorator(List<Team> Teams)
		{
			
		}
	}
}

