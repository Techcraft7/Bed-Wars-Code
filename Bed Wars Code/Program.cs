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
		public static List<Team> teams = new List<Team>();
		public static List<Player> players = new List<Player>();
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
			for (int i = 0; i < NumberOfPlayers; i++)
			{
				Console.WriteLine("Enter a name for Player {0}:", i + 1);
				players.Add(new Player(Console.ReadLine()));
			}
			Console.WriteLine("Loading...");
			NumberOfTeams = NumberOfPlayers;
			List<int> used = new List<int>();
			int c = 1;
			int p = 0;
			for (int i = 0; i < NumberOfPlayers; i++)
			{
				teams.Add((new Team(((ConsoleColor)c))));
				c++;
			}
			for (int i = 0; i < teams.Count; i++)
			{
				p = new Random(DateTime.Now.Millisecond).Next(0, players.Count - 1);
				foreach (int n in used)
				{
					if (p == n)
					{
						while (p != n)
						{
							p = new Random(DateTime.Now.Millisecond).Next(0, players.Count - 1);
						}
					}
				}
				players[p].CurrentTeam = teams[i];
			}
			CurrentGame = new Game(players, new MapGenorator(teams).GeneratedMap);
			StartGame();
			Console.WriteLine("Starting!");
			while (CurrentGame.Running)
			{
				
			}
			Console.Write("Press enter to continue . . . ");
			Console.ReadLine();
		}
		public static void StartGame()
		{
			CurrentGame.Running = true;
		}
	}
}