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
	public class Program
	{
		public static int NumberOfPlayers = 0;
		public static Game CurrentGame;
		public static int NumberOfTeams = 0;
		public static List<Team> teams;
		public static List<Player> players;
		public static void Main(string[] args)
		{
			Console.WriteLine("Welcome to BedWars!");
			while (NumberOfPlayers == 0)
			{
				try
				{
					Console.WriteLine("How many players are playing?");
					NumberOfPlayers = Convert.ToInt32(Console.ReadLine());
					break;
				}
				catch (Exception e)
				{
					CCM.WriteLineColor("An error happened! :( try again!", ConsoleColor.Red);
					Console.ReadLine();
					Console.Clear();
				}
			}
			NumberOfTeams = NumberOfPlayers;
			List<int> used = new List<int>();
			int c = 1;
			int p = 0;
			for (int i = 0; i < NumberOfPlayers / 2; i++)
			{
				teams.Add((new Team(((ConsoleColor)c))));
				c++;
			}
			Console.WriteLine("Loading...");
			for (int i = 0; i < players.Count; i++)
			{
				p = new Random(DateTime.Now.Millisecond).Next(0, players.Count);
				foreach (int n in used)
				{
					if (p == n)
					{
						while (p != n)
						{
							p = new Random(DateTime.Now.Millisecond).Next(0, players.Count);
						}
					}
				}
				players[p].CurrentTeam = teams[i];
			}
			CurrentGame = new Game(players, new MapGenorator(teams).GeneratedMap);
			StartGame();
			while (CurrentGame.Running)
			{
				//do stuff
			}
			Console.Write("Press enter to continue . . . ");
			Console.ReadLine();
		}
		public static void StartGame()
		{
			CurrentGame.Running = true;
		}
	}
	public class Question
	{
		public List<BWAction> actions = new List<BWAction>();
		public string Text = "";
		bool success = false;
		public Question(string QText, List<BWAction> actions)
		{
			Text = QText;
			this.actions = actions;
		}
		private string GetOptions()
		{
			string output = "";
			foreach (BWAction i in actions)
			{
				output += i.alias + ",";
			}
			output.Remove(output.Length - 1, 1);
			return output;
		}
		public void Ask()
		{
			while (success == false)
			{
				Console.WriteLine(Text);
				foreach (BWAction i in actions)
				{
					if (Console.ReadLine() == i.alias)
					{
						i.Execute();
						success = true;
					}
					else
					{
						CCM.WriteLineColor("You did not enter a valid option! options: " + GetOptions() + "\nPress enter to continue", ConsoleColor.Red);
						Console.ReadLine();
						Console.Clear();
						break;
					}
				}
			}
			success = false;
		}
	}
	public class BWAction
	{
		public readonly string alias = "";
		public Action action;
		public BWAction(string name, Action action)
		{
			this.alias = name;
			this.action = action;
		}
		public void Execute()
		{
			action.Invoke();
		}
	}
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
	public class Game
	{
		public Map BWMap;
		public bool Running = false;
		public List<Player> Players = new List<Player>();
		public Game(List<Player> Players, Map map)
		{
			this.Players = Players;
			this.BWMap = map;
		}
	}
	public class Map
	{
		public Location[][] LocCoords = null;
		public int width = 0;
		public int height = 0;
		public Map(int width, int height)
		{
			this.width = width;
			this.height = height;
		}
		public Location GetLocationByCoords(int x, int y)
		{
			return LocCoords[x][y];
		}
	}
	public class MapGenorator
	{
		public Map GeneratedMap;
		public MapGenorator(List<Team> Teams)
		{
			/*
			 * D-B-B-D
			 * | | | |
			 * B-E-E-B
			 * | | | |
			 * B-E-E-B
			 * | | | |
			 * D-B-B-D
			 */
		}
	}
	public class Base : Location
	{
		public Base(string name,Map map, int blocksreq) : base (name, map, blocksreq)
		{
			
		}
		public Team Team;
		public Forge TeamForge = new Forge();
		public bool BedDestoryed = false;
	}
	public class Team
	{
		public Base BaseLoc;
		public List<Player> Players = new List<Player>();
		public string Name;
		public Team(ConsoleColor TeamColor)
		{
			this.DisplayColor = TeamColor;
		}
		ConsoleColor DisplayColor;
		Color TeamColor; //for a gui (to be implimented)
	}
	public class Player
	{
		public string Name = "";
		public Location loc;
		public int[] Items = new int[4];  //iron, gold, diamond, emerald
		public int[] EnderChest = new int[4];  //iron, gold, diamond, emerald
		public Team CurrentTeam;
		public Player(string name)
		{
			this.Name = name;
		}
	}
	public class Genorator : Location
	{
		public Genorator(string name,Map map, int blocksreq) : base (name, map, blocksreq)
		{
			
		}
		public int step = 0;
		public int[] Times = {0, 0, 0, 0}; //iron, gold, diamond, emerald
		public int[] Items = {0, 0, 0, 0}; //iron, gold, diamond, emerald
		Resources Res;
		public void Update()
		{
			for (int i = 0; i < Times.Length; i++)
			{
				if (Times[i] < 0)
				{
					continue;
				}
				if (step % Times[i] == 0  && Times[i] > 0)
				{
					Items[i]++;
				}
			}
		}
	}
	enum Resources
	{
		Null = 0, Iron = 1, Gold = 2, Diamond = 3, Emerald = 4
	}
	public class Forge : Genorator
	{
		public int[] Times = {1, 4, -1, 40}; //iron, gold, diamond, emerald
		public int[] Items = {0, 0, 0, 0}; //iron, gold, diamond, emerald
		public Forge(string name,Map map, int blocksreq) : base (name, map, blocksreq)
		{
			
		}
	}
	public class EmeraldGen : Genorator
	{
		public EmeraldGen(string name,Map map, int blocksreq) : base (name, map, blocksreq)
		{
			
		}
	}
	public class DiamondGen : Genorator
	{
		public DiamondGen(string name,Map map, int blocksreq) : base (name, map, blocksreq)
		{
			
		}
	}
}