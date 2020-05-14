using Bed_Wars_Code.Locations;
using System;
using System.Linq;
using System.Collections.Generic;
using Techcraft7_DLL_Pack;

namespace Bed_Wars_Code
{
	internal class Map
	{
		public const int MIN_PLAYERS = 2;
		public const int MAX_PLAYERS = 8;

		private Random rng = new Random();
		private Location[/*Num cols (X)*/][/*Num rows (Y)*/] mapData;

		public Map(int numPlayers, List<Player> players)
		{
			Utils.ThrowIfOutOfBounds(numPlayers, MIN_PLAYERS, MAX_PLAYERS);
			Utils.ThrowIfAnyNull(players);
			switch (numPlayers)
			{
				case 2:
				case 3:
				case 4:
					mapData = Utils.CreateMatrix<Location>(3, 3);
					if (numPlayers == 2) //create 2 team map
					{
						mapData[0][0] = null;
						mapData[1][0] = new Base(players[0]);
						mapData[2][0] = null;
						mapData[0][1] = new Generator(GeneratorType.DIAMOND);
						mapData[1][1] = new Generator(GeneratorType.EMERALD);
						mapData[2][1] = new Generator(GeneratorType.DIAMOND);
						mapData[0][2] = null;
						mapData[1][2] = new Base(players[1]);
						mapData[2][2] = null;
					}
					else //create 4 team map
					{
						//make players of length 4 with null, this will create dead bases
						players = Utils.ExtendWithNull(players, 4).ToList();
						mapData[0][0] = new Generator(GeneratorType.DIAMOND);
						mapData[1][0] = new Base(players[0]);
						mapData[2][0] = new Generator(GeneratorType.DIAMOND);
						mapData[0][1] = new Base(players[2]);
						mapData[1][1] = new Generator(GeneratorType.EMERALD);
						mapData[2][1] = new Base(players[3]);
						mapData[0][2] = new Generator(GeneratorType.DIAMOND);
						mapData[1][2] = new Base(players[1]);
						mapData[2][2] = new Generator(GeneratorType.DIAMOND);
					}
					break;
				case 5:
				case 6:
				case 7:
				case 8:
					//make players of length 4 with null, this will create dead bases
					mapData = Utils.CreateMatrix<Location>(4, 4); //create 8 team map
					players = Utils.ExtendWithNull(players, 8).ToList();
					mapData[0][0] = new Generator(GeneratorType.DIAMOND);
					mapData[1][0] = new Base(players[0]);
					mapData[2][0] = new Base(players[1]);
					mapData[3][0] = new Generator(GeneratorType.DIAMOND);
					mapData[0][1] = new Base(players[2]);
					mapData[1][1] = new Generator(GeneratorType.EMERALD);
					mapData[2][1] = new Generator(GeneratorType.EMERALD);
					mapData[3][1] = new Base(players[3]);
					mapData[0][2] = new Base(players[4]);
					mapData[1][2] = new Generator(GeneratorType.EMERALD);
					mapData[2][2] = new Generator(GeneratorType.EMERALD);
					mapData[3][2] = new Base(players[5]);
					mapData[0][3] = new Generator(GeneratorType.DIAMOND);
					mapData[1][3] = new Base(players[6]);
					mapData[2][3] = new Base(players[7]);
					mapData[3][3] = new Generator(GeneratorType.DIAMOND);
					break;
				default:
					throw new ArgumentException($"{nameof(numPlayers)} is invalid!", nameof(numPlayers));
			}
			foreach (Player p in players)
			{
				if (p != null)
				{
					SendPlayerToSpawn(p);
				}
			}
		}

		public Location GetLocation(int x, int y)
		{
			try
			{
				return mapData[x][y];
			}
			catch
			{
				return null;
			}
		}

		public void TickGenerators()
		{
			for (int y = 0; y < mapData.Length; y++)
			{
				for (int x = 0; x < mapData[y].Length; x++)
				{
					var l = mapData[x][y];
					if (l != null)
					{
						if (l.GetType().IsSubclassOf(typeof(Generator)) || l.GetType() == typeof(Generator))
						{
							for (int i = 0; i <= rng.Next(Generator.DIAMOND_TIME / 2); i++)
							{
								l.GeneratorTick();
							}
						}
					}
				}
			}
		}

		public Location GetPlayerLocation(Player p)
		{
			if (p == null)
			{
				throw new ArgumentNullException(nameof(p), "Cannot get the location of a null player!");
			}
			try
			{
				return mapData[p.Coords.Item1][p.Coords.Item2];
			}
			catch //if out of bounds
			{
				return null;
			}
		}

		public Base GetPlayerBase(Player p)
		{
			for (int y = 0; y < mapData.Length; y++)
			{
				for (int x = 0; x < mapData[y].Length; x++)
				{
					if (mapData[x][y] != null)
					{
						if (mapData[x][y].GetType() == typeof(Base))
						{
							//"as base" is the same as "(Base)mapData[x][y]"
							//so basically a cast
							return mapData[x][y] as Base;
						}
					}
				}
			}
			return null;
		}

		public void SendPlayerToSpawn(Player p)
		{
			p.Health = 20;
			for (int y = 0; y < mapData.Length; y++)
			{
				for (int x = 0; x < mapData.Length; x++)
				{
					if (mapData[x][y] != null)
					{
						if (mapData[x][y].GetType() == typeof(Base))
						{
							Base b = (Base)mapData[x][y];
							if (p.Color == b.Color)
							{
								p.Coords = new Tuple<int, int>(x, y);
							}
						}
					}
				}
			}
		}

		public void DrawMap()
		{
			string header = "------- MAP -------";
			ColorConsoleMethods.WriteLineColor(header, ConsoleColor.White);
			for (int y = 0; y < mapData.Length; y++)
			{
				for (int x = 0; x < mapData[y].Length; x++)
				{
					if (mapData[x][y] == null)
					{
						Console.Write("  ");
					}
					else
					{
						var kv = mapData[x][y].GetMapSymbol();
						ColorConsoleMethods.WriteColor(kv.Key.ToString(), kv.Value);
						Console.Write(" ");
					}
				}
				Console.WriteLine("\n");
			}
			ColorConsoleMethods.WriteLineColor("-".PadLeft(header.Length, '-'), ConsoleColor.White);
		}
	}
}