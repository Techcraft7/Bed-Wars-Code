using System;
using System.Linq;
using System.Collections.Generic;

namespace Bed_Wars_Code
{
	internal abstract class Location
	{
		public abstract void GeneratorTick();
		public abstract KeyValuePair<char, ConsoleColor> GetMapSymbol();
		public virtual List<KeyValuePair<string, Action<Game>>> GetOptions()
		{
			var list = new List<KeyValuePair<string, Action<Game>>>
			{
				new KeyValuePair<string, Action<Game>>("Leave", new Action<Game>(LeaveLocation))
			};
			return list;
		}

		private void LeaveLocation(Game game)
		{
			Tuple<int, int> c = game.CurrentPlayer.Coords;
			Location u = game.map.GetLocation(c.Item1, c.Item2 - 1);
			Location d = game.map.GetLocation(c.Item1, c.Item2 + 1);
			Location l = game.map.GetLocation(c.Item1 - 1, c.Item2);
			Location r = game.map.GetLocation(c.Item1 + 1, c.Item2);
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
			throw new NotImplementedException("FINISH THIS!");
		}

		public void ExecuteTurn(Game game)
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
			kv.Value.Invoke(game);
		}
	}
}