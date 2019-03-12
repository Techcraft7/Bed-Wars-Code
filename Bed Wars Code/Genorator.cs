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
	public class Genorator : Location
	{
		public Genorator(string name, Map map, int blocksreq) : base(name, map, blocksreq)
		{
			
		}

		internal bool MultiRes = false;
		
		public Resources SingleResItemID = Resources.Null;
		
		public int step = 0;

		internal int SingleResTime = 0;
		
		public int[] MultiResTimes =  {
			0,
			0,
			0,
			0
		};

		//iron, gold, diamond, emerald
		public int[] MultiResItems =  {
			0,
			0,
			0,
			0
		};

		public int SingleResItemCount = 0;
		
		//iron, gold, diamond, emerald

		public int[] GetResources()
		{
			if (MultiRes)
			{
				return MultiResItems;
			}
			else
			{
				switch (SingleResItemID)
				{
					case Resources.Iron:
						return new int[4] {SingleResItemCount, 0, 0, 0};
						break;
					case Resources.Gold:
						return new int[4] {0, SingleResItemCount, 0, 0};
						break;
					case Resources.Diamond:
						return new int[4] {0, 0, SingleResItemCount, 0};
						break;
					case Resources.Emerald:
						return new int[4] {0, 0, 0, SingleResItemCount};
						break;
					default:
						throw new InvalidOperationException("Resource was set to null!");
				}
			}
		}
		
		public void Update()
		{
			if (MultiRes)
			{
				for (int i = 0; i < MultiResTimes.Length; i++)
				{
					if (MultiResTimes[i] <= 0)
					{
						continue;
					}
					if (step % MultiResTimes[i] == 0 && MultiResTimes[i] > 0)
					{
						MultiResItems[i]++;
					}
				}
			}
			else
			{
				if (SingleResTime <= 0)
				{
					return;
				}
				if (step % SingleResTime == 0 && SingleResTime > 0)
				{
					SingleResItemCount++;
				}
			}
		}
	}
}

