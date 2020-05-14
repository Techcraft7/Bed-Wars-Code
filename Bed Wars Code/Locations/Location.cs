using System;
using System.Linq;
using System.Collections.Generic;

namespace Bed_Wars_Code
{
	using static Techcraft7_DLL_Pack.ColorConsoleMethods;
	internal abstract class Location
	{
		public const int BRIDGE_DISTANCE = 32;
		public readonly bool[] Bridges = new bool[4];

		public abstract void GeneratorTick();
		public abstract KeyValuePair<char, ConsoleColor> GetMapSymbol();
		public virtual List<KeyValuePair<string, Func<Game, bool>>> GetOptions()
		{
			var list = new List<KeyValuePair<string, Func<Game, bool>>>
			{
				new KeyValuePair<string, Func<Game, bool>>("Leave", new Func<Game, bool>(LeaveLocation)),
				new KeyValuePair<string, Func<Game, bool>>("Map", new Func<Game, bool>(ShowMap))
			};
			return list;
		}

		private bool ShowMap(Game game)
		{
			game.Map.DrawMap();
			WriteLineColor($"You are at: ({game.CurrentPlayer.Coords.Item1}, {game.CurrentPlayer.Coords.Item2}) - {game.Map.GetPlayerLocation(game.CurrentPlayer)}", ConsoleColor.Yellow);
			Console.WriteLine("Press enter to close map!");
			_ = Console.ReadLine();
			return false;
		}

		private bool LeaveLocation(Game game)
		{
			Tuple<int, int> c = game.CurrentPlayer.Coords;
			Location u = game.Map.GetLocation(c.Item1, c.Item2 - 1);
			Location d = game.Map.GetLocation(c.Item1, c.Item2 + 1);
			Location l = game.Map.GetLocation(c.Item1 - 1, c.Item2);
			Location r = game.Map.GetLocation(c.Item1 + 1, c.Item2);
			Location[] dirs = new Location[] { u, d, l, r };
			bool[] nulls = dirs.Select(x => x == null).ToArray();
			for (int i = 0; i < dirs.Length; i++)
			{
				if (!nulls[i])
				{
					Console.WriteLine($"[{i}] {new string[] { "Up", "Down", "Left", "Right"}[i]} -> {dirs[i].ToString()}");
				}
			}
			int ans = -1;
			while (ans < 0 || (Utils.IsInRange(ans, 0, 3) ? nulls[ans] : false))
			{
				ans = Program.ReadInt("Enter a number: ", 0, 3);
			}
			int dx = 0;
			int dy = 0;
			switch (ans)
			{
				case 0:
					dy = -1;
					break;
				case 1:
					dy = 1;
					break;
				case 2:
					dx = -1;
					break;
				case 3:
					dx = 1;
					break;
			}
			if (!Bridges[ans])
			{
				bool build = Program.ReadInt("0 = No\n1 = Yes\nBuild a bridge: ", 0, 1) == 1;
				if (!build)
				{
					WriteLineColor("You cannot leave if there is not a bridge!", ConsoleColor.Red);
					return false;
				}
				else if (game.CurrentPlayer.Inventory.Blocks >= BRIDGE_DISTANCE)
				{
					WriteLineColor($"You built a bridge!", ConsoleColor.Green);
					game.CurrentPlayer.Inventory.BuildBridge();
				}
				else
				{
					WriteLineColor("You dont have enough blocks!", ConsoleColor.Red);
					return false;
				}
			}
			WriteLineColor("You left!", ConsoleColor.Green);
			game.CurrentPlayer.Coords = new Tuple<int, int>(c.Item1 + dx, c.Item2 + dy);
			return true;
		}

		public void ExecuteTurn(Game game)
		{
			foreach (Player p in game.Players)
			{
				if (p != game.CurrentPlayer)
				{
					if (p.Coords.Equals(game.CurrentPlayer.Coords))
					{
						Player v = p;
						game.StartCombat(ref v);
					}
				}
			}
			bool res = true;
			do
			{
				var opts = GetOptions();
				Console.WriteLine("You may do one of these:");
				Console.WriteLine(string.Empty.PadLeft(30, '-'));
				foreach (string k in opts.Select(x => x.Key))
				{
					Console.WriteLine($"-> {k}");
				}
				Console.WriteLine(string.Empty.PadLeft(30, '-'));
				string answer = null;
				while (answer == null || opts.FindIndex(x => x.Key.ToLower() == answer.ToLower()) < 0)
				{
					Console.Write("Enter an option listed above: ");
					answer = Console.ReadLine();
				}
				Console.WriteLine($"You chose: {answer}");
				var kv = opts.Find(x => x.Key.ToLower() == answer.ToLower());
				res = kv.Value.Invoke(game);
				if (!res)
				{
					Console.WriteLine("The action you chose was canceled or does not consume a turn! Go again!");
				}
			}
			while (!res);
		}
	}
}