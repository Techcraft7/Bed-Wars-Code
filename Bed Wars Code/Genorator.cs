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

		public int step = 0;

		public int[] Times =  {
			0,
			0,
			0,
			0
		};

		//iron, gold, diamond, emerald
		public int[] Items =  {
			0,
			0,
			0,
			0
		};

		//iron, gold, diamond, emerald
		Resources Res;

		public void Update()
		{
			for (int i = 0; i < Times.Length; i++) {
				if (Times[i] < 0) {
					continue;
				}
				if (step % Times[i] == 0 && Times[i] > 0) {
					Items[i]++;
				}
			}
		}
	}
}

