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
	public class Location
	{
		public bool BuiltTo = false;

		public string name = "";

		public int[] Coords = new int[2];

		public List<Location> ConnectedPlaces = new List<Location>();

		public int BlocksRequired = 0;

		List<BWAction> acts = new List<BWAction>();

		public Question ques;

		public Map map;

		public Location(string name, Map map, int blocksreq)
		{
			this.map = map;
			this.BlocksRequired = blocksreq;
			this.name = name;
			ConnectedPlaces = GetConnectedPlaces();
		}

		public List<Location> GetConnectedPlaces()
		{
			List<Location> output = new List<Location>();
			//0=left
			//1=up
			//2=right
			//3=down
			try
			{
				output.Add(map.GetLocationByCoords(Coords[0] - 1, Coords[1]));
			}
			catch (Exception e) 
			{
			}
			try
			{
				output.Add(map.GetLocationByCoords(Coords[0], Coords[1] + 1));
			}
			catch (Exception e) 
			{
			}
			try
			{
				output.Add(map.GetLocationByCoords(Coords[0] + 1, Coords[1]));
			}
			catch (Exception e) 
			{
			}
			try
			{
				output.Add(map.GetLocationByCoords(Coords[0], Coords[1] - 1));
			}
			catch (Exception e) 
			{
			}
			return output;
		}

		public void SendPlayer(Player p)
		{
			if (p.loc.name == name) 
			{
				Console.WriteLine("You are already here!");
			}
		}
	}
}

