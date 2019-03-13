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
			while (NumberOfPlayers <= 1 || NumberOfPlayers > 8)
			{
				try
				{
					Console.WriteLine("How many players are playing?");
					NumberOfPlayers = Convert.ToInt32(Console.ReadLine());
					if (NumberOfPlayers > 8 || NumberOfPlayers == 1)
					{
						throw new Exception("YOU CAN ONLY HAVE UP TO 8 PLAYERS!\n YOU MUST HAVE 2 PLAYERS");
					}
					break;
				}
				catch (Exception e)
				{
					CCM.WriteLineColor("An error happened! :( try again!\n" + e.Message, ConsoleColor.Red);
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
			List<int> usedclrs = new List<int>();
			int c = 1;
			int index = 0;
			int p = 0;
			teams.Add(new Team(ConsoleColor.White));
			usedclrs.Add(0xF);
			Random rng = new Random();
			for (int i = 0; i < 7; i++)
			{
				c = rng.Next(0, players.Count);
				Console.WriteLine("c = {0}", c);
				foreach (int n in usedclrs)
				{
					if (c == n)
					{
						Console.WriteLine("c == n", c);
						while (c == n)
						{
							c = new Random().Next(0, players.Count);
							Console.WriteLine("c is now {0}", c);
						}
					}
				}
				teams.Add(new Team((ConsoleColor)c));
				usedclrs.Add(c);
			}
			for (int i = 0; i < teams.Count; i++)
			{
				p = new Random().Next(0, players.Count);
				Console.WriteLine("p = {0}",p);
				foreach (int n in used)
				{
					if (p == n)
					{
						Console.WriteLine("p == n",p);
						while (p != n)
						{
							p = new Random().Next(0, players.Count);
							Console.WriteLine("p is now {0}",p);
						}
					}
				}
				players[p].CurrentTeam = teams[i];
				used.Add(p);
			}
			CurrentGame = new Game(players, new MapGenorator(teams).GeneratedMap);
			StartGame();
			#if DEBUG
			Console.WriteLine("Press enter to continue");
				Console.ReadLine();
			#endif
			Console.Clear();
			Console.WriteLine("Starting!");
			while (CurrentGame.Running)
			{
				CurrentGame.NextTurn();
				foreach (Team t in CurrentGame.teams)
				{
					t.Update();
					if (t.Eliminated == false)
					{
						continue;
					}
					else
					{
						CurrentGame.Running = false;
						break;
					}
				}
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