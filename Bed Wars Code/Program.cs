using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using Bed_Wars_Code;
using Techcraft7_DLL_Pack;

namespace Bed_Wars_Code
{
	using static Console;
	using static ConsoleColor;
	using static ColorConsoleMethods;
	public class Program
	{
		private static readonly ConsoleColor[] TEAM_COLORS = new ConsoleColor[] { Red, Blue, Green, Yellow, Cyan, Magenta, Gray, DarkMagenta };

		public static void Main(string[] args)
		{
			WriteLineMultiColor(new string[] { "~~~~~~~~~~ [", "Bed", "Wars", "] ~~~~~~~~~~" }, new ConsoleColor[] { White, Yellow, Cyan, White});
			int numPlayers = ReadInt("Number of players: ", Map.MIN_PLAYERS, Map.MAX_PLAYERS);
			List<Player> players = new List<Player>();
			for (int i = 0; i < numPlayers; i++)
			{
				Write($"Player {i + 1}, enter your name: ");
				players.Add(new Player(ReadLine(), TEAM_COLORS[i]));
			}
			foreach (Player p in players)
			{
				Utils.PrintPlayerNameInText("Welcome, %PLAYER%!\n", p);
			}
			Game game = new Game(players);
			game.Start();
			Write("Press enter to continue...");
			ReadLine();
		}

		public static int ReadInt(string prompt, int min, int max)
		{
			bool first = true;
			int v = -1;
			do
			{
				if (!first)
				{
					WriteLineColor($"Please enter a valid number! {min} to {max}", Red);
				}
				first = false;
				Write(prompt);
			}
			while (!int.TryParse(ReadLine(), out v) || !Utils.IsInRange(v, min, max));
			return v;
		}
	}
}