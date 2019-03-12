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
}